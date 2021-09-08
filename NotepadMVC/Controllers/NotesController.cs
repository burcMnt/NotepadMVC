using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadMVC.Data;
using NotepadMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotepadMVC.Controllers
{
    [Authorize] //giriş yapıldıgını kontrol eder
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext db;

        public NotesController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Yeni()
        {
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Yeni(Note note)
        {
            if (ModelState.IsValid)
            {
                note.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value; // Kimin giriş yaptıgını söyler ki
                db.Notes.Add(note);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }
            NoteViewModel nvm = new NoteViewModel();
            nvm.Id = note.Id;
            nvm.Title = note.Title;
            nvm.Content = note.Content;
            return View(nvm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Note note1 = db.Notes.FirstOrDefault(x => x.Id.Equals(model.Id));
                db.Notes.Remove(note1);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Edit(int id)
        {
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }
            NoteViewModel nvm = new NoteViewModel();
            nvm.Id = note.Id;
            nvm.Title = note.Title;
            nvm.Content = note.Content;
            return View(nvm);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Note note1 = db.Notes.FirstOrDefault(x => x.Id.Equals(model.Id));
                note1.Content = model.Content;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
