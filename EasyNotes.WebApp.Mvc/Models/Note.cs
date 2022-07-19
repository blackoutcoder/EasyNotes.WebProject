using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class Note
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        public string Content { set; get; }
        public string Img { set; get; }
        public string CategoryName { set; get; }
        public virtual Category Category { set; get; }
        public Guid UserID { set; get; }
    }
}
