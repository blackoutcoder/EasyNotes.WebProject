using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class Category
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        [DisplayName("Catergory Name")]
        public string CategoryName { get; set; }
        public string ?Description { get; set; }
        //public List<Note>? Notes { set; get; }
        public string? UserName { get; set; }
    }
}
