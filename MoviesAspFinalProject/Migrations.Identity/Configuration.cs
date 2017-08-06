namespace MoviesAspFinalProject.Migrations.Identity
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesAspFinalProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.Identity";
        }

        protected override void Seed(MoviesAspFinalProject.Models.ApplicationDbContext context)
        {
            // context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole { Name = "Admin" });
            //context.Roles.AddOrUpdate(x => x.Name, new ApplicationRole { Name = "User" });

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
