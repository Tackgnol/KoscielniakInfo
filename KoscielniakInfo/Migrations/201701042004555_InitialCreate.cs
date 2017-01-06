namespace KoscielniakInfo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherName = c.String(),
                        CompanyName = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Skill_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skill", t => t.Skill_Id)
                .Index(t => t.Skill_Id);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Role = c.String(),
                        Position = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        WikipediaCompanyName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                        Description = c.String(),
                        Outcome = c.String(),
                        PortfolioLink = c.String(),
                        Job_id = c.Int(),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Job", t => t.Job_id)
                .ForeignKey("dbo.School", t => t.School_Id)
                .Index(t => t.Job_id)
                .Index(t => t.School_Id);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        ExpirienceID = c.Int(nullable: false),
                        EducationID = c.Int(nullable: false),
                        HobbyID = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        Sorting = c.Int(nullable: false),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.PhotoID)
                .ForeignKey("dbo.School", t => t.EducationID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.ExpirienceID, cascadeDelete: true)
                .ForeignKey("dbo.Hobby", t => t.HobbyID, cascadeDelete: true)
                .Index(t => t.ExpirienceID)
                .Index(t => t.EducationID)
                .Index(t => t.HobbyID);
            
            CreateTable(
                "dbo.School",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Finished = c.Boolean(nullable: false),
                        University = c.String(),
                        Faculty = c.String(),
                        Course = c.String(),
                        Level = c.String(),
                        EuGrade = c.Double(nullable: false),
                        ThesisPromoter = c.String(),
                        ThesisTitle = c.String(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hobby",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillGroup = c.String(),
                        Name = c.String(),
                        SkillLevel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        SchoolID = c.Int(nullable: false),
                        CertificateID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                        Text = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Certificate", t => t.CertificateID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.School", t => t.SchoolID, cascadeDelete: true)
                .ForeignKey("dbo.Skill", t => t.SkillID, cascadeDelete: true)
                .Index(t => t.JobID)
                .Index(t => t.SchoolID)
                .Index(t => t.CertificateID)
                .Index(t => t.SkillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tag", "SkillID", "dbo.Skill");
            DropForeignKey("dbo.Tag", "SchoolID", "dbo.School");
            DropForeignKey("dbo.Tag", "JobID", "dbo.Job");
            DropForeignKey("dbo.Tag", "CertificateID", "dbo.Certificate");
            DropForeignKey("dbo.Certificate", "Skill_Id", "dbo.Skill");
            DropForeignKey("dbo.Photo", "HobbyID", "dbo.Hobby");
            DropForeignKey("dbo.Photo", "ExpirienceID", "dbo.Job");
            DropForeignKey("dbo.Photo", "EducationID", "dbo.School");
            DropForeignKey("dbo.Project", "School_Id", "dbo.School");
            DropForeignKey("dbo.Project", "Job_id", "dbo.Job");
            DropIndex("dbo.Tag", new[] { "SkillID" });
            DropIndex("dbo.Tag", new[] { "CertificateID" });
            DropIndex("dbo.Tag", new[] { "SchoolID" });
            DropIndex("dbo.Tag", new[] { "JobID" });
            DropIndex("dbo.Photo", new[] { "HobbyID" });
            DropIndex("dbo.Photo", new[] { "EducationID" });
            DropIndex("dbo.Photo", new[] { "ExpirienceID" });
            DropIndex("dbo.Project", new[] { "School_Id" });
            DropIndex("dbo.Project", new[] { "Job_id" });
            DropIndex("dbo.Certificate", new[] { "Skill_Id" });
            DropTable("dbo.Tag");
            DropTable("dbo.Skill");
            DropTable("dbo.Hobby");
            DropTable("dbo.School");
            DropTable("dbo.Photo");
            DropTable("dbo.Project");
            DropTable("dbo.Job");
            DropTable("dbo.Certificate");
        }
    }
}
