namespace MoviesAspFinalProject.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "BirthDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Actors", "DeathDay", c => c.DateTime(nullable: false));
            DropColumn("dbo.Actors", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Actors", "DeathDay");
            DropColumn("dbo.Actors", "BirthDay");
        }
    }
}
