using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTO;
using PeliculasAPI.Entitdades;

namespace PeliculasAPI.Testss.PruebasUnitarias
{
    [TestClass]
    public class PeliculasControllerTests: BasePruebas
    {
        private string CrearDataPrueba()
        {
            var databaseName = Guid.NewGuid().ToString();
            var context = ConstruirContext(databaseName);
            var genero = new Genero() { Nombre = "genre 1" };

            var peliculas = new List<Pelicula>()
            {
                new Pelicula(){Titulo = "Pelicula 1", FechaEstreno = new DateTime(2010,1,1), EnCines = false},
                new Pelicula(){Titulo = "No estrenada", FechaEstreno = DateTime.Today.AddDays(1), EnCines = false},
                new Pelicula(){Titulo = "Pelicula en cines", FechaEstreno = DateTime.Today.AddDays(-1), EnCines = true}
            };

            var peliculaConGenero = new Pelicula()
            {
                Titulo = "Pelicula con Genero",
                FechaEstreno = new DateTime(2010, 1, 1),
                EnCines = false
            };
            peliculas.Add(peliculaConGenero);

            context.Add(genero);
            context.AddRange(peliculas);
            context.SaveChanges();

            var peliculaGenero = new PeliculasGeneros() { GeneroId = genero.Id, PeliculaId = peliculaConGenero.Id };
            context.Add(peliculaGenero);
            context.SaveChanges();

            return databaseName;
        }
    
        [TestMethod]
        public async Task FiltrarPorTitulo()
        {
            var nombreDB = CrearDataPrueba();
            var mapper = ConfigurarAutoMapper();
            var contexto = ConstruirContext(nombreDB);

            var controller = new PeliculasController(contexto, mapper, null, null);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var tituloPelicula = "Pelicula 1";
            var filtroDTO = new FiltroPeliculasDTO()
            {
                Titulo = tituloPelicula,
                CantidadRegistrosPorPagina = 10
            };

            var respuesta = await controller.Filtrar(filtroDTO);
            var peliculas = respuesta.Value;
            Assert.AreEqual(1, peliculas.Count);
            Assert.AreEqual(tituloPelicula, peliculas[0].Titulo);
        }
    }
}
