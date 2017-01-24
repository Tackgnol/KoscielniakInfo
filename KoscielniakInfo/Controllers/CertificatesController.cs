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
    public class CertificatesController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Certificates
        public async Task<ActionResult> Index()
        {
            var certificates = db.Certificates.Include(c => c.Skill);
            return View(await certificates.ToListAsync());
        }

        // GET: Certificates/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // GET: Certificates/Create
        public ActionResult Create()
        {
            ViewBag.SkillID = new SelectList(db.Skills, "Id", "Name");
            return View();
        }

        // POST: Certificates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,TeacherName,TeacherURL,CompanyName,CompanyURL,StartDate,EndDate,SkillID")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Certificates.Add(certificate);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SkillID = new SelectList(db.Skills, "Id", "Name", certificate.SkillID);
            return View(certificate);
        }

        // GET: Certificates/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            ViewBag.SkillID = new SelectList(db.Skills, "Id", "Name", certificate.SkillID);
            return View(certificate);
        }

        // POST: Certificates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,TeacherName,TeacherURL,CompanyName,CompanyURL,StartDate,EndDate,SkillID")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificate).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.SkillID = new SelectList(db.Skills, "Id", "Name", certificate.SkillID);
            return View(certificate);
        }

        // GET: Certificates/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = await db.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // POST: Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Certificate certificate = await db.Certificates.FindAsync(id);
            db.Certificates.Remove(certificate);
            var photos = db.Photos
                .Where(p => p.CertificateID == id);
            foreach (var photo in photos)
            {
                if (photo!=null)
                {
                    photo.CertificateID = null;
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
    }
}
