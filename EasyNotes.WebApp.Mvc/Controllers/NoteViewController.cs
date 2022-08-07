using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Helpers;
using EasyNotes.WebApp.Mvc.Models;
using EasyNotes.WebApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NoteViewController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public NoteViewModel NoteVM { get; set; }

        public NoteViewController(ApplicationDbContext db)
        {
            _db = db;
            NoteVM = new NoteViewModel()
            {
                Categories = _db.Categories.ToList(),
                VNote = new Note()
            };
        }
        [Authorize(Roles = Roles.Executive)]
        public IActionResult Index()
        {
            
            var notesList = new List<Note>();
            var model = _db.Notes.Include(m => m.Category);
           
            return View(model);
        }
        //HTTPGET
        public IActionResult Create()
        {
            return View(NoteVM);
        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(NoteVM);
            }
            _db.Notes.Add(NoteVM.VNote);
            _db.SaveChanges();
            TempData["success"] = "Note successfully created!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            NoteVM.VNote = _db.Notes.Include(m => m.Category).SingleOrDefault(m => m.Id == id);
            if(NoteVM.VNote == null)
            {
                return NotFound();
            }
            return View(NoteVM);
        }
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            if(!ModelState.IsValid)
            {
                return View(NoteVM);
            }

            _db.Update(NoteVM.VNote);
            _db.SaveChanges();
            TempData["success"] = "Note successfully updated!";
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete (Guid id)
        {
            Note note = _db.Notes.Find(id);
            if(note == null)
            {
                return NotFound();
            }
            _db.Notes.Remove(note);
            _db.SaveChanges();
            TempData["success"] = "Note successfully deleted!";
            return RedirectToAction(nameof(Index));
        }
    }
}
