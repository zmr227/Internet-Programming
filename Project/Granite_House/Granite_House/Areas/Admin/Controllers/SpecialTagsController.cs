using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Granite_House.Data;
using Granite_House.Models;
using Microsoft.AspNetCore.Mvc;

namespace Granite_House.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // use EF to access DB and convert whatever is in the DB and pass it to a view
            return View(_db.SpecialTags.ToList());
        }

        // GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }
        // POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags SpecialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(SpecialTags);

                // always use await keyword when using SaveChangesAsync()
                await _db.SaveChangesAsync();

                // using nameof(Index) instead of "Index" can prevent error caused by typing error
                return RedirectToAction(nameof(Index));
            }

            return View(SpecialTags);
        }

        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _db.SpecialTags.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags SpecialTags)
        {
            if (id != SpecialTags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(SpecialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTags);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _db.SpecialTags.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Details action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, SpecialTags SpecialTags)
        {
            if (id != SpecialTags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(SpecialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTags);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _db.SpecialTags.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")] // Though the func is named DeletePost, the view will still see it as "Delete" because the ActionName("Delete")
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var productType = await _db.SpecialTags.FindAsync(id);
            _db.SpecialTags.Remove(productType);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}