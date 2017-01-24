namespace KoscielniakInfo.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KoscielniakInfo.DAL.CVContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "KoscielniakInfo.DAL.CVContext";
        }

        protected override void Seed(KoscielniakInfo.DAL.CVContext context)
        {
            var grades = new List<EUGrade>
            {
                new EUGrade {Grade=5 },
                new EUGrade {Grade=4.5 },
                new EUGrade {Grade=4 },
                new EUGrade {Grade=3.5 },
                new EUGrade {Grade=3 },
                new EUGrade {Grade=2.5 },
                new EUGrade {Grade=2 }
            };
            grades.ForEach(s => context.EuGrades.AddOrUpdate(s));
            context.SaveChanges();

            var schools = new List<School>
            {
                new School {University="University of Łódź", Faculty="Faculty of Economics and Sociology", Course="IT and Econometrics", Level="B.A", ThesisPromoter="prof. nadzw. UŁ, dr hab. Michał Majsterek", ThesisTitle="Methods of Building and Verifying Econometric Models", From=DateTime.Parse("2007-09-01"), To=DateTime.Parse("2010-07-05"), EuGradeId=29},
                new School {University="University of Łódź", Faculty="Faculty of Economics and Sociology", Course="IT and Econometrics", Level="Masters", ThesisPromoter="prof. zw. dr hab Jan J. Sztaudynger", ThesisTitle="Economical and Demographical Causes of Fertility", From=DateTime.Parse("2010-09-01"), To=DateTime.Parse("2013-02-13"),EuGradeId=30},
            };
            schools.ForEach(s => context.Schools.AddOrUpdate(s));
            var jobs = new List<Job>
            {
                new Job { CompanyName = "Astra-Zeneca Pharma Poland", Role="Reporting Developer", Position="Procurement analyst – Global Procurement Services Analytics Warsaw", StartDate=DateTime.Parse("2016-04-01"), Description="Automation of existing processes, Creating new reports on internal customer demand(KPIs, Dashboards, Analytics), Managing the transition from Excel to a BI platform, Continuous improvement of reporting, data delivery and quality", WikipediaCompanyName="AstraZeneca" },
                new Job { CompanyName = "Philips Lighting Polska", Role="Reporoting Developer", Position="Procurement Analyst", Description = "Report design and automation, IT project management and rollouts, UAT of new solutions, Performing all required reporting and trend analysis, Maintaining related documentation", StartDate=DateTime.Parse("2016-02-01"), EndDate=DateTime.Parse("2016-03-31"), WikipediaCompanyName="Philips"}
            };
            jobs.ForEach(j => context.Jobs.AddOrUpdate(j));

            context.SaveChanges();
            var projects = new List<Project>
            {
                new Project {Name="Study budgeting Improvement", Role="Developer / Analytics support", Description="Redesigning the Request for Proposal (RfP) templates for the clinical research operatives, then building an automated consolidation and an analytics layer.",  Outcome="Tool rolled out to Procurement", JobId=12 },
                new Project {Name="PEx – Philips Excel Add-in", Role="Software analyst", Description="Building an MS Excel Add-in to improve the everyday Excel workload within Philips.",  Outcome="The project won the audience prize in a local Business Improvement Competition", JobId=11 },
                new Project {Name="MEC Improvement Project", Role="Project Lead", Description="Recreation of the Month End Closing process from excel files sent around via e-mail to one cross company collecting tool",  Outcome="Success, a Access Web App has been deployed to a SharePoint Site collecting savings data", JobId=11 }
            };
            projects.ForEach(p => context.Projects.AddOrUpdate(p));

            var hobbies = new List<Hobby>
            {
                new Hobby { Title = "Lifting Weights", Description = "I started going to the Gym in 2013 back then I did not know what I was doing so the results were minimal but still they gave me great joy a lot has changes since then" },
                new Hobby { Title = "Board Games", Description = "I love board games, mostly cooperative ones, especially in a Cult of Cthulu theme" },
                new Hobby { Title ="Video Games", Description="Somewhat of a guilty pleasure, over the years I have become a bit de-sencetized in this field meaning that most of them bore me (beem there, done that situation), but still a piece of excellence such as the Dark Souls series will catch me for hours" },
                new Hobby { Title= "Reading", Description="Something I wish I did more often in spare of my other Hobbies, but most of my reading is either Fanthasy Novels or Comic Books, which I find no shame in" },
                new Hobby { Title = "Music", Description = "Treated more often then not as a side hobby, but I LOVE MUSIC, when it comes to Genres I stray more towards the rock/metal side" }

            };
            hobbies.ForEach(h => context.Hobbies.AddOrUpdate(h));
            var photos = new List<Photo>
            {
                new Photo { Title="IM-Tech Anniversary", Description="Photo taken for our 10 Year anniversary with the faculty",  Sorting=1, URL="CVImage00000.jpg", SchoolID=4 },
                new Photo { Title="2012 Conference", Description="Photo taken at the last day of the student conference",  Sorting=1, URL="CVImage00001.jpg", SchoolID=5  },
                new Photo { Title="PEX Presentation", Description="Me presenting our project during the Philips Business Improvement Competition", Sorting=1, URL="CVImage00002.jpg", JobID=12 }
            };
            photos.ForEach(p => context.Photos.AddOrUpdate(p));
            context.SaveChanges();
            
        }
    }
}
