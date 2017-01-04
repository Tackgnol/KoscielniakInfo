using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class School
    {
 
 
        public int Id { get; set; }
        public bool Finished { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public string Course { get; set; }
        public string Level { get; set; }
        public double EuGrade { get; set; }
        public string ThesisPromoter { get; set; }
        public string ThesisTitle { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string USGrade
        {
            get
            {
                string grade = this.EuGrade.ToString();
                if (this.EuGrade == 5)
                {
                    grade = "A";
                }
                else if (this.EuGrade == 4.5)
                {
                    grade = "B+";
                }
                else if (this.EuGrade == 4)
                {
                    grade = "B";
                }
                else if (this.EuGrade == 3.5)
                {
                    grade = "C+";
                }
                else if (this.EuGrade == 3)
                {
                    grade = "C";
                }
                else if (this.EuGrade == 2.5)
                {
                    grade = "D+";
                }
                else if (this.EuGrade == 2)
                {
                    grade = "D";
                }
                return grade;
            }
        }
    }
}