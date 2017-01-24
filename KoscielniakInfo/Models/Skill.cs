using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public enum Level { Advanced, Intermediate, Begginer }
    public class Skill
    {
        
        [Display(Name="Skill")]
        public int Id { get; set; }
        [Display(Name ="Skill Group")]
        public string SkillGroup { get; set; }
        [Display(Name ="Skill Name")]
        public string Name { get; set; }
        [Display(Name ="Level")]
        public Level SkillLevel { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}