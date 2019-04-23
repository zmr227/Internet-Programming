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
        public IEnumerable<Stories> GetAllStories()
        {
            var story = context_.Stories.ToList();

            return story;
        }

        [HttpGet("{id}")]
        public Stories GetStory(int id)
        {
            var story = context_.Stories.SingleOrDefault(s => s.Id == id);

            if (story == null)
            {
                return null;
            }

            //context_.Entry(story).Collection(s => s.StoryBlocks).Load();
            
            return story;
        }


    }
}