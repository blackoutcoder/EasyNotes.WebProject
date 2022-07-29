using Microsoft.AspNetCore.Identity.UI.Services;

namespace EasyNotes.WebApp.Mvc.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //TODO Create Email Service
            await Task.CompletedTask;
        }
    }
}
