using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storyphase.Models;

namespace Storyphase.Pages.Stories
{
    public class IndexModel : PageModel
    {
        private readonly Storyphase.Models.StoryphaseContext _context;

        public IndexModel(Storyphase.Models.StoryphaseContext context)
        {
            _context = context;
        }

        public IList<Story> Story { get;set; }

        public async Task OnGetAsync()
        {
            Story = await _context.Story.ToListAsync();
        }
    }
}
