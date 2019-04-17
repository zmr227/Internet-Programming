using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Extensions;
using Storyphase.Models;
using Storyphase.Models.ViewModels;

namespace Storyphase.Areas.User.Controllers
{
    [Area("User")]
    public class FavoriteController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public FavoriteViewModel FavoriteVM { get; set; }

        public FavoriteController(ApplicationDbContext db)
        {
            _db = db;
            FavoriteVM = new FavoriteViewModel()
            {
                Stories = new List<Stories>()
            };
        }

        // Get 
        public async Task<IActionResult> Index()
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if (lstFavorite != null && lstFavorite.Count > 0)
            {
                foreach (int item in lstFavorite)
                {
                    Stories sty = _db.Stories.Include(p => p.SpecialTags).Include(p => p.StoryTypes).Include(p => p.PrivacyTags).Where(p => p.Id == item).FirstOrDefault();
                    FavoriteVM.Stories.Add(sty);
                }
            }
            return View(FavoriteVM);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            
            foreach(int item in lstFavorite)
            {
                StoriesAddToFavorite storiesSelected = new StoriesAddToFavorite
                {
                    StoryId = item
                };
               
                _db.StoriesAddToFavorites.Add(storiesSelected);
            }
            _db.SaveChanges();
            // empty list
            // lstFavorite = new List<int>();
            // HttpContext.Session.Set("ssFavorite", lstFavorite);
            return RedirectToAction("Index");
        }

        // remove favorite
        public IActionResult Remove(int id)
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if(lstFavorite.Count > 0)
            {
                if (lstFavorite.Contains(id))
                {
                    lstFavorite.Remove(id);
                }
            }
            HttpContext.Session.Set("ssFavorite", lstFavorite);

            return RedirectToAction(nameof(Index));
        }
    }
}