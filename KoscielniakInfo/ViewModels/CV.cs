using KoscielniakInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.ViewModels
{
    public class CV
    {
        public List<Job> Jobs { get; set; }
        public List<School> Schools { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Certificate> Certificates { get; set; }
        public List<Hobby> Hobbies { get; set; }
    }
}