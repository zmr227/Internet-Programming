using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        
        [BindProperty]
        public FavoriteViewModel FavoriteVM { get; set; }

        public FavoriteController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            FavoriteVM = new FavoriteViewModel()
            {
                Stories = new List<Stories>()
            };
        }

        // Get 
        public async Task<IActionResult> Index()
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            // retrieve the previous favorites
            var userId = _userManager.GetUserId(HttpContext.User);
            var favoriteStories = _db.StoriesAddToFavorites.Where(s => s.UserId == userId).ToList();

            if (favoriteStories != null && favoriteStories.Count > 0)
            {
                foreach (var item in favoriteStories)
                {
                    Stories sty = _db.Stories.Include(p => p.SpecialTags).Include(p => p.StoryTypes).Include(p => p.PrivacyTags).Where(p => p.Id == item.StoryId).FirstOrDefault();
                    FavoriteVM.Stories.Add(sty);

                    var id = item.StoryId;
                    if (!lstFavorite.Contains(id))
                    {
                        lstFavorite.Add(id);
                    }
                }
            }

            if (lstFavorite != null && lstFavorite.Count > 0)
            {
                foreach (int item in lstFavorite)
                {
                    Stories sty = _db.Stories.Include(p => p.SpecialTags).Include(p => p.StoryTypes).Include(p => p.PrivacyTags).Where(p => p.Id == item).FirstOrDefault();
                    if (!FavoriteVM.Stories.Contains(sty))
                    {
                        FavoriteVM.Stories.Add(sty);
                    }
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
            var userId = _userManager.GetUserId(HttpContext.User);

            foreach (int item in lstFavorite)
            {
                StoriesAddToFavorite storiesSelected = new StoriesAddToFavorite
                {
                    StoryId = item,
                    UserId = userId
                };
                StoriesAddToFavorite exists = _db.StoriesAddToFavorites.Where(s => s.StoryId == item && s.UserId == userId).FirstOrDefault();
                if (exists == null)
                {
                    _db.StoriesAddToFavorites.Add(storiesSelected);
                }

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
            var userId = _userManager.GetUserId(HttpContext.User);
            StoriesAddToFavorite record = _db.StoriesAddToFavorites.Where(s => s.StoryId == id && s.UserId == userId).FirstOrDefault();
            try { 
            if (lstFavorite != null && lstFavorite.Count > 0)
            {
                if (lstFavorite.Contains(id))
                {
                    lstFavorite.Remove(id);
                }
            }
            if (record != null)
            {
                _db.StoriesAddToFavorites.Remove(record);
                _db.SaveChanges();
            }
            HttpContext.Session.Set("ssFavorite", lstFavorite);

            return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}