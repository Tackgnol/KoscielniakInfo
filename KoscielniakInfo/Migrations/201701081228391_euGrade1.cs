namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class euGrade1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.School", "EuGradeId", "dbo.EUGrade");
            DropIndex("dbo.School", new[] { "EuGradeId" });
            RenameColumn(table: "dbo.School", name: "EuGradeId", newName: "EuGrade_Id");
            AlterColumn("dbo.School", "EuGrade_Id", c => c.Int());
            CreateIndex("dbo.School", "EuGrade_Id");
            AddForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade");
            DropIndex("dbo.School", new[] { "EuGrade_Id" });
            AlterColumn("dbo.School", "EuGrade_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.School", name: "EuGrade_Id", newName: "EuGradeId");
            CreateIndex("dbo.School", "EuGradeId");
            AddForeignKey("dbo.School", "EuGradeId", "dbo.EUGrade", "Id", cascadeDelete: true);
        }
    }
}
