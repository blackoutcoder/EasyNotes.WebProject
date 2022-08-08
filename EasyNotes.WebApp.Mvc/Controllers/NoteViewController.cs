using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Helpers;
using EasyNotes.WebApp.Mvc.Models;
using EasyNotes.WebApp.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class NoteViewController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment? _hostingEnvironment;
        [BindProperty]
        public NoteViewModel NoteVM { get; set; }

        public NoteViewController(ApplicationDbContext db)
        {
            _db = db;
           // _hostingEnvironment = hostingEnvironment;
            NoteVM = new NoteViewModel()
            {
                Categories = _db.Categories.ToList(),
                VNote = new Note()
            };
        }
        [Authorize(Roles = Roles.Executive)]
        public IActionResult Index()
        {
            var categoryList = new List<Note>();
            var userName = User.Identity.Name;
            //IEnumerable<Note> objNotesList = _context.Notes.ToList();
            foreach (Note note in _db.Notes)
            {
                if (note.UserName == userName)
                {
                    categoryList.Add(note);
                }
            }
            return View(categoryList);
            /*var notesList = new List<Note>();
            var model = _db.Notes.Include(m => m.Category);
            return View(model) ;*/
        }

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Notes/ShowSearchResults

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _db.Notes.Where(j => j.Name.Contains(SearchPhrase)).ToListAsync());
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
            NoteVM.VNote.UserName = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                
                return View(NoteVM);
            }
            _db.Notes.Add(NoteVM.VNote);
            _db.SaveChanges();

           /* var NoteID = NoteVM.VNote.Id;

            string wwrootPath = _hostingEnvironment.ContentRootPath;
            var files = HttpContext.Request.Form.Files;
            var SavedNote = _db.Notes.Find(NoteID);
            if (files.Count != 0)
            {
                var imagePath = @"images\note\";
                var Extention = Path.GetExtension(files[0].FileName);
                var RelativeImagePat = imagePath + NoteID + Extention;
                var AbsImagePath = Path.Combine(wwrootPath, RelativeImagePat);

                using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                    _db.SaveChanges();
                }
            }*/


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
