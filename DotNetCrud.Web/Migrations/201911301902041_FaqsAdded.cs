namespace DotNetCrud.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FaqsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(maxLength: 450),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Question, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Faqs", new[] { "Question" });
            DropTable("dbo.Faqs");
        }
    }
}
