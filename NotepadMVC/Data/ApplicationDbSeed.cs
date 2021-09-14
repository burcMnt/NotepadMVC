using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadMVC.Data
{
    public static class ApplicationDbSeed
    {
        public static async Task SeedRolesAndUsers(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            //admin rolü henüz yoksa oluştur
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole() { Name = "admin" });
            }

            //admin@example.com kullanıcı yoksa oluştur ve sonra admin rolünü ata
            if (!await userManager.Users.AnyAsync(x => x.Email == "admin@example.com"))
            {
                ApplicationUser adminUser = new ApplicationUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser,"P@ssword1");
                await userManager.AddToRoleAsync(adminUser, "admin");
            }
        }
    }
}
