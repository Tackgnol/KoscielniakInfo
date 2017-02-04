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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace KoscielniakInfo.Controllers
{
    public class PhotosController : Controller
    {
        private CVContext db = new CVContext();

        // GET: Photos
        public async Task<ActionResult> Index()
        {
            var photos = db.Photos;
            return View(await photos.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Admin()
        {
            var photos = db.Photos;
            return View(await photos.ToListAsync());
        }

        // GET: Photos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: Photos/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Photo photo = new Photo();

            PopulateJobs(photo.JobID);
            PopulateSchools(photo.SchoolID);
            PopulateHobbies(photo.HobbyID);
            PopulateCertificates(photo.CertificateID);
            PopulateSortingNumbers(photo.Sorting);
            return View();
        }

        // POST: Photos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "PhotoID,JobID,SchoolID,HobbyID,CertificateID,Title,Description,Sorting,URL,isCertificate")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                photo.URL = UploadPhoto();
                db.Photos.Add(photo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            PopulateJobs(photo.JobID);
            PopulateSchools(photo.SchoolID);
            PopulateHobbies(photo.HobbyID);
            PopulateCertificates(photo.CertificateID);
            PopulateSortingNumbers(photo.Sorting);
            return View(photo);
        }

        // GET: Photos/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            PopulateJobs(photo.JobID);
            PopulateSchools(photo.SchoolID);
            PopulateHobbies(photo.HobbyID);
            PopulateCertificates(photo.CertificateID);
            PopulateSortingNumbers(photo.Sorting);
            return View(photo);
        }

        // POST: Photos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditPost(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var photoToUpdate = db.Photos
                .Include(h => h.Hobby)
                .Include(j => j.Job)
                .Include(s => s.School)
                .Include(c=>c.Certificate)
                .Where(p => p.PhotoID == Id)
                .Single();


            if (TryUpdateModel(photoToUpdate, "", new string[]
                {
                    "Title",
                    "Description",
                    "JobID",
                    "HobbyID",
                    "SchoolID",
                    "CertificateID",
                    "Sorting",
                    "URL"
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
            PopulateJobs(photoToUpdate.JobID);
            PopulateSchools(photoToUpdate.SchoolID);
            PopulateHobbies(photoToUpdate.HobbyID);
            PopulateCertificates(photoToUpdate.CertificateID);
            PopulateSortingNumbers(photoToUpdate.Sorting);
            return View(photoToUpdate);

        }

        // GET: Photos/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = await db.Photos.FindAsync(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Photo photo = await db.Photos.FindAsync(id);
            db.Photos.Remove(photo);
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Photos/Miniatures/"), photo.URL));
            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Photos/"), photo.URL));
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
        private void PopulateJobs(object selectedJob = null)
        {
            var allJobs = from j in db.Jobs
                          orderby j.id
                          select j;
            ViewBag.JobID = new SelectList(allJobs, "id", "DisplayName", selectedJob);
        }
        private void PopulateHobbies(object selectedHobby = null)
        {
            var allHobbies = from h in db.Hobbies
                             orderby h.Id
                             select h;
            ViewBag.HobbyID = new SelectList(allHobbies, "Id", "Title", selectedHobby);
        }
        private void PopulateSchools(object selectedSchool = null)
        {
            var allShools = from s in db.Schools
                            orderby s.Id
                            select s;
            ViewBag.SchoolID = new SelectList(allShools, "Id", "DisplayName", selectedSchool);
        }
        private void PopulateCertificates(object selectedCertificate = null)
        {
            var allCertificates = from c in db.Certificates
                            orderby c.ID
                            select c;
            ViewBag.CertificateID = new SelectList(allCertificates, "Id", "Name", selectedCertificate);
        }

        private void PopulateSortingNumbers(object selectedSorting = null)
        {
            var allPhotos = from p in db.Photos
                            select p.PhotoID;
            List<int> Numbers = new List<int>();
            for (int i = 1; i <= allPhotos.Count()+1; i++)
            {
                Numbers.Add(i);
            }
            ViewBag.Sorting = new SelectList(Numbers, selectedValue: selectedSorting);
        }
        private string UploadPhoto()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null &&
                    file.ContentLength > 0 &&
                    file.ContentType.Contains("image"
                    ))
                {
                    string extension = Path.GetExtension(file.FileName.ToString().ToLower());
                    string fileName = "CVImage" + String.Format("{0:D5}", db.Photos.Count()) + extension;
                    var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                    file.SaveAs(path);
                    Image imageToResize = Image.FromFile(path);
                    Bitmap resizedImage = ResizeImage(imageToResize, 200, 133);
                    resizedImage.Save(Path.Combine(Server.MapPath("~/Photos/Miniatures/"), fileName));
                    imageToResize.Dispose();
                    resizedImage.Dispose();
                    return fileName;
                }

            }
            return String.Empty;

        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
