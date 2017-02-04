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
    public class HobbiesController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Hobbies
        public async Task<ActionResult> Index()
        {
            return View(await db.Hobbies.ToListAsync());
        }
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Admin()
        {
            return View(await db.Hobbies.ToListAsync());
        }

        // GET: Hobbies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = await db.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        // GET: Hobbies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hobbies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Description")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                db.Hobbies.Add(hobby);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hobby);
        }

        // GET: Hobbies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = await db.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        // POST: Hobbies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Description")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hobby).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hobby);
        }

        // GET: Hobbies/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobby hobby = await db.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }

        // POST: Hobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Hobby hobby = await db.Hobbies.FindAsync(id);
            db.Hobbies.Remove(hobby);
            var photos = db.Photos
                        .Where(p => p.HobbyID == id);
            foreach (var photo in photos)
            {
                if (photo != null)
                {
                    photo.HobbyID = null;
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
