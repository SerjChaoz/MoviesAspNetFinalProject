namespace MoviesAspFinalProject.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 250),
                        LastName = c.String(nullable: false, maxLength: 250),
                        Age = c.Int(nullable: false),
                        Gender = c.String(nullable: false, maxLength: 128),
                        HasOskar = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        RoleName = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        MovieId = c.String(nullable: false, maxLength: 128),
                        ActorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Movies", t => t.MovieId)
                .ForeignKey("dbo.Actors", t => t.ActorId)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 250),
                        ReleaseYear = c.Int(nullable: false),
                        Budget = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.String(nullable: false, maxLength: 128),
                        MovieRating = c.Decimal(nullable: false, precision: 18, scale: 0),
                        CreateDate = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);    
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Ratings", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Roles", "MovieId", "dbo.Movies");
            DropIndex("dbo.Ratings", new[] { "MovieId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "ActorId" });
            DropIndex("dbo.Roles", new[] { "MovieId" });
            DropTable("dbo.Ratings");
            DropTable("dbo.Movies");
            DropTable("dbo.Roles");
            DropTable("dbo.Actors");
        }
    }
}
