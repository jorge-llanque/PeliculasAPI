using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Entitdades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PeliculasAPI
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>().HasKey(x => new { x.ActorId, x.PeliculaId });
            modelBuilder.Entity<PeliculasGeneros>().HasKey(x => new { x.PeliculaId, x.GeneroId });
            modelBuilder.Entity<PeliculasSalasDeCine>().HasKey(x => new { x.PeliculaId, x.SalaDeCineId });

            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var rolAdminId = "8cbea090-cd0e-4073-abea-c5922b4778bb";
            var usuarioAdminId = "2307ad12-4449-463c-a67c-dfa369d10ac9";

            var rolAdmin = new IdentityRole()
            {
                Id = rolAdminId,
                Name = "Admin",
                NormalizedName = "Admin"
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            var username = "jorge@test.com";
            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa123!")
            };

            //modelBuilder.Entity<IdentityUser>().HasData(usuarioAdmin);
            //modelBuilder.Entity<IdentityRole>().HasData(rolAdmin);
            //modelBuilder.Entity<IdentityUserClaim<string>>().HasData(new IdentityUserClaim<string>()
            //{
            //    Id = 1,
            //    ClaimType = ClaimTypes.Role,
            //    UserId = usuarioAdminId,
            //    ClaimValue = "Admin"
            //});

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            modelBuilder.Entity<SalaDeCine>()
                .HasData(new List<SalaDeCine>
                {
                    new SalaDeCine{Id=10, Nombre="Sambil", Ubicacion=geometryFactory.CreatePoint(new Coordinate(-17.6966808, -63.1678671))},
                    new SalaDeCine{Id=11, Nombre="Megacentro", Ubicacion=geometryFactory.CreatePoint(new Coordinate(-17.6966708, -63.167971))},
                    new SalaDeCine{Id=12, Nombre="Village East Cinema", Ubicacion=geometryFactory.CreatePoint(new Coordinate(-17.6966608, -63.1678071))}
                });
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculasActores> PeliculasActores { get; set; }
        public DbSet<PeliculasGeneros> PeliculasGeneros { get; set; }
        public DbSet<PeliculasSalasDeCine> PeliculasSalasDeCines { get; set; }
        public DbSet<SalaDeCine> SalaDeCines { get; set; }
    }
}
