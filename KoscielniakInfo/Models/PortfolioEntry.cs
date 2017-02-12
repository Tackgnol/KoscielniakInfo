using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class PortfolioEntry
    {
        public int ID { get; set; }
        [Display(Name="Project Name")]
        public string Name { get; set; }
        [Display(Name="Created for")]
        public string Client { get; set; }
        public string Description { get; set; }
        [Display(Name ="Started at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        [Display(Name ="Ended by")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
        [Display(Name ="Skills used")]
        public string Category { get; set; }
        [Display(Name ="GitHub Link")]
        public string GitHubLink { get; set; }
        [Display(Name ="Solution Screenshots")]
        public virtual ICollection<ScreenShot> ScreenShots { get; set; }
    }
}