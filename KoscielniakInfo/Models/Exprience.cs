
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Exprience
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string JobRole { get; set; }
        public string JobPosition { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string WikipediaCompanyName { get; set; }
        public string JobDescription { get; set; }
        public virtual ICollection<Project> Projects {get; set;}
        
    }
}