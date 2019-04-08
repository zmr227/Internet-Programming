using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Models;

namespace Storyphase.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var storyList = await _db.Stories.Include(m => m.StoryTypes).Include(m => m.SpecialTags).Include(m => m.PrivacyTags).ToListAsync();

            return View(storyList);
        }

        public async Task<IActionResult> Details(int id)
        {

            var story = await _db.Stories.Include(m => m.StoryTypes).Include(m => m.SpecialTags).Include(m => m.PrivacyTags).Where(m=>m.Id == id).FirstOrDefaultAsync();

            return View(story);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
