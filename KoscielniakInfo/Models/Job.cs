
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KoscielniakInfo.Models
{
    public class Job
    {
        public int id { get; set; }
        [Display(Name="Company Name")]

        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string Position { get; set; }
        [Display(Name ="From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "To")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public string WikipediaCompanyName { get; set; }
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public virtual ICollection<Project> Projects {get; set;}
        public virtual ICollection<Photo> Photos { get; set; }

        public string DisplayName
        {
            get
            {
                return CompanyName + " - " + Position;
            }
        }
        
    }
}