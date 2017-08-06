namespace MoviesAspFinalProject.Migrations.DataContext
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesAspFinalProject.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.DataContext";
        }

        protected override void Seed(MoviesAspFinalProject.Models.DataContext context)
        {
            context.Movies.AddOrUpdate(x => x.Name, new Movie {
                Name = "Star Wars 4",
                ReleaseYear = 1980,
                Budget = "$80kk"
            });

            context.Movies.AddOrUpdate(x => x.Name, new Movie
            {
                Name = "Star Wars 5",
                ReleaseYear = 1983,
                Budget = "$90kk"
            });

            context.Movies.AddOrUpdate(x => x.Name, new Movie
            {
                Name = "Star Wars 6",
                ReleaseYear = 1985,
                Budget = "$100kk"
            });

            context.Actors.AddOrUpdate(x => x.LastName, new Actor
            {
                FirstName = "Brad",
                LastName = "Pit",
                BirthDay = DateTime.Parse("1963-12-18"),
                DeathDay = DateTime.Parse("1900-1-1"),
                Gender = "Male",
                HasOskar = false
            });

            context.Actors.AddOrUpdate(x => x.LastName, new Actor
            {
                FirstName = "Harrison",
                LastName = "Ford",
                BirthDay = DateTime.Parse("1942-7-13"),
                DeathDay = DateTime.Parse("1900-1-1"),
                Gender = "Male",
                HasOskar = true
            });
        }
    }
}
