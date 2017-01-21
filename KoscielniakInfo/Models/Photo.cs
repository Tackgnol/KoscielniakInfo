using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        [Display(Name ="Assigned To Job")]
        public int? JobID { get; set; }
        [Display(Name = "Assigned To School")]
        public int? SchoolID { get; set; }
        [Display(Name = "Assigned To Hobby")]
        public int? HobbyID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int? Sorting { get; set; }
        public string URL { get; set; }

        public virtual Job Job { get; set; }
        public virtual School School { get; set; }
        public virtual Hobby Hobby { get; set; }
    }
}