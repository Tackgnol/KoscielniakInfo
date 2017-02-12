namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Portfolio : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.PortfolioEntry",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Client = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        UsedSkills = c.String(),
                        GitHubLink = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ScreenShot",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        ImageURL = c.String(),
                        Sorting = c.Int(nullable: false),
                        PortfolioEntry_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PortfolioEntry", t => t.PortfolioEntry_ID)
                .Index(t => t.PortfolioEntry_ID);
            
 
            
        }
        
        public override void Down()
        {

            DropForeignKey("dbo.ScreenShot", "PortfolioEntry_ID", "dbo.PortfolioEntry");

            DropIndex("dbo.ScreenShot", new[] { "PortfolioEntry_ID" });

            DropTable("dbo.ScreenShot");
            DropTable("dbo.PortfolioEntry");

        }
    }
}
