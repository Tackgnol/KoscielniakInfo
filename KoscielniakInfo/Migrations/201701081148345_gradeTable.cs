namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gradeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EUGrade",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.School", "EuGrade_Id", c => c.Int());
            CreateIndex("dbo.School", "EuGrade_Id");
            AddForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade", "Id");
            DropColumn("dbo.School", "EuGrade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.School", "EuGrade", c => c.Double(nullable: false));
            DropForeignKey("dbo.School", "EuGrade_Id", "dbo.EUGrade");
            DropIndex("dbo.School", new[] { "EuGrade_Id" });
            DropColumn("dbo.School", "EuGrade_Id");
            DropTable("dbo.EUGrade");
        }
    }
}
