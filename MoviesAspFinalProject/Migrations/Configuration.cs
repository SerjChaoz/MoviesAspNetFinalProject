namespace MoviesAspFinalProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesAspFinalProject.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesAspFinalProject.Models.DataContext context)
        {
            context.Actors.Add(new Models.Actor { FirstName = "Tom", LastName = "Crouse" });
            context.Actors.Add(new Models.Actor { FirstName = "Billy", LastName = "Bobstervue" });
            context.Actors.Add(new Models.Actor { FirstName = "Bam", LastName = "Tadam" });

            context.Movies.Add(new Models.Movie { Name = "American Pie" });
            context.Movies.Add(new Models.Movie { Name = "Mask" });
            context.Movies.Add(new Models.Movie { Name = "Alice in Wonderland" });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
