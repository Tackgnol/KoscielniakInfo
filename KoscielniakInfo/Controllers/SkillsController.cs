using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KoscielniakInfo.DAL;
using KoscielniakInfo.Models;

namespace KoscielniakInfo.Controllers
{
    public class SkillsController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Skills
        public async Task<ActionResult> Index()
        {


            Dictionary<string, List<Skill>> skillDictionary = await db.Skills
                .GroupBy(s => s.SkillGroup)
                .ToDictionaryAsync(g => g.Key, g => g.OrderBy(v=>(int)v.SkillLevel).ToList());
            
            ViewBag.GroupedSkills = skillDictionary;                   
            return View();
        }
        [Authorize]
        public async Task<ActionResult> Admin()
        {
            return View(await db.Skills.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Skills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SkillGroup,Name,SkillLevel")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Skills.Add(skill);
                await db.SaveChangesAsync();
                return RedirectToAction("Admin");
            }

            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SkillGroup,Name,SkillLevel")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skill).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = await db.Skills.FindAsync(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Skill skill = await db.Skills.FindAsync(id);
            db.Skills.Remove(skill);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
