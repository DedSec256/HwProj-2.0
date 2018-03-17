﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HwProj.Models;
using HwProj.Models.Contexts;
using HwProj.Repository;

namespace HwProj.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Edit()
        {
	        if (!User.Identity.IsAuthenticated)
				ModelState.AddModelError("", "Ошибка авторизации");

	        User dbUser = UserRepository.Instance.Get(u => u.Email == User.Identity.Name);
	        if (dbUser != null)
	        {
		        return View(dbUser);
	        }
	        else
	        {
				ModelState.AddModelError("", "Пользователь не найден");
			}
	        return View();
        }

		[HttpPut]
	    public ActionResult Edit(User user)
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    User dbUser = null;
			    using (var db = new EduContext())
			    {
				    dbUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
			    }
			    if (dbUser != null)
			    {
				    if (true)//user.EncryptedPassword == dbUser.EncryptedPassword)
				    {
						using (var db = new EduContext())
						{
							db.Entry(user).State = EntityState.Modified;
							db.SaveChanges();
						}
					}
				    else
				    {
						return View("Error");
					}
			    }
			    else
			    {
				    ModelState.AddModelError("", "Пользователь не найден");
			    }
		    }
		    else
		    {
				return View("Error");
			}
		    return View(user);
	    }
	}
}