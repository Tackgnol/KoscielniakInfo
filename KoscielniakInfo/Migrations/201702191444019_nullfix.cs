namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry");
            DropIndex("dbo.ScreenShot", new[] { "PortfolioEntryID" });
            AlterColumn("dbo.ScreenShot", "PortfolioEntryID", c => c.Int());
            CreateIndex("dbo.ScreenShot", "PortfolioEntryID");
            AddForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry");
            DropIndex("dbo.ScreenShot", new[] { "PortfolioEntryID" });
            AlterColumn("dbo.ScreenShot", "PortfolioEntryID", c => c.Int(nullable: false));
            CreateIndex("dbo.ScreenShot", "PortfolioEntryID");
            AddForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry", "ID", cascadeDelete: true);
        }
    }
}
