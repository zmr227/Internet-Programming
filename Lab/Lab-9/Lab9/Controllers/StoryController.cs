using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab9.Controllers
{
    public class StoryController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = StoryList.getInstance();
            IEnumerable<Story> crsLst = (IEnumerable<Story>)model.Stories;
            return View(crsLst);
        }

        //----< shows details for each story >----------------------
        public ViewResult Details(int id)
        {
            var model = StoryList.getInstance();
            var sty = model[id - 1];
            return View(sty);
        }

        //----< gets form to edit a specific course via id >---------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var styList = StoryList.getInstance();
            var model = styList[id - 1];
            return View(model);
        }

        //----< posts back edited results for specific course >------

        [HttpPost]
        public IActionResult Edit(int id, Story sty)
        {
            var model = StoryList.getInstance();
            model[id - 1] = sty;
            return RedirectToAction("Index");
        }

        //----< gets form for creating a course >--------------------

        [HttpGet]
        public IActionResult Create(int id)
        {
            var styList = StoryList.getInstance();

            var model = new Story();
            model.Id = styList.size() + 1;
            return View(model);
        }
        //----< posts back new courses details >---------------------

        [HttpPost]
        public IActionResult Create(int id, Story sty)
        {
            var model = StoryList.getInstance();
            model.add(sty);
            return RedirectToAction("Index");
        }

        //----< deletes a story by id >-----------------------------
        /*
         * - note that Delete does not send back a view, but
         *   simply redirects back to the Index view, which 
         *   will not show the deleted course because it was ...
         */
        public IActionResult Delete(int id)
        {
            var styList = StoryList.getInstance();
            styList.delete(id - 1);
            return RedirectToAction("Index");
        }
    }
}