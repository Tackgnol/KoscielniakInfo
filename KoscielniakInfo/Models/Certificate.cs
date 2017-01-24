using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Certificate
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name="Course Teacher")]
        public string TeacherName { get; set; }
        [Display(Name="Teachers Homepage")]
        public string TeacherURL { get; set; }
        [Display(Name ="Course Organizer")]
        public string CompanyName { get; set; }
        [Display(Name = "Oranizer URL")]
        public string CompanyURL { get; set; }
        [Display(Name ="Started At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Display(Name ="Finished At")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        [Display(Name ="Certificate for skill")]
        public int? SkillID { get; set; }
        [Display(Name = "Certificate for skill")]
        public virtual Skill Skill { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}