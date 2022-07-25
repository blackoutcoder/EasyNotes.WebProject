using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class PublicNote
    {
        [Key]
        public Guid Id { set; get; }
        [Required]
        public string Name { set; get; }
        [DisplayName("Message")]
        [Required]
        public string Content { set; get; }
        [DisplayName("Create Date/Time")]
        public DateTime CreateDate { set; get; }

    }
}
