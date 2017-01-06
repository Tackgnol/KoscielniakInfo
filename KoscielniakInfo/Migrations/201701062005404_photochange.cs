namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class photochange : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Photo", name: "EducationID", newName: "SchoolID");
            RenameColumn(table: "dbo.Photo", name: "ExpirienceID", newName: "JobID");
            RenameIndex(table: "dbo.Photo", name: "IX_ExpirienceID", newName: "IX_JobID");
            RenameIndex(table: "dbo.Photo", name: "IX_EducationID", newName: "IX_SchoolID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Photo", name: "IX_SchoolID", newName: "IX_EducationID");
            RenameIndex(table: "dbo.Photo", name: "IX_JobID", newName: "IX_ExpirienceID");
            RenameColumn(table: "dbo.Photo", name: "JobID", newName: "ExpirienceID");
            RenameColumn(table: "dbo.Photo", name: "SchoolID", newName: "EducationID");
        }
    }
}
