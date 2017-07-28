namespace MoviesAspFinalProject.Migrations.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public partial class AlterColumns : DbMigration
    {
        public override void Up()
        {
            List<string> tablenames = new List<string>();
            tablenames.Add("Rating");
            tablenames.Add("Actor");
            tablenames.Add("Movie");
            tablenames.Add("Role");

            foreach (var tablename in tablenames)
            {
                string table = String.Format("dbo.{0}s", tablename);
                string primarykey = String.Format("{0}Id", tablename);
                AlterColumn(table, primarykey, c => c.String(nullable: false, maxLength: 128, defaultValueSql: "newid()"));
                AlterColumn(table, "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "getutcdate()"));
                AlterColumn(table, "EditDate", c => c.DateTime(nullable: false, defaultValueSql: "getutcdate()"));
            }
        }
        
        public override void Down()
        {
        }
    }
}
