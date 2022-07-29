using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyNotes.WebAplication.Models
{
    public class Note
    {
        [Key]
        public Guid Id { set; get; }
        [Required]
        public string Name { set; get; }
        [DisplayName("Message")]
        public string Content { set; get; }
        [DisplayName("Picture")]
        public string? Img { set; get; }
        [DisplayName("Category")]
        [ForeignKey("CategoryId")]
        public Category? Category { set; get; }

        public Note() 
        {
        }
    }
}
