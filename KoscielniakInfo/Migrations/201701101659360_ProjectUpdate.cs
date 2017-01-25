namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project", "Job_id", "dbo.Job");
            DropForeignKey("dbo.Project", "School_Id", "dbo.School");
            DropIndex("dbo.Project", new[] { "Job_id" });
            DropIndex("dbo.Project", new[] { "School_Id" });
            RenameColumn(table: "dbo.Project", name: "Job_id", newName: "JobId");
            RenameColumn(table: "dbo.Project", name: "School_Id", newName: "SchoolId");
            AlterColumn("dbo.Project", "JobId", c => c.Int(nullable: false));
            AlterColumn("dbo.Project", "SchoolId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project", "SchoolId");
            CreateIndex("dbo.Project", "JobId");
            AddForeignKey("dbo.Project", "JobId", "dbo.Job", "id", cascadeDelete: true);
            AddForeignKey("dbo.Project", "SchoolId", "dbo.School", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "SchoolId", "dbo.School");
            DropForeignKey("dbo.Project", "JobId", "dbo.Job");
            DropIndex("dbo.Project", new[] { "JobId" });
            DropIndex("dbo.Project", new[] { "SchoolId" });
            AlterColumn("dbo.Project", "SchoolId", c => c.Int());
            AlterColumn("dbo.Project", "JobId", c => c.Int());
            RenameColumn(table: "dbo.Project", name: "SchoolId", newName: "School_Id");
            RenameColumn(table: "dbo.Project", name: "JobId", newName: "Job_id");
            CreateIndex("dbo.Project", "School_Id");
            CreateIndex("dbo.Project", "Job_id");
            AddForeignKey("dbo.Project", "School_Id", "dbo.School", "Id");
            AddForeignKey("dbo.Project", "Job_id", "dbo.Job", "id");
        }
    }
}
