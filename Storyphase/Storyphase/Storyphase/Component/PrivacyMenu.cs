using Microsoft.AspNetCore.Mvc;
using Storyphase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storyphase.Component
{
    public class PrivacyMenu : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public PrivacyMenu(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var privacy = _db.PrivacyTags.OrderBy(x => x.Name);
            return View(privacy); 
        }
    }
}
