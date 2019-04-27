using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Models;
using Storyphase.Utility;

namespace Storyphase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoryBlocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnvironment;

        public StoryBlocksController(ApplicationDbContext context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Admin/StoryBlocks
        public async Task<IActionResult> Index()
        {
            var blocks = _context.StoryBlocks.Include(s => s.Stories);
            return View(await blocks.ToListAsync());
        }

        // GET: Admin/StoryBlocks/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyBlocks = await _context.StoryBlocks
                .Include(s => s.Stories)
                .FirstOrDefaultAsync(m => m.StoryBlocksId == id);
            if (storyBlocks == null)
            {
                return NotFound();
            }

            return View(storyBlocks);
        }

        // GET: Admin/StoryBlocks/Create
        public IActionResult Create()
        {
            ViewData["StoriesId"] = new SelectList(_context.Stories, "Id", "Title");
            return View();
        }
       
        // POST: StoryBlocks/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(StoryBlocks storyBlock)
        {
            try
            {
                _context.Add(storyBlock);
                await _context.SaveChangesAsync();

                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var blockFromDb = _context.StoryBlocks.Find(storyBlock.StoryBlocksId);


                if (files.Count != 0)
                {
                    // if image has been uploaded
                    var curPath = SD.StoryFolder + @"\" + storyBlock.StoriesId;
                    var uploads = Path.Combine(webRootPath, curPath);
                    //var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var filestream = new FileStream(Path.Combine(uploads, storyBlock.StoryBlocksId + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    blockFromDb.Image = @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + extension;
                }
                else
                {
                    // when no image uploaded by user, use default image
                    var uploads = Path.Combine(webRootPath, SD.StoryFolder + @"\" + SD.DefaultStoryImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + ".png");
                    blockFromDb.Image = @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + ".png";
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(storyBlock);
            }
        }
        
        // GET: Admin/StoryBlocks/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyBlocks = await _context.StoryBlocks.FindAsync(id);
            if (storyBlocks == null)
            {
                return NotFound();
            }
            ViewData["StoriesId"] = new SelectList(_context.Stories, "Id", "Title", storyBlocks.StoriesId);
            return View(storyBlocks);
        }

        // POST: StoryBlocks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, StoryBlocks storyBlock)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var storyFromDb = _context.StoryBlocks.Where(m => m.StoryBlocksId == storyBlock.StoryBlocksId).FirstOrDefault();

                // if user uploads a new image file
                if (files.Count > 0 && files[0] != null)
                {
                    var curPath = SD.StoryFolder + @"\" + storyBlock.StoriesId;
                    var uploads = Path.Combine(webRootPath, curPath);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(storyFromDb.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, storyBlock.StoryBlocksId + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, storyBlock.StoryBlocksId + extension_old));
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, storyBlock.StoryBlocksId + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    storyBlock.Image = @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + extension_new;
                }

                // image uploaded by user
                if (storyBlock.Image != null)
                {
                    storyFromDb.Image = storyBlock.Image;
                }

                storyFromDb.Name = storyBlock.Name;
                storyFromDb.Content = storyBlock.Content;
                storyFromDb.Path = storyBlock.Path;

                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(storyBlock);
            }
        }
       
        // GET: Admin/StoryBlocks/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storyBlocks = await _context.StoryBlocks
                .Include(s => s.Stories)
                .FirstOrDefaultAsync(m => m.StoryBlocksId == id);
            if (storyBlocks == null)
            {
                return NotFound();
            }

            return View(storyBlocks);
        }

        // POST: Admin/StoryBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var storyBlocks = await _context.StoryBlocks.FindAsync(id);
            _context.StoryBlocks.Remove(storyBlocks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryBlocksExists(long id)
        {
            return _context.StoryBlocks.Any(e => e.StoryBlocksId == id);
        }
    }
}
