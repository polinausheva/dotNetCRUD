namespace DotNetCrud.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductUniqueLimitedName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 450));
            CreateIndex("dbo.Products", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Products", new[] { "Name" });
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}
