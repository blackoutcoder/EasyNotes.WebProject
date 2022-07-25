using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            IEnumerable<Note> objNotesList = _context.Notes.ToList();
            



            return View(objNotesList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Note obj)
        {
            
            if (ModelState.IsValid)
            {
                obj.UserName = User.Identity.Name.ToLower();
                _context.Notes.Add(obj);
                _context.SaveChanges();
                TempData["Success"] = "Note successfully created!";
                return RedirectToAction("Index");
            }
            
            return View(obj);
        }

        //GET
        public IActionResult Edit(uint? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var noteFromDb = _context.Notes.Find(id);
            /*var noteFromDbFirst = _context.Notes.FirstOrDefault(x => x.Id == id);
            var noteFromDbSingle = _context.Notes.SingleOrDefault(x => x.Id == id);*/

            if(noteFromDb == null)
            {
                return NotFound();
            }
            return View(noteFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Note obj)
        {

            if (ModelState.IsValid)
            {
                _context.Notes.Update(obj);
                _context.SaveChanges();
                TempData["Success"] = "Note successfully updated!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET
        public IActionResult Delete(uint? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var noteFromDb = _context.Notes.Find(id);
            /*var noteFromDbFirst = _context.Notes.FirstOrDefault(x => x.Id == id);
            var noteFromDbSingle = _context.Notes.SingleOrDefault(x => x.Id == id);*/

            if (noteFromDb == null)
            {
                return NotFound();
            }
            return View(noteFromDb);
        }

        //POST
        [HttpPost, ActionName("DeletePOST")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(uint? id)
        {
            var obj = _context.Notes.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _context.Notes.Remove(obj);
            _context.SaveChanges();
            TempData["Success"] = "Note successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
