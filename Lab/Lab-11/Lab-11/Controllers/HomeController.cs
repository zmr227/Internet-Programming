/////////////////////////////////////////////////////////////////
// HomeController.cs - Controller for CourseApplication Demo   //
//                                                             //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019     //
/////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Lab11.Models;
using Lab11.Data;
using Microsoft.AspNetCore.Authorization;

namespace Lab11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context_;
        private const string sessionId_ = "SessionId";

        public HomeController(ApplicationDbContext context)
        {
            context_ = context;
        }

        //----< show list of courses >-------------------------------

        public IActionResult Index()
        {
            return View(context_.Stories.ToList<Story>());
        }

        //----< show list of lectures, ordered by Title >------------

        public IActionResult Comments()
        {
            // fluent API
            var comments = context_.Comments.Include(l => l.Story);
            var orderedLects = comments.OrderBy(l => l.PostDate)
              .OrderBy(l => l.Story)
              .Select(l => l);
            return View(orderedLects);

            // Linq
            //var lects = context_.Lectures.Include(l => l.Course);
            //var orderedLects = from l in lects
            //                   orderby l.Title
            //                   orderby l.Course
            //                   select l;
            //return View(orderedLects);

            // doesn't return Lecture's course nor order by title
            //return View(context_.Lectures.ToList<Lecture>());
        }

        //----< displays form for creating a course >----------------

        [HttpGet]
        public IActionResult CreateStory(int id)
        {
            var model = new Story();
            return View(model);
        }

        //----< posts back new courses details >---------------------

        [HttpPost]
        public IActionResult CreateStory(int id, Story sty)
        {
            context_.Stories.Add(sty);
            context_.SaveChanges();
            return RedirectToAction("Index");
        }

        //----< deletes a course by id >-----------------------------
        /*
         * - note that Delete does not send back a view, but
         *   simply redirects back to the Index view.
         */

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteStory(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var story = context_.Stories.Find(id);
            try
            {
                if (story != null)
                {
                    context_.Remove(story);
                    context_.SaveChanges();
                }
            }
            catch (Exception)
            {
                // nothing for now
            }
            return RedirectToAction("Index");
            //return View(story);
        }
        //----< shows details for each course >----------------------

        public ActionResult StoryDetails(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Story story = context_.Stories.Find(id);

            if (story == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            var comments = context_.Comments.Where(l => l.Story == story);

            story.Comments = comments.OrderBy(l => l.PostDate).Select(l => l).ToList<Comment>();
            //course.Lectures = lects.ToList<Lecture>();

            if (story.Comments == null)
            {
                story.Comments = new List<Comment>();
                Comment cmt = new Comment();
                cmt.PostDate = DateTime.Now;
                cmt.Content = "none";
                story.Comments.Add(cmt);
            }
            return View(story);
        }

        //----< gets form to edit a specific course via id >---------

        [HttpGet]
        public IActionResult EditStory(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Story story = context_.Stories.Find(id);
            if (story == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(story);
        }

        //----< posts back edited results for specific course >------

        [HttpPost]
        public IActionResult EditStory(int? id, Story sty)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var story = context_.Stories.Find(id);
            if (story != null)
            {
                story.Description = sty.Description;
                story.Title = sty.Title;
                story.Privacy = sty.Privacy;

                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("Index");
        }
        //----< shows form for creating a lecture >------------------

        [HttpGet]
        public IActionResult CreateComment(int id)
        {
            var comment = new Comment();
            return View(comment);
        }

        //----< posts back new courses details >---------------------

        [HttpPost]
        public IActionResult CreateComment(int id, Comment cmt)
        {
            context_.Comments.Add(cmt);
            context_.SaveChanges();
            return RedirectToAction("Comments");
        }

        //----< add new lecture to course >--------------------------

        [HttpGet]
        public IActionResult AddComment(int id)
        {
            HttpContext.Session.SetInt32(sessionId_, id);

            // this works too
            // TempData[sessionId_] = id;

            Story story = context_.Stories.Find(id);
            if (story == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Comment cmt = new Comment();
            return View(cmt);
        }

        //----< Add new lecture to course >--------------------------

        [HttpPost]
        public IActionResult AddComment(int? id, Comment cmt)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            // retreive the target course from static field

            int? storyId_ = HttpContext.Session.GetInt32(sessionId_);

            // this works too
            // int courseId_ = (int)TempData[sessionId_];

            var story = context_.Stories.Find(storyId_);

            if (story != null)
            {
                if (story.Comments == null)  // doesn't have any lectures yet
                {
                    List<Comment> comments = new List<Comment>();
                    story.Comments = comments;
                }
                story.Comments.Add(cmt);

                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("Index");
        }

        //----< gets form to edit a specific lecture via id >---------

        [HttpGet]
        public IActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Comment comment = context_.Comments.Find(id);

            if (comment == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(comment);
        }

        //----< posts back edited results for specific lecture >------

        [HttpPost]
        public IActionResult EditComment(int? id, Comment cmt)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var comment = context_.Comments.Find(id);

            if (comment != null)
            {
                comment.Content = cmt.Content;

                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("Index");
        }

        //----< deletes a lecture by id >-----------------------------
        /*
         * - note that Delete does not send back a view, but
         *   simply redirects back to the Index view, which 
         *   will not show the deleted lecture.
         */

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var comment = context_.Comments.Find(id);
                if (comment != null)
                {
                    context_.Remove(comment);
                    context_.SaveChanges();
                }
            }
            catch (Exception)
            {
                // nothing for now
            }
            return RedirectToAction("Index");
        }

        //----< wizard generated actions >---------------------------

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
