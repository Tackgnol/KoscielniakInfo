namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isCertificate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photo", "isCertificate", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photo", "isCertificate");
        }
    }
}
