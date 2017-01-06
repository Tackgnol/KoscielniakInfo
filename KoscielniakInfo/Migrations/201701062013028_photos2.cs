namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photos2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Photo", "HobbyID", "dbo.Hobby");
            DropForeignKey("dbo.Photo", "JobID", "dbo.Job");
            DropForeignKey("dbo.Photo", "SchoolID", "dbo.School");
            DropIndex("dbo.Photo", new[] { "JobID" });
            DropIndex("dbo.Photo", new[] { "SchoolID" });
            DropIndex("dbo.Photo", new[] { "HobbyID" });
            AlterColumn("dbo.Photo", "JobID", c => c.Int());
            AlterColumn("dbo.Photo", "SchoolID", c => c.Int());
            AlterColumn("dbo.Photo", "HobbyID", c => c.Int());
            AlterColumn("dbo.Photo", "Sorting", c => c.Int());
            CreateIndex("dbo.Photo", "JobID");
            CreateIndex("dbo.Photo", "SchoolID");
            CreateIndex("dbo.Photo", "HobbyID");
            AddForeignKey("dbo.Photo", "HobbyID", "dbo.Hobby", "Id");
            AddForeignKey("dbo.Photo", "JobID", "dbo.Job", "id");
            AddForeignKey("dbo.Photo", "SchoolID", "dbo.School", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "SchoolID", "dbo.School");
            DropForeignKey("dbo.Photo", "JobID", "dbo.Job");
            DropForeignKey("dbo.Photo", "HobbyID", "dbo.Hobby");
            DropIndex("dbo.Photo", new[] { "HobbyID" });
            DropIndex("dbo.Photo", new[] { "SchoolID" });
            DropIndex("dbo.Photo", new[] { "JobID" });
            AlterColumn("dbo.Photo", "Sorting", c => c.Int(nullable: false));
            AlterColumn("dbo.Photo", "HobbyID", c => c.Int(nullable: false));
            AlterColumn("dbo.Photo", "SchoolID", c => c.Int(nullable: false));
            AlterColumn("dbo.Photo", "JobID", c => c.Int(nullable: false));
            CreateIndex("dbo.Photo", "HobbyID");
            CreateIndex("dbo.Photo", "SchoolID");
            CreateIndex("dbo.Photo", "JobID");
            AddForeignKey("dbo.Photo", "SchoolID", "dbo.School", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Photo", "JobID", "dbo.Job", "id", cascadeDelete: true);
            AddForeignKey("dbo.Photo", "HobbyID", "dbo.Hobby", "Id", cascadeDelete: true);
        }
    }
}
