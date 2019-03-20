using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab9.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab9.Controllers
{
    public class AuthorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = AuthorList.getInstance();
            IEnumerable<Author> userList = (IEnumerable<Author>)model.Authors;
            return View(userList);
        }

        //----< shows details for each story >----------------------
        public ViewResult Details(int id)
        {
            var model = AuthorList.getInstance();
            var user = model[id - 1];
            return View(user);
        }

        //----< gets form to edit a specific course via id >---------

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userList = AuthorList.getInstance();
            var model = userList[id - 1];
            return View(model);
        }

        //----< posts back edited results for specific course >------

        [HttpPost]
        public IActionResult Edit(int id, Author user)
        {
            var model = AuthorList.getInstance();
            model[id - 1] = user;
            return RedirectToAction("Index");
        }

        //----< gets form for creating a course >--------------------

        [HttpGet]
        public IActionResult Create(int id)
        {
            var userList = AuthorList.getInstance();

            var model = new Author();
            model.Id = userList.size() + 1;
            return View(model);
        }
        //----< posts back new courses details >---------------------

        [HttpPost]
        public IActionResult Create(int id, Author user)
        {
            var model = AuthorList.getInstance();
            model.add(user);
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
            var styList = AuthorList.getInstance();
            styList.delete(id - 1);
            return RedirectToAction("Index");
        }
    }
}