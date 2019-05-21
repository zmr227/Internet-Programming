using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Razor.Pages.StoryList
{
    public class EditStoryModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        // constructor
        public EditStoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Story story { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int id)
        {
            story = _db.Stories.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var storyFromDb = _db.Stories.Find(story.Id);
                storyFromDb.Title = story.Title;
                storyFromDb.Author = story.Author;
                storyFromDb.Description = story.Description;

                await _db.SaveChangesAsync();
                Message = "Story updated successfully";

                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}