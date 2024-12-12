using ProjectNutrition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectNutrition.Controllers
{
    public class UserController : Controller
    {
        private Nutrition_DBEntities db = new Nutrition_DBEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            var feedback = new Feedback();
            // Assuming 'UsernameUser' session contains the user's username
            if (Session["UsernameUser"] != null)
            {
                feedback.Username = Session["UsernameUser"].ToString();
            }

            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Feedback([Bind(Include = "Username, Comment")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback); // Ensure your DB context has a Feedbacks DbSet
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (Nutrition_DBEntities db = new Nutrition_DBEntities())
                {
                    // Check if the username already exists
                    var existingUser = db.Users.Where(a => a.Username.Equals(objUser.Username)).FirstOrDefault();
                    if (existingUser == null)
                    {
                        db.Users.Add(objUser);
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
            return View(objUser);
        }
        public ActionResult Article()
        {
            return View();
        }
        public ActionResult ArticleDetail()
        {
            return View();
        }
        public ActionResult Product()
        {
            using (Nutrition_DBEntities db = new Nutrition_DBEntities())
            {
                List<Drink> drinks = db.Drinks.ToList();
                List<Food> foods = db.Foods.ToList();

                var viewModel = new ViewModel
                {
                    Drinks = drinks,
                    Foods = foods
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {

            if (ModelState.IsValid)
            {
                using (Nutrition_DBEntities db = new Nutrition_DBEntities())
                {
                    var obj = db.Users.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UsernameUser"] = obj.Username.ToString();
                        Session["NamaUser"] = obj.Nama.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Login Failed";
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}