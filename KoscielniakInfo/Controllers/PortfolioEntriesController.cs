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

namespace KoscielniakInfo.Controllers
{
    public class PortfolioEntriesController : Controller
    {
        private CVContext db = new CVContext();

        // GET: PortfolioEntries
        public async Task<ActionResult> Index()
        {
            return View(await db.Entries.ToListAsync());
        }

        // GET: PortfolioEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);

            if (portfolioEntry == null)
            {
                return HttpNotFound();
            }
            return View(portfolioEntry);
        }

        // GET: PortfolioEntries/Create
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            if (portfolioEntry == null)
            {
                return HttpNotFound();
            }
            return View(portfolioEntry);
        }

        // POST: PortfolioEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PortfolioEntry portfolioEntry = await db.Entries.FindAsync(id);
            db.Entries.Remove(portfolioEntry);
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
        private void GetCategories(PortfolioEntry portfolioEntry)
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
    }
}
