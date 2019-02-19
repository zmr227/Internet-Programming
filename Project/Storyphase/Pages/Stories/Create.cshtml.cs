using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storyphase.Models;

namespace Storyphase.Pages.Stories
{
    public class CreateModel : PageModel
    {
        private readonly Storyphase.Models.StoryphaseContext _context;

        public CreateModel(Storyphase.Models.StoryphaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Story Story { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Story.Add(Story);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}