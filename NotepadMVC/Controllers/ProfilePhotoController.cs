using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotepadMVC.Data;
using NotepadMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotepadMVC.Controllers
{ //https://docs.microsoft.com/tr-tr/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
    [Authorize]
    public class ProfilePhotoController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly ApplicationDbContext db;

        public ProfilePhotoController(IWebHostEnvironment env, ApplicationDbContext db)
        {
            this.env = env;
            this.db = db;
        }
        public IActionResult Index(string result)
        {
            var vm = new ProfilePhotoViewModel()
            {
                Uploaded = result == "Successfuly uploaded",
                Photo = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).Photo

            };
            return View(vm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                ModelState.AddModelError("photo", " Please select image file fisrt.");
            }
            else if (!photo.ContentType.StartsWith("image/"))
            {
                ModelState.AddModelError("photo", "Wrong type of file! , Please select image file.");
            }
            //izin verilen en büyük dosya boyutu 1MB
            else if (photo.Length > 1 * 1000 * 1000)
            {
                ModelState.AddModelError("photo", "Maximum file size 1MB! , Please choose image size under 1MB.");

            }
            if (ModelState.IsValid)
            {
                string uzanti = Path.GetExtension(photo.FileName);
                string newFileName = Guid.NewGuid() + uzanti;
                string filePath = Path.Combine(env.WebRootPath, "uploads", newFileName);
                using (var fs = System.IO.File.Create(filePath))
                {
                    photo.CopyTo(fs);
                }
                ApplicationUser user = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
                user.Photo = newFileName;
                db.SaveChanges();
                return RedirectToAction("Index", new { result = "Successfuly uploaded" });
            }
            var vm = new ProfilePhotoViewModel()
            {
                Photo = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).Photo

            };
            return View(vm);
        }
    }
}
