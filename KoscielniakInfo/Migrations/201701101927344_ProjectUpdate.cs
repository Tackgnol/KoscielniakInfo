namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Project", name: "Job_id", newName: "JobId");
            RenameColumn(table: "dbo.Project", name: "School_Id", newName: "SchoolID");
            RenameIndex(table: "dbo.Project", name: "IX_School_Id", newName: "IX_SchoolID");
            RenameIndex(table: "dbo.Project", name: "IX_Job_id", newName: "IX_JobId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Project", name: "IX_JobId", newName: "IX_Job_id");
            RenameIndex(table: "dbo.Project", name: "IX_SchoolID", newName: "IX_School_Id");
            RenameColumn(table: "dbo.Project", name: "SchoolID", newName: "School_Id");
            RenameColumn(table: "dbo.Project", name: "JobId", newName: "Job_id");
        }
    }
}
