using EasyNotes.WebApp.Mvc.Data;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyNotes.WebApp.Mvc.Controllers
{
    public class PublicNotesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublicNotesController(ApplicationDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            IEnumerable<PublicNote> objNotesList = _db.PublicNotes.ToList();
            return View(objNotesList);
        }
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: Notes/ShowSearchResults

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index",  _db.PublicNotes.Where(j => j.Id.Equals(SearchPhrase)).ToList());
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublicNote obj)
        {

            if (ModelState.IsValid)
            {
                obj.Id = Guid.NewGuid();
                obj.CreateDate = DateTime.Now;
                _db.PublicNotes.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Note successfully created!";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
