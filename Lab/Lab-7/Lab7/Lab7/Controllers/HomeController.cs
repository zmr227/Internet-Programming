using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        // 
        // GET: /Home/

        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /Home/About/
        public IActionResult About(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

        //
        // GET: /Home/Labs/
        public IActionResult Labs()
        {
            return View();
        }

    }
}