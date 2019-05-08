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
        // define all available image format
        public List<string> ImageFormat;

        public StoryBlocksController(ApplicationDbContext context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            ImageFormat = new List<string>(new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" });
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
            if (ModelState.IsValid)
            {
                {
                    // set its position as the last block
                    var story = _context.Stories.Where(x => x.Id == storyBlock.StoriesId).FirstOrDefault();
                    story.BlockNumber++;
                    storyBlock.Position = story.BlockNumber;

                    _context.StoryBlocks.Add(storyBlock);
                    await _context.SaveChangesAsync();

                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var blockFromDb = _context.StoryBlocks.Find(storyBlock.StoryBlocksId);


                    if (files.Count != 0)
                    {
                        // if image has been uploaded
                        var curPath = SD.StoryFolder + @"\" + storyBlock.StoriesId;
                        var uploads = Path.Combine(webRootPath, curPath);
                        var extension = Path.GetExtension(files[0].FileName);
                        if (ImageFormat.Contains(extension))
                        {
                            using (var filestream = new FileStream(Path.Combine(uploads, storyBlock.StoryBlocksId + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filestream);
                            }
                            blockFromDb.Image = @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + extension;
                        }
                        else
                        {
                            uploads = Path.Combine(webRootPath, SD.StoryFolder + @"\" + SD.DefaultStoryImage);
                            System.IO.File.Copy(uploads, webRootPath + @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + ".png");
                            blockFromDb.Image = @"\" + SD.StoryFolder + @"\" + storyBlock.StoriesId + @"\" + storyBlock.StoryBlocksId + ".png";

                        }
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
            }

            return View(storyBlock);
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

                    // if the extension of the upload file is valid
                    if (ImageFormat.Contains(extension_new))
                    {
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
                }
                // image uploaded by user
                if (storyBlock.Image != null)
                {
                    storyFromDb.Image = storyBlock.Image;
                }

                storyFromDb.Name = storyBlock.Name;
                storyFromDb.Content = storyBlock.Content;
                storyFromDb.StoriesId = storyBlock.StoriesId;

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
            try
            {
                var storyBlocks = await _context.StoryBlocks.FindAsync(id);
                // remove image
                string webRootPath = _hostingEnvironment.WebRootPath;
                var curPath = SD.StoryFolder + @"\" + storyBlocks.StoriesId;
                var uploads = Path.Combine(webRootPath, curPath);
                var extension = Path.GetExtension(storyBlocks.Image);

                if (System.IO.File.Exists(Path.Combine(uploads, storyBlocks.StoryBlocksId + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, storyBlocks.StoryBlocksId + extension));
                }

                _context.StoryBlocks.Remove(storyBlocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return BadRequest();
            }
        }



        private bool StoryBlocksExists(long id)
        {
            return _context.StoryBlocks.Any(e => e.StoryBlocksId == id);
        }

        public ActionResult UpdateItem(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIdList)
            {
                try
                {
                    StoryBlocks block = _context.StoryBlocks.Where(x => x.StoryBlocksId == itemId).FirstOrDefault();
                    block.Position = count;
                    if (block == null)
                    {
                        _context.StoryBlocks.Add(block);
                    }
                    else
                    {
                        _context.StoryBlocks.Update(block);
                    }
                    _context.SaveChangesAsync();

                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return new JsonResult(true);
        }
    }
}
