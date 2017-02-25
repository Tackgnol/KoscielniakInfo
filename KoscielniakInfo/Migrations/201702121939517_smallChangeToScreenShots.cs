namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallChangeToScreenShots : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScreenShot", "PortfolioEntry_ID", "dbo.PortfolioEntry");
            DropIndex("dbo.ScreenShot", new[] { "PortfolioEntry_ID" });
            RenameColumn(table: "dbo.ScreenShot", name: "PortfolioEntry_ID", newName: "PortfolioEntryID");
            AlterColumn("dbo.ScreenShot", "PortfolioEntryID", c => c.Int(nullable: false));
            CreateIndex("dbo.ScreenShot", "PortfolioEntryID");
            AddForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScreenShot", "PortfolioEntryID", "dbo.PortfolioEntry");
            DropIndex("dbo.ScreenShot", new[] { "PortfolioEntryID" });
            AlterColumn("dbo.ScreenShot", "PortfolioEntryID", c => c.Int());
            RenameColumn(table: "dbo.ScreenShot", name: "PortfolioEntryID", newName: "PortfolioEntry_ID");
            CreateIndex("dbo.ScreenShot", "PortfolioEntry_ID");
            AddForeignKey("dbo.ScreenShot", "PortfolioEntry_ID", "dbo.PortfolioEntry", "ID");
        }
    }
}
