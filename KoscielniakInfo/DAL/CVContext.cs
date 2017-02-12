using KoscielniakInfo.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KoscielniakInfo.DAL
{
    public class CVContext : DbContext
    {
        public CVContext() : base("CVContext")
        {

        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<EUGrade> EuGrades { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<PortfolioEntry> Entries { get; set; }
        public DbSet<ScreenShot> ScreenShots { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}