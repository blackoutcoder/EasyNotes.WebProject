using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class Note
    {
        [Key]
        public uint Id { set; get; }
        [Required]
        public string Name { set; get; }
        [DisplayName("Message")]
        public string Content { set; get; }
        [DisplayName("Picture")]
        public string ?Img { set; get; }
        [DisplayName("Category")]
        public string ?CategoryName { set; get; }
        public virtual List<Category> ?Categories { get; set; }
        public virtual Category ?Category { set; get; }
        public string? UserName { set; get; }
    }
}
