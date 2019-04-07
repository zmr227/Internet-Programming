using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    [Area("Admin")]
    public class StoriesController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly HostingEnvironment _hostingEnvironment;

        [BindProperty]
        public StoriesViewModel StoriesVM { get; set; }

        // initialize the constructor
        public StoriesController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            StoriesVM = new StoriesViewModel()
            {
                StoryTypes = _db.StoryTypes.ToList(),
                SpecialTags = _db.SpecialTags.ToList(),
                PrivacyTags = _db.PrivacyTags.ToList(),
                Stories = new Models.Stories()
            };
        }

        // GET: Stories
        public async Task<IActionResult> Index()
        {
            var stories = _db.Stories.Include(m => m.StoryTypes).Include(m => m.SpecialTags).Include(m => m.PrivacyTags);
            return View(await stories.ToListAsync());
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
            try
            {
                // TODO: Add insert logic here
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

                    using (var filestream = new FileStream(Path.Combine(uploads, StoriesVM.Stories.Id + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    storyFromDb.Image = @"\" + SD.ImageFolder + @"\" + StoriesVM.Stories.Id + extension;
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
            catch
            {
                return View(StoriesVM);
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
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
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
            catch
            {
                return View(StoriesVM);
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
            StoriesVM.Stories = await _db.Stories.Include(m => m.SpecialTags).Include(m => m.StoryTypes).SingleOrDefaultAsync(m => m.Id == id);
            if (StoriesVM.Stories == null)
            {
                return NotFound();
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
            StoriesVM.Stories = await _db.Stories.Include(m => m.SpecialTags).Include(m => m.StoryTypes).SingleOrDefaultAsync(m => m.Id == id);
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
    }
}