using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string SkillGroup { get; set; }
        public string Name { get; set; }
        public string SkillLevel { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }

    }
}