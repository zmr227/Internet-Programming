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
    public class StoriesController : ControllerBase
    {
        private readonly StoryphaseContext _context;

        public StoriesController(StoryphaseContext context)
        {
            _context = context;
        }

        // GET: api/Stories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stories>>> GetStories()
        {
            return await _context.Stories.ToListAsync();
        }

        // GET: api/Stories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stories>> GetStories(int id)
        {
            var stories = await _context.Stories.FindAsync(id);

            if (stories == null)
            {
                return NotFound();
            }

            return stories;
        }

        // PUT: api/Stories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStories(int id, Stories stories)
        {
            if (id != stories.Id)
            {
                return BadRequest();
            }

            _context.Entry(stories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoriesExists(id))
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

        // POST: api/Stories
        [HttpPost]
        public async Task<ActionResult<Stories>> PostStories(Stories stories)
        {
            _context.Stories.Add(stories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStories", new { id = stories.Id }, stories);
        }

        // DELETE: api/Stories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Stories>> DeleteStories(int id)
        {
            var stories = await _context.Stories.FindAsync(id);
            if (stories == null)
            {
                return NotFound();
            }

            _context.Stories.Remove(stories);
            await _context.SaveChangesAsync();

            return stories;
        }

        private bool StoriesExists(int id)
        {
            return _context.Stories.Any(e => e.Id == id);
        }
    }
}
