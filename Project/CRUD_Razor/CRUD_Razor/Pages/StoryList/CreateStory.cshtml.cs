using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRUD_Razor.Pages.StoryList
{
    public class CreateStoryModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public CreateStoryModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // automatically bind property "story" to the onGet, onPost, no need to write parameter
        [BindProperty] 
        public Story story { get; set; }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.Stories.Add(story); // story is auto binded here
            await _db.SaveChangesAsync();

            Message = "Story created successfully! ";

            return RedirectToPage("Index");
        }
    }
}