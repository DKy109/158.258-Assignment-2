using System;
using System.Linq;
using System.Web.Mvc;
using WebAss2.Models;

namespace WebAss2.Controllers
{
    public class BooksController : Controller
    {
        private BookDbContext db = new BookDbContext();
        private readonly int PageSize = 5; // Items per page

        // GET: Books
        public ActionResult Index(string searchString, int? category, string sortOrder, int? page)
        {
            // Set ViewBag for sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSort = sortOrder == "author_asc" ? "author_desc" : "author_asc";
            ViewBag.PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            // Get all books with categories included
            var books = from b in db.Books.Include("Category") select b;

            // Apply search filter
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString));
                ViewBag.SearchString = searchString;
            }

            // Apply category filter
            if (category != null && category > 0)
            {
                books = books.Where(b => b.CategoryId == category);
                ViewBag.SelectedCategory = category;
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "author_asc":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                case "price_asc":
                    books = books.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    books = books.OrderByDescending(b => b.Price);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            // Get all categories for filter dropdown
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

            // Pagination
            int pageNumber = (page ?? 1);
            int totalItems = books.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            var pagedBooks = books.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = PageSize;

            return View(pagedBooks);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Book book = db.Books.Include("Category").FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}