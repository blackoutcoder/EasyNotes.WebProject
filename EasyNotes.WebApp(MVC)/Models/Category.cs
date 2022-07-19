namespace EasyNotes.WebApp_MVC_.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Note> Notes { get; set; }
    }
}
