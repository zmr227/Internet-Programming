using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Storyphase.Data;
using Storyphase.Models;
using Microsoft.AspNetCore.Mvc;

namespace Storyphase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrivacyTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PrivacyTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // use EF to access DB and convert whatever is in the DB and pass it to a view
            return View(_db.PrivacyTags.ToList());
        }

        // GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }
        // POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrivacyTags PrivacyTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(PrivacyTags);

                // always use await keyword when using SaveChangesAsync()
                await _db.SaveChangesAsync();

                // using nameof(Index) instead of "Index" can prevent error caused by typing error
                return RedirectToAction(nameof(Index));
            }

            return View(PrivacyTags);
        }

        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyType = await _db.PrivacyTags.FindAsync(id);
            if (storyType == null)
            {
                return NotFound();
            }

            return View(storyType);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrivacyTags PrivacyTags)
        {
            if (id != PrivacyTags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(PrivacyTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PrivacyTags);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyType = await _db.PrivacyTags.FindAsync(id);
            if (storyType == null)
            {
                return NotFound();
            }

            return View(storyType);
        }

        //POST Details action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, PrivacyTags PrivacyTags)
        {
            if (id != PrivacyTags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(PrivacyTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PrivacyTags);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyType = await _db.PrivacyTags.FindAsync(id);
            if (storyType == null)
            {
                return NotFound();
            }

            return View(storyType);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")] // Though the func is named DeletePost, the view will still see it as "Delete" because the ActionName("Delete")
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var storyType = await _db.PrivacyTags.FindAsync(id);
            _db.PrivacyTags.Remove(storyType);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}