﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Extensions;
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

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> lstFavorite = HttpContext.Session.Get<List<int>>("ssFavorite");
            if(lstFavorite == null)
            {
                lstFavorite = new List<int>();
            }
            lstFavorite.Add(id);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
