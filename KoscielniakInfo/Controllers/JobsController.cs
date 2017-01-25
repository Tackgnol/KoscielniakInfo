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
            var job = new Job();
            job.Projects = new List<Project>();
            PopulateNullProjects(job);
            PopulateTakenProjects(job);
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CompanyName,Role,Position,StartDate,EndDate,WikipediaCompanyName,Description")] Job job, string[] selectedProjects)
        {
            if (selectedProjects != null)
            {
                job.Projects = new List<Project>();
                foreach (var project in selectedProjects)
                {
                    var projectToAdd =await db.Projects.FindAsync(int.Parse(project));
                    job.Projects.Add(projectToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateNullProjects(job);
            PopulateTakenProjects(job);
            
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
            PopulateAssignedProjects(job);
            PopulateNullProjects(job);
            PopulateTakenProjects(job);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, string[] selectedProjects)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var jobToUpdate = db.Jobs
                .Include(p => p.Projects)
                .Where(p => p.id == id)
                .Single();

            // "id,CompanyName,Role,Position,StartDate,EndDate,WikipediaCompanyName,Description"
            if (TryUpdateModel(jobToUpdate, "",new string[] { "CompanyName", "Role", "Position", "StartDate", "EndDate", "WikipediaCompanyName", "Description" }))
            {
                UpdateJobProjects(selectedProjects, jobToUpdate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateTakenProjects(jobToUpdate);
            PopulateNullProjects(jobToUpdate);
            return View(jobToUpdate);
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

        private void PopulateTakenProjects(Job job)
        {
            var allProjects = db.Projects;
            var hashJobProjects = new HashSet<int>(job.Projects.Select(p => p.Id));
            var jobProjects = from p in allProjects
                                 where p.SchoolID != job.id && p.SchoolID != null
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
        private void UpdateJobProjects(string[] selectedProjects, Job jobToUpdate)
        {
            if (selectedProjects == null)
            {
                jobToUpdate.Projects = new List<Project>();
                return;
            }
            var selectedProjectsHS = new HashSet<string>(selectedProjects);
            var jobProjects = new HashSet<int>
                (jobToUpdate.Projects.Select(p => p.Id));

            foreach (var project in db.Projects)
            {
                if (selectedProjectsHS.Contains(project.Id.ToString()))
                {
                    if (!jobProjects.Contains(project.Id))
                    {
                        jobToUpdate.Projects.Add(project);
                    }
                }
                else
                {
                    if (jobProjects.Contains(project.Id))
                    {
                        jobToUpdate.Projects.Remove(project);
                    }
                }

            }

        }
    }

    
}
