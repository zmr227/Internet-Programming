using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryBlocksController : ControllerBase
    {
        private readonly FileContext context_;
        private IHostingEnvironment _env;

        

        public StoryBlocksController(FileContext context, IHostingEnvironment env)
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

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<StoryBlock>> PostStoryBlock(StoryBlock fileItem)
        {
            context_.StoryBlocks.Add(fileItem);
            await context_.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStoryBlock), new { id = fileItem.StoryBlockId }, fileItem);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
