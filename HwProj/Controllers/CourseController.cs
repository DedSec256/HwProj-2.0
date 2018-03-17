﻿using HwProj.Models;
using HwProj.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HwProj.Controllers
{
    public class CourseController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course model)
        {
            if (!User.Identity.IsAuthenticated)
                return View("Error", new Error() { Message = "Пользователь не авторизирован" });

            if (ModelState.IsValid)
            {
                using (var db = new EduContext())
                {
                    db.Courses.Add(new Course
                    {
                        Name = model.Name,
                        GroupName = model.GroupName,
                        IsComplete = false
                    });
                    db.SaveChanges();
                }
                ModelState.AddModelError("", "Курс успешно создан");
            }
            return View(model);
        }
    }
}