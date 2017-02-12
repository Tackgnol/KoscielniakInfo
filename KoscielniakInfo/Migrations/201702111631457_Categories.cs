namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.PortfolioEntry", "Category", c => c.String());
            DropColumn("dbo.PortfolioEntry", "UsedSkills");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PortfolioEntry", "UsedSkills", c => c.String());
            DropColumn("dbo.PortfolioEntry", "Category");
            DropTable("dbo.Category");
        }
    }
}
