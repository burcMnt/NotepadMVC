using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotepadMVC.Data;
using NotepadMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace NotepadMVC.ViewComponents
{ //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0
    public class ProfilePhotoViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilePhotoViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int width1,int height1,string class1)
        {
            var userId = ((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            var vm = new ProfilePhotoComponentViewModel()
            {
                FileName = user.Photo,
                Width1 = width1,
                Height1 = height1,
                Class1 = class1
            };
            return View(vm);
        }
    }
}
