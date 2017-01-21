
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Job
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WikipediaCompanyName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Project> Projects {get; set;}

        public string DisplayName
        {
            get
            {
                return CompanyName + " - " + Position;
            }
        }
        
    }
}