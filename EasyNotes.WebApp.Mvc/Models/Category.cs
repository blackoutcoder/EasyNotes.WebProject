using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Catergory Name")]
        public string CategoryName { get; set; }
        public string ?Description { get; set; }
        public string? UserName { get; set; }
        public  ICollection<Note> ?Notes { set; get; }

    }
}
