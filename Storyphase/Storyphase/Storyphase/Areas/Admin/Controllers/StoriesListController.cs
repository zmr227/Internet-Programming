using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storyphase.Data;
using Storyphase.Models;
using Storyphase.Models.ViewModels;
using Storyphase.Utility;

namespace Storyphase.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    [Area("Admin")]
    public class StoriesListController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly HostingEnvironment _hostingEnvironment;

        public List<String> ImageFormat;
          
        [BindProperty]
        public StoriesViewModel StoriesVM { get; set; }

        // initialize the constructor
        public StoriesListController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            ImageFormat = new List<string>(new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".tif"});

            StoriesVM = new StoriesViewModel()
            {
                StoryTypes = _db.StoryTypes.ToList(),
                SpecialTags = _db.SpecialTags.ToList(),
                PrivacyTags = _db.PrivacyTags.ToList(),
                Stories = new Stories(),
                StoryBlocks = new List<StoryBlocks>(),
                Comments = new List<Comments>()
            };
        }

        // GET: Stories
        public async Task<IActionResult> Index()
        {
            var storiesVM = await _db.Stories.Include(m => m.StoryTypes)
                            .Include(m => m.SpecialTags).Include(m => m.PrivacyTags)
                            .Include(m => m.StoryBlocks).Include(m => m.Comments).ToListAsync();

            return View(storiesVM);
        }


        // GET: Stories/Create
        public IActionResult Create()
        {
            return View(StoriesVM);
        }

        // POST: Stories/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (StoriesVM.Stories.Title != null && _db.Stories.Any(x => x.Title.Equals(StoriesVM.Stories.Title)))
            {
                ModelState.AddModelError("Title", "Already Exists");
            }
            if (ModelState.IsValid)
            {
                _db.Stories.Add(StoriesVM.Stories);
                await _db.SaveChangesAsync();

                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var storyFromDb = _db.Stories.Find(StoriesVM.Stories.Id);

                if (files.Count != 0)
                {
                    // if image has been uploaded
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension = Path.GetExtension(files[0].FileName);
                    if (ImageFormat.Contains(extension))
                    {
                        // create a folder for current story
                        string pathString = Path.Combine(webRootPath, SD.StoryFolder) + @"\" + StoriesVM.Stories.Id.ToString();
                        Directory.CreateDirectory(pathString);

                        using (var filestream = new FileStream(Path.Combine(uploads, StoriesVM.Stories.Id + extension), FileMode.Create))
                        {
                            files[0].CopyTo(filestream);
                        }
                        storyFromDb.Image = @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + extension;
                    }
                    else
                    {
                        // if the uploaded file is not of supported format
                        uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultStoryImage);
                        System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + ".png");
                        storyFromDb.Image = @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + ".png";
                    }
                }
                else
                {
                    // when no image uploaded by user, use default image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultStoryImage);
                    System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + ".png");
                    storyFromDb.Image = @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + ".png";
                }
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            else
            {
                ViewBag.stories = StoriesVM.Stories;
                return View("Conflict");
            }
        }

        // GET: Stories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StoriesVM.Stories = await _db.Stories.Include(m => m.StoryTypes).Include(m => m.SpecialTags).Include(m => m.PrivacyTags).SingleOrDefaultAsync(m => m.Id == id);

            if (StoriesVM.Stories == null)
            {
                return NotFound();
            }
            return View(StoriesVM);
        }

        // POST: Stories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (StoriesVM.Stories.Title != null && _db.Stories.Any(x => x.Title.Equals(StoriesVM.Stories.Title) && x.Id != id))
            {
                ModelState.AddModelError("Title", "Already Exists");
            }
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var storyFromDb = _db.Stories.Where(m => m.Id == StoriesVM.Stories.Id).FirstOrDefault();

                // if user uploads a new image file
                if (files.Count > 0 && files[0] != null)
                {
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(storyFromDb.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, StoriesVM.Stories.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, StoriesVM.Stories.Id + extension_old));
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, StoriesVM.Stories.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    StoriesVM.Stories.Image = @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + extension_new;
                }

                // image uploaded by user
                if (StoriesVM.Stories.Image != null)
                {
                    storyFromDb.Image = StoriesVM.Stories.Image;
                }

                storyFromDb.Title = StoriesVM.Stories.Title;
                storyFromDb.Description = StoriesVM.Stories.Description;
                storyFromDb.CreateTime = DateTime.Now.ToLocalTime();
                storyFromDb.StoryTypeId = StoriesVM.Stories.StoryTypeId;
                storyFromDb.SpecialTagId = StoriesVM.Stories.SpecialTagId;
                storyFromDb.PrivacyTagId = StoriesVM.Stories.PrivacyTagId;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.stories = StoriesVM.Stories;
                return View("Conflict");
            }
        }

        // GET: Stories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // receive the product info
            StoriesVM.Stories = await _db.Stories.Include(m => m.SpecialTags)
                                        .Include(m => m.StoryTypes)
                                        .SingleOrDefaultAsync(m => m.Id == id);

            if (StoriesVM.Stories == null)
            {
                return NotFound();
            }

            var comments = await _db.Comments.Where(m => m.StoriesId == id).ToListAsync();
            for (int i = 0; i < comments.Count(); i++)
            {
                StoriesVM.Comments.Add(comments[i]);
            }

            return View(StoriesVM);
        }

        // GET: Stories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // receive the product info
            StoriesVM.Stories = await _db.Stories.Include(m => m.SpecialTags)
                                        .Include(m => m.StoryTypes)
                                        .Include(m => m.Comments)
                                        .Include(m => m.StoryBlocks)
                                        .SingleOrDefaultAsync(m => m.Id == id);
            if (StoriesVM.Stories == null)
            {
                return NotFound();
            }
            return View(StoriesVM);
        }

        // POST: Stories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                Stories stories = await _db.Stories.FindAsync(id);

                // delete blocks
                var blocks = _db.StoryBlocks.Where(b => b.StoriesId == id).ToList();
                if (blocks.Count > 0)
                {
                    foreach (var item in blocks)
                    {
                        _db.StoryBlocks.Remove(item);
                    }
                    var block_imgs = Path.Combine(webRootPath, SD.StoryFolder + @"\" + stories.Id);
                    Directory.Delete(block_imgs, true);

                }
                // delete comments
                var comments = _db.Comments.Where(c => c.StoriesId == id);
                if (comments != null)
                {
                    foreach (var item in comments)
                    {
                        _db.Comments.Remove(item);
                    }
                }
                // delete story
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(stories.Image);

                if (System.IO.File.Exists(Path.Combine(uploads, stories.Id + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, stories.Id + extension));
                }

                _db.Stories.Remove(stories);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Get StoryBlocks
        public async Task<IActionResult> BlockShow(int? id)
        {
            var blocks = _db.StoryBlocks.Where(b => b.StoriesId == id);
            var blocklist = await blocks.OrderBy(b => b.Position).ToListAsync();

            return View(blocklist);
        }


    }
}