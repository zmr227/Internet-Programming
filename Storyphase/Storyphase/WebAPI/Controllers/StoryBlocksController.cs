using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryBlocksController : ControllerBase
    {
        private readonly StoryphaseContext _context;

        public StoryBlocksController(StoryphaseContext context)
        {
            _context = context;
        }

        // GET: api/StoryBlocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryBlocks>>> GetStoryBlocks()
        {
            return await _context.StoryBlocks.ToListAsync();
        }

        // GET: api/StoryBlocks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoryBlocks>> GetStoryBlocks(long id)
        {
            var storyBlocks = await _context.StoryBlocks.FindAsync(id);

            if (storyBlocks == null)
            {
                return NotFound();
            }

            return storyBlocks;
        }

        // PUT: api/StoryBlocks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoryBlocks(long id, StoryBlocks storyBlocks)
        {
            if (id != storyBlocks.StoryBlocksId)
            {
                return BadRequest();
            }

            _context.Entry(storyBlocks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryBlocksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StoryBlocks
        [HttpPost]
        public async Task<ActionResult<StoryBlocks>> PostStoryBlocks(StoryBlocks storyBlocks)
        {
            _context.StoryBlocks.Add(storyBlocks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoryBlocks", new { id = storyBlocks.StoryBlocksId }, storyBlocks);
        }

        // DELETE: api/StoryBlocks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StoryBlocks>> DeleteStoryBlocks(long id)
        {
            var storyBlocks = await _context.StoryBlocks.FindAsync(id);
            if (storyBlocks == null)
            {
                return NotFound();
            }

            _context.StoryBlocks.Remove(storyBlocks);
            await _context.SaveChangesAsync();

            return storyBlocks;
        }

        private bool StoryBlocksExists(long id)
        {
            return _context.StoryBlocks.Any(e => e.StoryBlocksId == id);
        }
    }
}
