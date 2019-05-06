using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Storyphase.Data;
using Storyphase.Models;
using Storyphase.Utility;

namespace Storyphase.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminUser)]
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        public readonly ApplicationDbContext _db;
        //private readonly UserManager<ApplicationUsers> _userManager;

        public AdminUsersController(ApplicationDbContext db)
        {
            _db = db;
           
        }
        public IActionResult Index()
        {
            return View(_db.ApplicationUsers.ToList());
        }

        //Get Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _db.ApplicationUsers.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, ApplicationUsers applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUsers userFromDb = _db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefault();
                userFromDb.UserName = applicationUser.UserName;
                userFromDb.PhoneNumber = applicationUser.PhoneNumber;

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(applicationUser);
        }


        //Get Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || id.Trim().Length == 0)
            {
                return NotFound();
            }

            var userFromDb = await _db.ApplicationUsers.FindAsync(id);
            if (userFromDb == null)
            {
                return NotFound();
            }

            return View(userFromDb);
        }


        //Post Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string id)
        {
            ApplicationUsers userFromDb = _db.ApplicationUsers.Where(u => u.Id == id).FirstOrDefault();
            userFromDb.LockoutEnd = DateTime.Now.AddYears(100);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}