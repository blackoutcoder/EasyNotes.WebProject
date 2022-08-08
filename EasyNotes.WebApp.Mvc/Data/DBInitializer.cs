using EasyNotes.WebApp.Mvc.Helpers;
using EasyNotes.WebApp.Mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyNotes.WebApp.Mvc.Data
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;   
        public DBInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            //Add pending migration if exists
            if(_db.Database.GetPendingMigrations().Count()>0)
            {
                _db.Database.Migrate();
            }

            //Exit if role already exists
            if (_db.Roles.Any(r => r.Name == Helpers.Roles.Admin)) return;

            //Create Admin role
            _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();

            //Create Admin role
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName="Admin",
                Email="Admin@gmail.com",
                EmailConfirmed=true,
                
            },"Admin@easynotes").GetAwaiter().GetResult();

            //Assign role to Admin
            await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync("Admin"), Roles.Admin);
          
        }
    }
}
