﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotepadMVC.Areas.Admin.Models;
using NotepadMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotepadMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext db;

        public DashboardController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var vm = new DashboardViewModel()
            {
                NoteCount = db.Notes.Count(),
                MemberCount = db.Users.Count()

            };
            return View(vm);
        }
    }
}
