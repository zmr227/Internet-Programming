using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Razor.Pages.StoryList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Story> Stories { get; set; }

        // works like controller in MVC, but it can only pass data to page(view)
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task OnGet()
        {
            // stories will have all the books in the db
            Stories = await _db.Stories.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var story = await _db.Stories.FindAsync(id);
            _db.Stories.Remove(story);
            await _db.SaveChangesAsync();

            Message = "Story deleted successfully.";

            return RedirectToPage("Index");
        }
    }
}