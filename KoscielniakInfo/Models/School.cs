using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public class School
    {


        public int Id { get; set; }
        [Display(Name = "University Name")]
        public string University { get; set; }
        [Display(Name = "Faculty Name")]
        public string Faculty { get; set; }
        [Display(Name = "Course Name")]
        public string Course { get; set; }
        [Display(Name = "Education Level")]
        public string Level { get; set; }
        [Display(Name = "Thesis Promoter")]
        public string ThesisPromoter { get; set; }
        [Display(Name = "Thesis Title")]
        public string ThesisTitle { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        [Display(Name = "Started from:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [Display(Name = "Ended:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
        public int EuGradeId { get; set; }
        [Display(Name = "European Grade")]
        public virtual EUGrade EuGrade { get; set; }
        [Display(Name = "American Grade")]
        public string USGrade
        {
            get
            {
                double euGrade = this.EuGrade.Grade;
                string usGrade = euGrade.ToString();
                if (euGrade == 5)
                {
                   usGrade = "A";
                }
                else if (euGrade == 4.5)
                {
                    usGrade = "B+";
                }
                else if (euGrade == 4)
                {
                    usGrade = "B";
                }
                else if (euGrade == 3.5)
                {
                    usGrade = "C+";
                }
                else if (euGrade == 3)
                {
                    usGrade = "C";
                }
                else if (euGrade == 2.5)
                {
                    usGrade = "D+";
                }
                else if (euGrade == 2)
                {
                    usGrade = "D";
                }
                return usGrade;
            }
        }
        public bool Finished
        {
            get
            {
                if (this.To != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}