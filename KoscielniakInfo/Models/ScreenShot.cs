using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.Models
{
    public enum ScreenShotType
    {
        [Display(Name = "Use case Diagram")]
        UseCase,
        [Display(Name = "Behaviour Diagram")]
        Behavior,
        [Display(Name ="Class Diagram")]
        ClassStructure,
        [Display(Name ="Main View")]
        MainPage,
        [Display(Name = "Additional View")]
        SomthingCool
    }
    public class ScreenShot
    {
        public int ID { get; set; }
        public string Description { get; set; }
        [Display(Name="Screenshot Type")]
        public ScreenShotType Type { get; set; }
        public string ImageURL { get; set; }
        public int Sorting { get; set; }
    }
}