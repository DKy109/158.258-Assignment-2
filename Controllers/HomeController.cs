using System.Web.Mvc;

namespace WebAss2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Book Library Home";
            ViewBag.Message = "Welcome to our Book Library Management System";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About Book Library";
            ViewBag.Message = "This application demonstrates ASP.NET MVC web development skills.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Contact Us";
            ViewBag.Message = "Feel free to contact us for any inquiries.";
            return View();
        }
    }
}