using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Storyphase.Areas.Admin.Controllers
{
    public class StoryBlocksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}