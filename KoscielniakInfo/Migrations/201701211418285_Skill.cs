namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Skill : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Skill", "SkillLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Skill", "SkillLevel", c => c.String());
        }
    }
}
