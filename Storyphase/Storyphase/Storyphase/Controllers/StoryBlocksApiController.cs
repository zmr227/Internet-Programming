using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Storyphase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryBlocksApiController : ControllerBase
    {
        private readonly ApplicationDbContext context_;
        private IHostingEnvironment _env;
        
        public StoryBlocksApiController(ApplicationDbContext context, IHostingEnvironment env)
        {
            context_ = context;
            _env = env;
           
            if (context_.StoryBlocks.Count() == 0)
            {
                context_.StoryBlocks.Add(new StoryBlock { Name = "SeedBlock1", Path = "~/Stories", Content = "Hello World" });
                context_.StoryBlocks.Add(new StoryBlock { Name = "SeedBlock2", Path = "~/Stories", Content = "Hello Again" });
                context_.SaveChanges();
            }
        }
        // GET: api/<controller>
        // ActionResults are automatially serialized to JSON and written into the body of the reply
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryBlock>>> GetStoryBlocks()
        {
            return await context_.StoryBlocks.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoryBlock>> GetStoryBlock(long id)
        {
            var fileItem = await context_.StoryBlocks.FindAsync(id);
           
            if (fileItem == null)
            {
                return NotFound();
            }
            return fileItem;
        }

        // GET api/<controller>/5
        [HttpGet("Story/{id}")]
        public async Task<ActionResult<StoryBlock>> GetAllBlocksByStory(int id)
        {
            var blocks = context_.StoryBlocks.Where(s=>s.StoriesId == id);

            if (blocks == null)
            {
                return NotFound();
            }
            return Ok(blocks);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<StoryBlock>> PostStoryBlock(StoryBlock storyBlock)
        {
            try
            {
                // Find the block by id
                var blockFromDb = context_.StoryBlocks.SingleOrDefault(c => c.StoryBlockId == storyBlock.StoryBlockId);
                if (blockFromDb == null)
                    return NotFound();

                // Add Block
                var block = new StoryBlock
                {
                    Name = storyBlock.Name,
                    Image = storyBlock.Image,
                    Content = storyBlock.Content,
                    StoryBlockId = storyBlock.StoryBlockId,
                    Path = storyBlock.Path
                };
                context_.StoryBlocks.Add(block);
                context_.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
            // context_.StoryBlocks.Add(storyBlock);
            // await context_.SaveChangesAsync();

           // return CreatedAtAction(nameof(GetStoryBlock), new { id = storyBlock.StoryBlockId }, storyBlock);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int storyId, int storyBlockId)
        {
            try
            {
                // check validation 
                var blockInDb = context_.StoryBlocks.SingleOrDefault(c => c.StoryBlockId == storyBlockId && c.StoriesId == storyId);
                if (blockInDb == null)
                    return NotFound();

                // save change
                context_.StoryBlocks.Remove(blockInDb);
                context_.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}
