using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Storyphase.Data;
using Storyphase.Extensions;

namespace Storyphase.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // load current user's favorite stories' ids to session
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if (lstFavorite == null)
            {
                lstFavorite = new List<int>();
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            var favoriteStories = _db.StoriesAddToFavorites.Where(s => s.UserId == userId).ToList();

            if (favoriteStories != null && favoriteStories.Count > 0)
            {
                foreach (var item in favoriteStories)
                {
                    var id = item.StoryId;
                    if (!lstFavorite.Contains(id))
                    {
                        lstFavorite.Add(id);
                    }
                }
            }

            // set the session
            HttpContext.Session.Set("ssFavorite", lstFavorite);
            return View();
        }
    }
}
