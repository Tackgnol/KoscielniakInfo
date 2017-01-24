namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Certificates1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Certificate", "PhotoID", c => c.Int());
            AddColumn("dbo.Photo", "CertificateID", c => c.Int());
            CreateIndex("dbo.Photo", "CertificateID");
            AddForeignKey("dbo.Photo", "CertificateID", "dbo.Certificate", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photo", "CertificateID", "dbo.Certificate");
            DropIndex("dbo.Photo", new[] { "CertificateID" });
            DropColumn("dbo.Photo", "CertificateID");
            DropColumn("dbo.Certificate", "PhotoID");
        }
    }
}
