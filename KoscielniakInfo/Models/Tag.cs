using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public int ExpirienceID { get; set; }
        public int EducationID { get; set; }
        public int CertificateID { get; set; }
        public int SkillID { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public virtual Exprience Expirience { get; set; }
        public virtual Education Education { get; set; }
        public virtual Certificate Certificate { get; set; }
        public virtual Skill Skill { get; set; }
     }
}