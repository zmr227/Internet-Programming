using System;
using System.Collections.Generic;
using System.IO;
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
    public class StoryController : ControllerBase
    {
        private readonly StoryphaseContext context_;
        private string filePath = null;
        
        public StoryController(StoryphaseContext context)
        {
            context_ = context;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            filePath = Path.Combine(path, "Storyphase/wwwroot/Stories");
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

        [HttpPost("Uploads")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var result = new List<StoryBlocks>();
                foreach (var file in files)
                {
                    // save the uploaded file in wwwroot/images of the MVC project
                    //string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                    var mvcPath = Path.Combine(filePath, file.FileName);

                    var stream = new FileStream(mvcPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                    result.Add(new StoryBlocks() { Name = file.FileName, Path = mvcPath });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}