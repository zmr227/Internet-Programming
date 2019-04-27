using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly StoryphaseContext context_;

        public StoryController(StoryphaseContext context)
        {
            context_ = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stories>>> Index()
        {
            var stories = context_.Stories
                            .Include(s => s.StoryBlocks)
                            .Include(s => s.Comments)
                            .ToList();

            return stories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStory(int id)
        {
            var story = await context_.Stories
                                .Include(s => s.StoryBlocks)
                                .Include(s => s.Comments)
                                .FirstOrDefaultAsync(s => s.Id == id);

            if (story == null)
            {
                return NotFound();
            }
            
            return Ok(story);
        }

    }
}