using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Extensions;
using Storyphase.Models;
using Storyphase.Models.ViewModels;

namespace Storyphase.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public StoriesViewModel StoriesVM { get; set; }

        public HomeController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            StoriesVM = new StoriesViewModel()
            {
                StoryTypes = _db.StoryTypes.ToList(),
                SpecialTags = _db.SpecialTags.ToList(),
                PrivacyTags = _db.PrivacyTags.ToList(),
                Stories = new Stories(),
                StoryBlocks = new List<StoryBlocks>(),
                Comments = new List<Comments>()
            };
        }

        public async Task<IActionResult> Index()
        {
            var storyList = await _db.Stories.Include(m => m.StoryTypes)
                            .Include(m => m.SpecialTags).Include(m => m.PrivacyTags)
                            .Include(m => m.StoryBlocks).Include(m => m.Comments).ToListAsync();

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

            return View(storyList);
        }

        // Get Details
        public async Task<IActionResult> Details(int id)
        {

            StoriesVM.Stories = await _db.Stories.Include(m => m.StoryTypes).Include(m => m.SpecialTags)
                                .Include(m => m.PrivacyTags).Include(m => m.StoryBlocks).Include(m => m.Comments)
                                .Where(m => m.Id == id).FirstOrDefaultAsync();
            var comments = await _db.Comments.Where(m => m.StoriesId == id).ToListAsync();
            for (int i = 0; i < comments.Count(); i++)
            {
                StoriesVM.Comments.Add(comments[i]);
            }

            return View(StoriesVM);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if (lstFavorite == null)
            {
                lstFavorite = new List<int>();
            }
            if (!lstFavorite.Contains(id))
            {
                lstFavorite.Add(id);
            }
            // set the session
            HttpContext.Session.Set("ssFavorite", lstFavorite);

            return RedirectToAction("Index", "Home", new { area = "User" });
        }

        // remove from favorite list
        public IActionResult Remove(int id)
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if (lstFavorite.Count > 0)
            {
                if (lstFavorite.Contains(id))
                {
                    lstFavorite.Remove(id);
                }
            }
            // set the session
            HttpContext.Session.Set("ssFavorite", lstFavorite);
            return RedirectToAction(nameof(Index));
        }

        // Get StoryBlocks
        public async Task<IActionResult> BlockShow(int? id)
        {
            var blocks = _db.StoryBlocks.Where(b => b.StoriesId == id);
            var blocklist = await blocks.OrderBy(b => b.Position).ToListAsync();

            return View(blocklist);
        }
        public JsonResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIdList)
            {

                StoryBlocks block = _db.StoryBlocks.Where(x => x.StoryBlocksId == itemId).FirstOrDefault();
                block.Position = count;
                if (ModelState.IsValid)
                {
                    _db.Update(block);
                    _db.SaveChanges();
                    count++;
                }
            }
            return Json(true);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
