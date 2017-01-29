using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Project
    {

        public int Id { get; set; }
        [Display(Name="Project Name")]
        public string Name { get; set; }
        [Display(Name = "Project Role")]
        public string Role { get; set; }
        [Display(Name = "Project Description")]
        public string Description { get; set; }
        [Display(Name = "Project Result")]
        public string Outcome { get; set; }
        [Display(Name = "Project Portfolio link")]
        public string PortfolioLink { get; set; }
        [Display(Name = "Linked to School")]
        public int? SchoolID { get; set; }
        [Display(Name = "Linked to Job")]
        public int? JobId { get; set; }
        public virtual School School { get; set; }
        public virtual Job Job { get; set; }

    }
}