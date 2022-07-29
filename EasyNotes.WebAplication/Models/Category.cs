using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebAplication.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Catergory Name")]
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public ICollection<Note>? Notes { set; get; }

        public Category()
        {
            Notes = new List<Note>();
        }
        public override string ToString()
        {
            return $"{CategoryName}";
        }
    }
}
