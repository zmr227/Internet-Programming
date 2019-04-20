using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storyphase.Data;
using Storyphase.Models;
using Storyphase.Utility;

namespace Storyphase.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    [Area("Admin")]
    public class StoryTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StoryTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.StoryTypes.ToList());
        }

        // GET Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Create
        [HttpPost]
        [ValidateAntiForgeryToken] //with each post request, this token is added, passed along with the request and checked when request reaches the server
        public async Task<IActionResult> Create(StoryTypes storyTypes)
        {
            // check on the server side whether the state in the passed storyTypes meets the definition in StoryTypes Model
            if (ModelState.IsValid)
            {
                _db.Add(storyTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storyTypes);
        }

        // GET Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyTypes = await _db.StoryTypes.FindAsync(id);
            if (storyTypes == null)
            {
                return NotFound();
            }

            return View(storyTypes);
        }

        // POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StoryTypes storyTypes)
        {
            if (id != storyTypes.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                _db.Update(storyTypes); // auto update all properties but will be a waste when user only change several properties but the model has hundreds
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storyTypes);
        }

        // GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyTypes = await _db.StoryTypes.FindAsync(id);
            if (storyTypes == null)
            {
                return NotFound();
            }

            return View(storyTypes);
        }

        // GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyTypes = await _db.StoryTypes.FindAsync(id);
            if (storyTypes == null)
            {
                return NotFound();
            }

            return View(storyTypes);
        }

        // POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storyTypes = await _db.StoryTypes.FindAsync(id);

            _db.StoryTypes.Remove(storyTypes);

           await _db.SaveChangesAsync();
           return RedirectToAction(nameof(Index));
        }
    }
}