using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [Produces("text/plain")]
        [HttpGet("demo1")]
        public async Task<IActionResult> Demo1()
        {
            try
            {
                return Ok("Demo 1");
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("text/html")]
        [HttpGet("demo2")]
        public async Task<IActionResult> Demo2()
        {
            try
            {
                return new ContentResult ()
                {
                    ContentType = "text/html",
                    Content = "<b><i>Demo 2</i></b>"
                };
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("text/plain")]
        [HttpGet("demo3/{id}")]
        public async Task<IActionResult> Demo3(int id)
        {
            try
            {
                return Ok("Id:" + id);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("text/plain")]
        [HttpGet("demo4/{a}/{b}")]
        public async Task<IActionResult> Demo4(int a, string b)
        {
            try
            {
                return Ok("a:" + a + ", b:" + b);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("demo5")]
        public async Task<IActionResult> Demo5()
        {
            try
            {
                var storyBlock = new StoryBlock { StoryBlockId = 1, Content = "Hello World"};
                return Ok(storyBlock);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("demo6")]
        public async Task<IActionResult> Demo6()
        {
            try
            {
                var storyBlocks = new List<StoryBlock>()
                {
                    new StoryBlock { StoryBlockId = 1, Content = "Hello World" },
                    new StoryBlock { StoryBlockId = 2, Content = "Hello Again"},
                    new StoryBlock { StoryBlockId = 3, Content = "Hey there"}
            };
                return Ok(storyBlocks);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}