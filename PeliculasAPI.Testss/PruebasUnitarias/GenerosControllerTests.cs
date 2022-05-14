using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTO;
using PeliculasAPI.Entitdades;

namespace PeliculasAPI.Testss.PruebasUnitarias
{
    [TestClass]
    public class GenerosControllerTests: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerTodosLosGeneros()
        {
            // Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Genero 1" });
            contexto.Generos.Add(new Genero() { Nombre = "Genero 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);

            // Prueba
            var controller = new GenerosController(contexto2, mapper);
            var respuesta = await controller.Get();

            // Verificacion
            var generos = respuesta.Value;
            Assert.AreEqual(2, generos.Count);
        }

        [TestMethod]
        public async Task ObtenerGeneroPorIdNoExistente()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var controller = new GenerosController(contexto, mapper);
            var respuesta = await controller.Get(1);
            
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [TestMethod]
        public async Task ObtenerGeneroPorIdExistente()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Genero1" });
            contexto.Generos.Add(new Genero() { Nombre = "Genero2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new GenerosController(contexto2, mapper);

            var id = 1;
            var respuesta = await controller.Get(1);
            var resultado = respuesta.Value;
            Assert.AreEqual(id, resultado.Id);
        }

        [TestMethod]
        public async Task CrearGenero()
        {
            // Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            var nuevoGenero = new GeneroCreacionDTO() { Nombre = "genero prueba" };
            var controller = new GenerosController(contexto, mapper);

            // Prueba
            var respuesta = await controller.Post(nuevoGenero);
            var resultado = respuesta as CreatedAtRouteResult;

            // Verificacion
            Assert.IsNotNull(resultado);

            // Prueba
            var contexto2 = ConstruirContext(nombreDB);
            var cantidad = await contexto2.Generos.CountAsync();
            Assert.AreEqual(1, cantidad);
        }

        [TestMethod]
        public async Task ActualizarGenero()
        {
            // Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Genero 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new GenerosController(contexto2, mapper);

            // Prueba
            var generoCreacionDto = new GeneroCreacionDTO() { Nombre = "nuevo nombre" };
            var id = 1;
            var respuesta = await controller.Put(id, generoCreacionDto);
            var resultado = respuesta as StatusCodeResult;

            // Verificacion
            Assert.AreEqual(204, resultado.StatusCode);

            // Preparacion
            var contexto3 = ConstruirContext(nombreDB);

            // Prueba
            var existe = await contexto3.Generos.AnyAsync(x => x.Nombre == "nuevo nombre");

            // Verificacion
            Assert.IsTrue(existe);
        }

        [TestMethod]
        public async Task IntentaBorrarGeneroNoExistente()
        {
            // Preparacion
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            // Prueba
            var controller = new GenerosController(contexto, mapper);
            var respuesta = await controller.Delete(1);
            var resultado = respuesta as StatusCodeResult;

            // Verificacion
            Assert.AreEqual(404, resultado.StatusCode);
        }

        [TestMethod]
        public async Task BorrarGenero()
        {
            var nombreDB = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);
            var mapper = ConfigurarAutoMapper();

            contexto.Generos.Add(new Genero() { Nombre = "Genero 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreDB);
            var controller = new GenerosController(contexto2, mapper);

            var respuesta = await controller.Delete(1);
            var resultado = respuesta as StatusCodeResult;
            Assert.AreEqual(204, resultado.StatusCode);

            var contexto3 = ConstruirContext(nombreDB);
            var existeInDB = await contexto3.Generos.AnyAsync();
            Assert.IsFalse(existeInDB);
        }
    }
}
