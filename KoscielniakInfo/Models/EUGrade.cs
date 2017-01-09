using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class EUGrade
    {
        public EUGrade()
        {
            Schools = new List<School>();
        }
        public int Id { get; set; }
        public double Grade { get; set; }
        public virtual ICollection<School> Schools { get; set; }
    }
}