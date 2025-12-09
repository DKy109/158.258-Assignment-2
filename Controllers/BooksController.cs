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
        public ActionResult Index(string searchString, string category,
                                 string sortOrder, int? page,
                                 decimal? minPrice, decimal? maxPrice,
                                 int? minYear, int? maxYear)
        {
            // Set ViewBag for sorting
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSort = sortOrder == "author_asc" ? "author_desc" : "author_asc";
            ViewBag.PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewBag.YearSort = sortOrder == "year_asc" ? "year_desc" : "year_asc";
            ViewBag.RatingSort = sortOrder == "rating_asc" ? "rating_desc" : "rating_asc";

            // Get all books
            var books = from b in db.Books select b;

            // Apply search filter
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                      || b.Author.Contains(searchString)
                                      || b.ISBN.Contains(searchString)
                                      || b.Description.Contains(searchString));
                ViewBag.SearchString = searchString;
            }

            // Apply category filter
            if (!String.IsNullOrEmpty(category) && category != "All")
            {
                books = books.Where(b => b.Category == category);
                ViewBag.SelectedCategory = category;
            }

            // Apply price range filter
            if (minPrice.HasValue)
            {
                books = books.Where(b => b.Price >= minPrice.Value);
                ViewBag.MinPrice = minPrice.Value;
            }
            if (maxPrice.HasValue)
            {
                books = books.Where(b => b.Price <= maxPrice.Value);
                ViewBag.MaxPrice = maxPrice.Value;
            }

            // Apply year range filter
            if (minYear.HasValue)
            {
                books = books.Where(b => b.PublishedYear >= minYear.Value);
                ViewBag.MinYear = minYear.Value;
            }
            if (maxYear.HasValue)
            {
                books = books.Where(b => b.PublishedYear <= maxYear.Value);
                ViewBag.MaxYear = maxYear.Value;
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
                case "year_asc":
                    books = books.OrderBy(b => b.PublishedYear);
                    break;
                case "year_desc":
                    books = books.OrderByDescending(b => b.PublishedYear);
                    break;
                case "rating_asc":
                    books = books.OrderBy(b => b.Rating);
                    break;
                case "rating_desc":
                    books = books.OrderByDescending(b => b.Rating);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            // Get distinct categories for filter dropdown
            ViewBag.Categories = db.Books.Select(b => b.Category).Distinct().ToList();

            // Pagination
            int pageNumber = (page ?? 1);
            int totalItems = books.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            var pagedBooks = books.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = PageSize;

            // Pass data to view using ViewBag for filters
            ViewBag.CategoryList = new SelectList(db.Books.Select(b => b.Category).Distinct());

            return View(pagedBooks);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
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

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Books.Select(b => b.Category).Distinct().ToList();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Author,Category,ISBN,PublishedYear,Price,Description,InStock,Rating,PageCount,Publisher,Language")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = db.Books.Select(b => b.Category).Distinct().ToList();
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
            ViewBag.Categories = db.Books.Select(b => b.Category).Distinct().ToList();
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Author,Category,ISBN,PublishedYear,Price,Description,InStock,Rating,PageCount,Publisher,Language")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Books.Select(b => b.Category).Distinct().ToList();
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

        // AJAX Search Action
        [HttpGet]
        public JsonResult Search(string term)
        {
            var results = db.Books
                .Where(b => b.Title.Contains(term) || b.Author.Contains(term))
                .Select(b => new {
                    id = b.Id,
                    title = b.Title,
                    author = b.Author,
                    price = b.Price.ToString("C"),
                    category = b.Category
                })
                .Take(10)
                .ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        // Get Statistics
        public ActionResult Statistics()
        {
            var stats = new
            {
                TotalBooks = db.Books.Count(),
                AveragePrice = db.Books.Average(b => b.Price),
                AverageRating = db.Books.Where(b => b.Rating.HasValue).Average(b => b.Rating),
                BooksByCategory = db.Books.GroupBy(b => b.Category)
                                        .Select(g => new { Category = g.Key, Count = g.Count() })
                                        .ToList()
            };

            return View(stats);
        }

        // Export to CSV
        public ActionResult ExportToCsv()
        {
            var books = db.Books.ToList();

            string csv = "Title,Author,Category,ISBN,PublishedYear,Price\n";
            foreach (var book in books)
            {
                csv += $"\"{book.Title}\",\"{book.Author}\",\"{book.Category}\",\"{book.ISBN}\",{book.PublishedYear},{book.Price}\n";
            }

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BooksExport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();

            return null;
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