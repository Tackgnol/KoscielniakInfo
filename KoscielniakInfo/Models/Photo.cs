using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        public int ExpirienceID { get; set; }
        public int EducationID { get; set; }
        public int HobbyID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Sorting { get; set; }
        public string URL { get; set; }

        public virtual Job Expirience { get; set; }
        public virtual School Education { get; set; }
        public virtual Hobby Hobby { get; set; }
    }
}