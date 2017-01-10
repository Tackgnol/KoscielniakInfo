using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoscielniakInfo.ViewModels
{
    public class SelectedProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Selected { get; set; }
    }
}