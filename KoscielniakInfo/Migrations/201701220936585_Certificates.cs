namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Certificates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Certificate", "Skill_Id", "dbo.Skill");
            DropIndex("dbo.Certificate", new[] { "Skill_Id" });
            RenameColumn(table: "dbo.Certificate", name: "Skill_Id", newName: "SkillId");
            AlterColumn("dbo.Certificate", "SkillId", c => c.Int(nullable: true));
            CreateIndex("dbo.Certificate", "SkillId");
            AddForeignKey("dbo.Certificate", "SkillId", "dbo.Skill", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certificate", "SkillId", "dbo.Skill");
            DropIndex("dbo.Certificate", new[] { "SkillId" });
            AlterColumn("dbo.Certificate", "SkillId", c => c.Int());
            RenameColumn(table: "dbo.Certificate", name: "SkillId", newName: "Skill_Id");
            CreateIndex("dbo.Certificate", "Skill_Id");
            AddForeignKey("dbo.Certificate", "Skill_Id", "dbo.Skill", "Id");
        }
    }
}
