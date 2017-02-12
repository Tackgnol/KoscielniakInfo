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
    public class ScreenShotsController : Controller
    {
        private CVContext db = new CVContext();

        // GET: ScreenShots
        public async Task<ActionResult> Index()
        {
            return View(await db.ScreenShots.ToListAsync());
        }

        // GET: ScreenShots/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreenShot screenShot = await db.ScreenShots.FindAsync(id);
            if (screenShot == null)
            {
                return HttpNotFound();
            }
            return View(screenShot);
        }

        // GET: ScreenShots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ScreenShots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Description,Type,ImageURL,Sorting")] ScreenShot screenShot)
        {
            if (ModelState.IsValid)
            {
                db.ScreenShots.Add(screenShot);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(screenShot);
        }

        // GET: ScreenShots/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreenShot screenShot = await db.ScreenShots.FindAsync(id);
            if (screenShot == null)
            {
                return HttpNotFound();
            }
            return View(screenShot);
        }

        // POST: ScreenShots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Description,Type,ImageURL,Sorting")] ScreenShot screenShot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(screenShot).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(screenShot);
        }

        // GET: ScreenShots/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScreenShot screenShot = await db.ScreenShots.FindAsync(id);
            if (screenShot == null)
            {
                return HttpNotFound();
            }
            return View(screenShot);
        }

        // POST: ScreenShots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ScreenShot screenShot = await db.ScreenShots.FindAsync(id);
            db.ScreenShots.Remove(screenShot);
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
