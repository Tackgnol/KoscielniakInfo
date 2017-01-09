namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finishedChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.School", "Finished");
        }
        
        public override void Down()
        {
            AddColumn("dbo.School", "Finished", c => c.Boolean(nullable: false));
        }
    }
}
