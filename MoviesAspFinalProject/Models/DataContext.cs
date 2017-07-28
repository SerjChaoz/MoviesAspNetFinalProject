namespace MoviesAspFinalProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IdentityUserClaim>();
            modelBuilder.Ignore<IdentityUserLogin>();
            modelBuilder.Ignore<IdentityUserRole>();

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Actors)
                .WithRequired(e => e.Movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Actor>()
                .HasMany(e => e.Movies)
                .WithRequired(e => e.Actor)
                .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Rating>()
                .Property(e => e.MovieRating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Rating>()
                .HasRequired(x => x.User)
                .WithMany(c => c.Ratings)
                .Map(m => m.MapKey("Id"))
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Rating>()
                .HasRequired(x => x.Movie)
                .WithMany(c => c.Ratings)
                .WillCascadeOnDelete(true);
        }

        public System.Data.Entity.DbSet<MoviesAspFinalProject.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}
