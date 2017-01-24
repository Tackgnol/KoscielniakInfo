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
using KoscielniakInfo.ViewModels;
using System.IO;

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
            var school = new School();
            school.Projects = new List<Project>();
            PopulateNullProjects(school);
            PopulateTakenProjects(school);
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,University,Faculty,Course,Level,EuGradeId,ThesisPromoter,ThesisTitle,From,To")] School school, string[] selectedProjects)
        {
            if (selectedProjects != null)
            {
                school.Projects = new List<Project>();
                foreach (var project in selectedProjects)
                {
                    var projectToAdd = await db.Projects.FindAsync(int.Parse(project));
                    school.Projects.Add(projectToAdd);
                }
            }
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
            PopulateAssignedProjects(school);
            PopulateNullProjects(school);
            PopulateTakenProjects(school);
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
            PopulateAssignedProjects(school);
            PopulateNullProjects(school);
            PopulateTakenProjects(school);
            PopulateGradeDropDownList(school.EuGradeId);

            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, string[] selectedProjects)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var schoolToUpdate = db.Schools
                .Include(p => p.Projects)
                .Where(p => p.Id == id)
                .Single();
            if (TryUpdateModel(schoolToUpdate, "", new string[]
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
                    UpdateSchoolProjects(selectedProjects, schoolToUpdate);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /*dex*/)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateGradeDropDownList(schoolToUpdate.EuGradeId);
            PopulateAssignedProjects(schoolToUpdate);
            PopulateNullProjects(schoolToUpdate);
            PopulateTakenProjects(schoolToUpdate);
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
            var projects = db.Projects
                .Where(d => d.SchoolID == id);

            foreach (var project in projects)
            {
                if (project != null)
                {
                    project.SchoolID = null;
                }
            }
            var photos = db.Photos
            .Where(p => p.SchoolID == id);
            foreach (var photo in photos)
            {
                if (photo != null)
                {
                    photo.SchoolID = null;
                }
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]

        private void PopulateGradeDropDownList(object selectedGrade = null)
        {
            var gradeQuery = from g in db.EuGrades
                             orderby g.Id
                             select g;
            ViewBag.EuGradeID = new SelectList(gradeQuery, "ID", "Grade", selectedGrade);
        }
        private void PopulateAssignedProjects(School school)
        {
            var allProjects = db.Projects;
            var hashSchoolProjects = new HashSet<int>(school.Projects.Select(p => p.Id));
            var schoolProjects = from p in allProjects
                                 where p.SchoolID == school.Id
                                 select p;
            var viewModel = new List<SelectedProject>();

            foreach (var project in schoolProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashSchoolProjects.Contains(project.Id)
                });
            }
            ViewBag.AssignedProjects = viewModel;
        }
        private void PopulateNullProjects(School school)
        {
            var allProjects = db.Projects;
            var hashSchoolProjects = new HashSet<int>(school.Projects.Select(p => p.Id));
            var schoolProjects = from p in allProjects
                                 where p.SchoolID == null
                                 select p;
            var viewModel = new List<SelectedProject>();
            foreach (var project in schoolProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashSchoolProjects.Contains(project.Id)
                });
            }

            ViewBag.FreeProjects = viewModel;
        }

        private void PopulateTakenProjects(School school)
        {
            var allProjects = db.Projects;
            var hashSchoolProjects = new HashSet<int>(school.Projects.Select(p => p.Id));
            var schoolProjects = from p in allProjects
                                 where p.SchoolID != school.Id && p.SchoolID != null
                                 select p;
            var viewModel = new List<SelectedProject>();
            foreach (var project in schoolProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashSchoolProjects.Contains(project.Id)
                });
            }
            ViewBag.OtherProjects = viewModel;
        }

        private void UpdateSchoolProjects(string[] selectedProjects, School schoolToUpdate)
        {
            if (selectedProjects == null)
            {
                schoolToUpdate.Projects = new List<Project>();
                return;
            }
            var selectedProjectsHS = new HashSet<string>(selectedProjects);
            var schoolProjects = new HashSet<int>
                (schoolToUpdate.Projects.Select(p => p.Id));
            foreach (var project in db.Projects)
            {
                if (selectedProjectsHS.Contains(project.Id.ToString()))
                {
                    if (!schoolProjects.Contains(project.Id))
                    {
                        schoolToUpdate.Projects.Add(project);
                    }
                }
                else
                {
                    if (schoolProjects.Contains(project.Id))
                    {
                        schoolToUpdate.Projects.Remove(project);
                    }
                }
            }
        }
    }


}


