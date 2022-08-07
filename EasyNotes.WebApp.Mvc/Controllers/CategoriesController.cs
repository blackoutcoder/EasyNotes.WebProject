using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using EasyNotes.WebApp.Mvc.Helpers;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Roles.Executive)]
        public IActionResult Index()
        {
            var categoryList = new List<Category>();
            var userName = User.Identity.Name;
            //IEnumerable<Note> objNotesList = _context.Notes.ToList();
            foreach (Category category in _context.Categories)
            {
                if (category.UserName == userName)
                {
                    categoryList.Add(category);
                }
            }
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                obj.Id = Guid.NewGuid();
                obj.Notes = new List<Note>();
                obj.UserName = User.Identity.Name.ToLower();
                _context.Categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category successfully created!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            /*var noteFromDbFirst = _context.Notes.FirstOrDefault(x => x.Id == id);
            var noteFromDbSingle = _context.Notes.SingleOrDefault(x => x.Id == id);*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
            
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            
            if (ModelState.IsValid)
            {
                obj.UserName = User.Identity.Name.ToLower();
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category successfully updated!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);
            /*var noteFromDbFirst = _context.Notes.FirstOrDefault(x => x.Id == id);
            var noteFromDbSingle = _context.Notes.SingleOrDefault(x => x.Id == id);*/

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid id)
        {
            var obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Category successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
