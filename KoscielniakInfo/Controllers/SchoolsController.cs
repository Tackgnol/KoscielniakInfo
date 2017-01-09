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
using System.Data.Entity.Infrastructure;

namespace KoscielniakInfo.Controllers
{
    public class SchoolsController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Schools
        public async Task<ActionResult> Index()
        {
            return View(await db.Schools.ToListAsync());
        }

        // GET: Schools/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            PopulateGradeDropDownList();
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,University,Faculty,Course,Level,EuGradeId,ThesisPromoter,ThesisTitle,From,To")] School school)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Schools.Add(school);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateGradeDropDownList(school.EuGradeId);
            return View(school);
        }

        // GET: Schools/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            PopulateGradeDropDownList(school.EuGradeId);
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var schoolToUpdate = await db.Schools.FindAsync(id);
            if (TryUpdateModel(schoolToUpdate, "",new string[]
                {
                    "University",
                    "Faculty",
                    "Course",
                    "Level",
                    "ThesisPromoter",
                    "ThesisTitle",
                    "From",
                    "To",
                    "EuGradeID"
                }))
            {
                try
                {
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateGradeDropDownList(schoolToUpdate.EuGradeId);
            return View(schoolToUpdate);

        }

        // GET: Schools/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = await db.Schools.FindAsync(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            School school = await db.Schools.FindAsync(id);
            db.Schools.Remove(school);
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
        private void PopulateGradeDropDownList(object selectedGrade = null)
        {
            var gradeQuery = from g in db.EuGrades
                             orderby g.Id
                             select g;
            ViewBag.EuGradeID = new SelectList(gradeQuery, "ID", "Grade", selectedGrade);
        }

    }
}
