namespace EasyNotes.WebApp.Mvc.Models.ViewModels
{
    public class NoteViewModel
    {
        public Note Note { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
