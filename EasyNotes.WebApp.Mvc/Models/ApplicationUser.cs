using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyNotes.WebApp.Mvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Office Phone")]
        public string ? PhoneNumber2 { get; set; }
        [DisplayName("First Name")]
        public string ? FirstName { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}
