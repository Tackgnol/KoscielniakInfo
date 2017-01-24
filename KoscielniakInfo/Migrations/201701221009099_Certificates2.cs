namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Certificates2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificate", "TeacherURL", c => c.String());
            AddColumn("dbo.Certificate", "CompanyURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Certificate", "CompanyURL");
            DropColumn("dbo.Certificate", "TeacherURL");
        }
    }
}
