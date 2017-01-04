using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public int JobID { get; set; }
        public int SchoolID { get; set; }
        public int CertificateID { get; set; }
        public int SkillID { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public virtual Job Job { get; set; }
        public virtual School School { get; set; }
        public virtual Certificate Certificate { get; set; }
        public virtual Skill Skill { get; set; }
     }
}