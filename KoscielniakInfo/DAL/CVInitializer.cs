using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KoscielniakInfo.Models;

namespace KoscielniakInfo.DAL
{
    public class CVInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CVContext>
    {
        protected override void Seed(CVContext context)
        {
            var jobs = new List<Job>
            {
                new Job {CompanyName = "Philips", Role="VBA Developer/Analyst", Position="Procurement Analyst", Description="Writing Macros and making reports", WikipediaCompanyName="Philips", StartDate=DateTime.Parse("2012-06-25"),EndDate=DateTime.Parse("2016-03-31") },
                new Job {CompanyName = "AstraZeneca", Role="Reporting Developer", Position="Global Procurement Specialist", Description="Building Procurement Reporting from ground up", WikipediaCompanyName="AstraZeneca", StartDate=DateTime.Parse("2016-04-01") },
            };

            jobs.ForEach(j => context.Jobs.Add(j));
            context.SaveChanges();

            var schools = new List<School>
            {
                new School {University="Uniwersytet Łódzki", Faculty="Ekonomiczno Socjologiczny", Course="Informatyka i Ekonometria", Level="B.A", ThesisPromoter="Dr. Michał Majsterek", ThesisTitle="Metody modelowania modeli ekonometrycznych", From=DateTime.Parse("2007-09-31"), To=DateTime.Parse("2010-07-01") },
                new School {University="Uniwersytet Łódzki", Faculty="Ekonomiczno Socjologiczny", Course="Informatyka i Ekonometria", Level="Magistry", ThesisPromoter="prof. zw. dr hab Jan J. Sztaudynger", ThesisTitle="„Economical and Demographical Causes of Fertility”", From=DateTime.Parse("2010-09-01"), To=DateTime.Parse("2013-02-01") },
            };

            schools.ForEach(j => context.Schools.Add(j));
            context.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag {JobID=1, SchoolID=1, Text="IT"},
                new Tag {JobID=2, SchoolID=1, Text="Project Management" },
                new Tag {SchoolID=2, Text="Boredom" }
            };

            tags.ForEach(j => context.Tags.Add(j));
            context.SaveChanges();

        }
    }
}