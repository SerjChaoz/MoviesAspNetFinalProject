namespace MoviesAspFinalProject.Migrations.DataContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimetoDateTime2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Actors", "BirthDay");
            DropColumn("dbo.Actors", "DeathDay");
            AddColumn("dbo.Actors", "BirthDay", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Actors", "DeathDay", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Actors", "BirthDay");
            DropColumn("dbo.Actors", "DeathDay");
            AddColumn("dbo.Actors", "DeathDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Actors", "BirthDay", c => c.DateTime(nullable: false));
        }
    }
}
