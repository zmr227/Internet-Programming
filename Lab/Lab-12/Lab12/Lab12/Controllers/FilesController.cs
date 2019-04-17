/////////////////////////////////////////////////////////////
// FilesController.cs - Web Api Controller                 //
//                                                         //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019 //
/////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab12.Models;
using System.IO;
using Newtonsoft.Json;

namespace Lab12.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileContext context_;

        public FilesController(FileContext context)
        {
            context_ = context;
            if (context_.FileItems.Count() == 0)
            {
                context_.FileItems.Add(new FileItem { Name = "first", Path = "." });
                context_.FileItems.Add(new FileItem { Name = "second", Path = "../Storage" });
                context_.SaveChanges();
            }
        }

        // GET: api/Files - retrieves a JSON array of all file items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileItem>>> GetFileItems()
        {
            // contents are serialized to JSON and placed in the body of the reply message

            return await context_.FileItems.ToListAsync();
        }

        // GET: api/Files/1 - retrieves a single FileItem
        [HttpGet("{id}")]
        public async Task<ActionResult<FileItem>> GetFileItem(int id)
        {
            var fileItem = await context_.FileItems.FindAsync(id);
            if (fileItem == null)
            {
                return NotFound();
            }
            return fileItem;
        }

        // POST: api/Files - creates a new FileItem and returns JSON for that file
        [HttpPost]
        public async Task<ActionResult<FileItem>> PostFileItem(FileItem file)
        {
            context_.FileItems.Add(file);
            await context_.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFileItem), new { id = file.FileItemId }, file);
        }

        // - IIS Express is configured to service only GET, POST, and DELETE commands
        //   so PUT returns a 405 Method not Allowed response.
        // - IIS can be configured to allow them with appropriate settings in a Web.config file.
        //   Unfortunately IIS Express ignores web.config files.
        // - You can accomplish the same thing by DELETEing an item and POSTing a modified
        //   version.  That puts the desired data in the data store, but it will have a 
        //   different Id.

        // PUT: api/Files/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileItem(int id, FileItem file)
        {
            if (id != file.FileItemId)
            {
                return BadRequest();
            }

            context_.Entry(file).State = EntityState.Modified;
            await context_.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Files/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileItem(int id)
        {
            var fileItem = await context_.FileItems.FindAsync(id);

            if (fileItem == null)
            {
                return NotFound();
            }

            context_.FileItems.Remove(fileItem);
            await context_.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<FileItem>>> GetJsonList()
        {
            try
            {
                var result = new List<FileItem>();
                foreach (var file in context_.FileItems)
                {
                    result.Add(new FileItem() { FileItemId = file.FileItemId, Name = file.Name, Path = file.Path });
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("FileList")]
        public async Task<ActionResult<List<FileItem>>> GetFileList()
        {
            try
            {
                var result = new List<string>();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var files = Directory.GetFiles(path);
                var json = JsonConvert.SerializeObject(files);
                
                return Ok(json);
              
            }
            catch
            {
                return BadRequest();
            }
        }

        
        // Upload files
        [HttpPost("Uploads")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            try
            {
                var result = new List<FileItem>();
                var id = 0;
                foreach (var file in files)
                {
                    // save the uploaded file in wwwroot/images
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);
                    result.Add(new FileItem() { FileItemId = id, Name = file.FileName, Path = Directory.GetCurrentDirectory() });
                    id++;
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        // Download file by name
        [HttpGet("Download/{name}")]
        public async Task<IActionResult> DownloadFile(string name)
        {
            if (name == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", name);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}