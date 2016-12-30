using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.DAL
{
    public class Exprience
    {
        public string companyName { get; set; }
        public string jobRole { get; set; }
        public string jobPosition { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string wikipediaCompanyName { get; set; }
        public string jobDescription { get; set; }
        
    }
}