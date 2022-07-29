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
            var notesList = new List<Note>();
            var userName = User.Identity.Name;
            //IEnumerable<Note> objNotesList = _context.Notes.ToList();
            foreach(Note note in _context.Notes)
            {
                if (note.UserName == userName)
                {
                    notesList.Add(note);
                }
            }
            return View(notesList);
        }

        public IActionResult UploadImage()
        {
            return View();
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Note note)
        {
            
            if (ModelState.IsValid)
            {
                note.UserName = User.Identity.Name.ToLower();
                note.Category.UserName = User.Identity.Name.ToLower();
                _context.Notes.Add(note);
                _context.SaveChanges();
                TempData["success"] = "Note successfully created!";
                return RedirectToAction("Index");
            }
            
            return View(note);
        }

        //GET
        public IActionResult Edit(Guid? id)
        {
            if(id == null || id == Guid.Empty)
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
        public IActionResult Edit(Note note)
        {

            if (ModelState.IsValid)
            {
                /*obj.UserName = User.Identity.Name.ToLower();
                obj.Category.UserName = User.Identity.Name.ToLower();*/
                
                _context.Notes.Update(note);
                _context.SaveChanges();
                TempData["success"] = "Note successfully updated!";
                return RedirectToAction("Index");
            }

            return View(note);
        }

        //GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null || id == Guid.Empty)
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
        public IActionResult DeletePOST(Guid? id)
        {
            var obj = _context.Notes.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _context.Notes.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Note successfully deleted!";
            return RedirectToAction("Index");
        }
    }
}
