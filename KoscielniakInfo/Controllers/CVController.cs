using KoscielniakInfo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KoscielniakInfo.ViewModels;
using System.Web.Mvc;
using KoscielniakInfo.Models;

namespace KoscielniakInfo.Controllers
{
    public class CVController : Controller
    {
        // GET: CV
        private CVContext db = new CVContext();
        public ActionResult Index()
        {
            CV allTypes = new CV();
            allTypes.Jobs = db.Jobs.ToList();
            allTypes.Schools = db.Schools.ToList();
            allTypes.Certificates = db.Certificates.ToList();
            allTypes.Hobbies = db.Hobbies.ToList();
            Dictionary<string, List<Skill>> skillDictionary =db.Skills
            .GroupBy(s => s.SkillGroup)
            .ToDictionary(g => g.Key, g => g.OrderBy(v => (int)v.SkillLevel).ToList());
            ViewBag.GroupedSkills = skillDictionary;
            return View(allTypes);
        }


    }
}
