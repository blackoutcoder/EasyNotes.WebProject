using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class Category
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string ?Description { get; set; }
        //public List<Note> ?Notes { get; set; }
        public Guid ?UserID { get; set; }
    }
}
