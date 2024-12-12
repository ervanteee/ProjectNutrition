using ProjectNutrition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectNutrition.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin objAdmin)
        {

            if (ModelState.IsValid)
            {
                using (Nutrition_DBEntities db = new Nutrition_DBEntities())
                {
                    var obj = db.Admins.Where(a => a.Username.Equals(objAdmin.Username) && a.Password.Equals(objAdmin.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Username"] = obj.Username.ToString();
                        Session["Nama"] = obj.Nama.ToString();
                        return RedirectToAction("AdminDashboard");
                    }
                    else
                    {
                        ViewBag.Message = "Login Failed";
                    }
                }
            }
            return View(objAdmin);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Admin objAdmin)
        {
            if (ModelState.IsValid)
            {
                using (Nutrition_DBEntities db = new Nutrition_DBEntities())
                {
                    // Check if the username already exists
                    var existingUser = db.Admins.Where(a => a.Username.Equals(objAdmin.Username)).FirstOrDefault();
                    if (existingUser == null)
                    {
                        db.Admins.Add(objAdmin);
                        db.SaveChanges();
                        ViewBag.Message = "Registration successful. Please log in.";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Message = "Username already exists. Please choose another username.";
                    }
                }
            }
            return View(objAdmin);
        }

        public ActionResult AdminDashboard()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}
