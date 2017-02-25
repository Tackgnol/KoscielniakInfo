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
using System.Text;
using System.IO;

namespace KoscielniakInfo.Controllers
{
    public class PortfolioEntriesController : Controller
    {
        private CVContext db = new CVContext();

        // GET: PortfolioEntries
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult> Admin()
        {
            return View(await db.Entries.ToListAsync());
        }

        public async Task<ActionResult> Index(int? id)
        {
            ViewBag.Categories = db.Categories;
            if (id==null)
            {
                return View(await db.Entries.ToListAsync());
            }
            var currentCategory = await db.Categories.FindAsync(id);
            var filteredEntries = from e in db.Entries
                                  where e.Category.Contains(currentCategory.Name)
                                  select e;

            return View(await filteredEntries.ToListAsync());
                                     
        }

        // GET: PortfolioEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            GetScreenShots(portfolioEntry);
            if (portfolioEntry == null)
            {
                return HttpNotFound();
            }
            GetCategories(portfolioEntry, true);                         
            

            return View(portfolioEntry);
        }

        // GET: PortfolioEntries/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var entry = new PortfolioEntry();
            GetCategories(entry);
            return View();
        }

        // POST: PortfolioEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Client,Description,Start,End,UsedSkills,GitHubLink")] PortfolioEntry portfolioEntry, string[] selectedCategories, string[] newCategories)
        {
            if (ModelState.IsValid)
            {
                foreach (string newCategory in newCategories)
                {
                   AddCategory(newCategory);
                }
                string categories = "";
                List<string> selectedCategoriesList = new List<string>();
                if (selectedCategories == null)
                {
                    selectedCategoriesList = new List<string>();
                }
                else
                {
                    selectedCategoriesList = selectedCategories.ToList();
                }
                foreach (string category in newCategories)
                {
                    if (!selectedCategoriesList.Contains(category))
                    {
                        selectedCategoriesList.Add(category);
                    }
                }
                StringBuilder builder = new StringBuilder();
                foreach (string category in selectedCategoriesList)
                {
                    builder.Append(category).Append(";");
                }
                builder.Length--;
                categories = builder.ToString();
                portfolioEntry.Category = categories;
                db.Entries.Add(portfolioEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(portfolioEntry);
        }

        // GET: PortfolioEntries/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            GetCategories(portfolioEntry);
            if (portfolioEntry == null)
            {
                return HttpNotFound();
            }
            return View(portfolioEntry);
        }

        // POST: PortfolioEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Client,Description,Start,End,GitHubLink")] PortfolioEntry portfolioEntry, string[] selectedCategories, string[] newCategories)
        {
            if (ModelState.IsValid)
            {
                foreach (string newCategory in newCategories)
                {
                    AddCategory(newCategory);
                }
                string categories = "";
                List<string> selectedCategoriesList = new List<string>();
                if (selectedCategories == null)
                {
                    selectedCategoriesList = new List<string>();
                }
                else
                {
                    selectedCategoriesList = selectedCategories.ToList();
                }
                foreach (string category in newCategories)
                {
                    if (!selectedCategoriesList.Contains(category) && category!="")
                    {
                        selectedCategoriesList.Add(category);
                    }
                }
                StringBuilder builder = new StringBuilder();
                foreach (string category in selectedCategoriesList)
                {
                    builder.Append(category).Append(";");
                }
                builder.Length--;
                categories = builder.ToString();
                portfolioEntry.Category = categories;
                db.Entries.Add(portfolioEntry);
                db.Entry(portfolioEntry).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(portfolioEntry);
        }

        // GET: PortfolioEntries/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            GetCategories(portfolioEntry, true);
            if (portfolioEntry == null)
            {
                return HttpNotFound();
            }
            return View(portfolioEntry);
        }

        // POST: PortfolioEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            var entryScreenshots = from s in db.ScreenShots
                                   where s.PortfolioEntryID == id
                                   select s;
            foreach (var screen in entryScreenshots)
            {
                if (screen !=null)
                {
                    screen.PortfolioEntryID = null;
                }
                
            }
            db.Entries.Remove(portfolioEntry);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void GetCategories(PortfolioEntry portfolioEntry, bool selectedOnly=false)
        {
            var allCategories = db.Categories;
            var selectedCategories = CategoryList(portfolioEntry.Category);
            var hashCategories = new HashSet<string>(selectedCategories);
            var viewModel = new List<SelectedCategory>();
            foreach (var category in allCategories)
            {
                viewModel.Add(new SelectedCategory
                {
                    ID = category.ID,
                    Name = category.Name,
                    Selected = hashCategories.Contains(category.Name)
                });
            }
            if (selectedOnly == true)
            {
                viewModel.RemoveAll(c => c.Selected == false);
            }
            ViewBag.Categories = viewModel;

        }
        private List<string> CategoryList(string inputString)
        {
            char delimiter = ';';
            if (inputString == null)
            {
                return new List<string>();
            }
            else
            {
                return inputString.Split(delimiter).ToList();
            }
        }
        private void AddCategory(string category)
        {

            if (!db.Categories.Any(c=>c.Name==category) && category!="")
            {
                var categoryToAdd = new Category();
                categoryToAdd.Name = category;
                db.Categories.Add(categoryToAdd);
            }

        }
        private void GetScreenShots(PortfolioEntry portfolioEntry)
        {
            Dictionary<string, IEnumerable<ScreenShot>> Dict = new Dictionary<string, IEnumerable<ScreenShot>>();
            string displayName;
            foreach (var type in Enum.GetNames(typeof(ScreenShotType)))
            {
                switch (type)
                {
                    case "UseCase":
                        displayName = "Use Cases: ";
                        break;
                    case "Behavior":
                        displayName = "Behavioral Diagrams: ";
                        break;
                    case "ClassStructure":
                        displayName = "Class Structure Diagrams: ";
                        break;
                    case "MainPage":
                        displayName = "Main view: ";
                        break;
                    case "SomthingCool":
                        displayName = "Additional Views: ";
                        break;
                    default:
                        displayName = "Unknown Type: ";
                        break;
                }
                var typeScreens = from s in portfolioEntry.ScreenShots
                                  where s.Type.ToString() == type
                                  select s;
                if (typeScreens.Count() != 0)
                {
                    Dict.Add(displayName, typeScreens);
                }
                
            }
            ViewBag.ScreenShotDict = Dict;

        }


    }
}
