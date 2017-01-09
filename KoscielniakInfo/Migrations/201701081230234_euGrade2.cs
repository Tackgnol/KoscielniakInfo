namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class euGrade2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade");
            DropIndex("dbo.School", new[] { "EuGrade_Id" });
            RenameColumn(table: "dbo.School", name: "EuGrade_Id", newName: "EuGradeId");
            AlterColumn("dbo.School", "EuGradeId", c => c.Int(nullable: false));
            CreateIndex("dbo.School", "EuGradeId");
            AddForeignKey("dbo.School", "EuGradeId", "dbo.EUGrade", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.School", "EuGradeId", "dbo.EUGrade");
            DropIndex("dbo.School", new[] { "EuGradeId" });
            AlterColumn("dbo.School", "EuGradeId", c => c.Int());
            RenameColumn(table: "dbo.School", name: "EuGradeId", newName: "EuGrade_Id");
            CreateIndex("dbo.School", "EuGrade_Id");
            AddForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade", "Id");
        }
    }
}
