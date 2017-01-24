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
using KoscielniakInfo.ViewModels;

namespace KoscielniakInfo.Controllers
{
    public class JobsController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Jobs
        public async Task<ActionResult> Index()
        {
            return View(await db.Jobs.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CompanyName,Role,Position,StartDate,EndDate,WikipediaCompanyName,Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CompanyName,Role,Position,StartDate,EndDate,WikipediaCompanyName,Description")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            db.Jobs.Remove(job);
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
        private void PopulateAssignedProjects(Job job)
        {
            var allProjects = db.Projects;
            var hashjobProjects = new HashSet<int>(job.Projects.Select(p => p.Id));
            var jobProjects = from p in allProjects
                                 where p.SchoolID == job.id
                                 select p;
            var viewModel = new List<SelectedProject>();

            foreach (var project in jobProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashjobProjects.Contains(project.Id)
                });
            }
            ViewBag.AssignedProjects = viewModel;
        }
        private void PopulateNullProjects(Job job)
        {
            var allProjects = db.Projects;
            var hashJobProjects = new HashSet<int>(job.Projects.Select(p => p.Id));
            var jobProjects = from p in allProjects
                                 where p.SchoolID == null
                                 select p;
            var viewModel = new List<SelectedProject>();
            foreach (var project in jobProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashJobProjects.Contains(project.Id)
                });
            }

            ViewBag.FreeProjects = viewModel;
        }

        private void PopulateTakenProjects(School school)
        {
            var allProjects = db.Projects;
            var hashJobProjects = new HashSet<int>(school.Projects.Select(p => p.Id));
            var jobProjects = from p in allProjects
                                 where p.SchoolID != school.Id && p.SchoolID != null
                                 select p;
            var viewModel = new List<SelectedProject>();
            foreach (var project in jobProjects)
            {
                viewModel.Add(new SelectedProject
                {
                    Id = project.Id,
                    Title = project.Name,
                    Selected = hashJobProjects.Contains(project.Id)
                });
            }
            ViewBag.OtherProjects = viewModel;
        }
    }
}
