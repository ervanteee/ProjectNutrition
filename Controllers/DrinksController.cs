using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectNutrition.Models;

namespace ProjectNutrition.Controllers
{
    public class DrinksController : Controller
    {
        private Nutrition_DBEntities db = new Nutrition_DBEntities();

        // GET: Drinks
        public ActionResult Index()
        {
            var drinks = db.Drinks.Include(d => d.Grade1);
            return View(drinks.ToList());
        }

        // GET: Drinks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // GET: Drinks/Create
        public ActionResult Create()
        {
            var drink = new Drink();
            if (Session["Username"] != null)
            {
                drink.Admin = Session["Username"].ToString();
            }
            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1");
            return View(drink);
        }

        // POST: Drinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Company,Grade,Admin,Img_Path")] Drink drink, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Contents/Images"), fileName);
                    file.SaveAs(path);
                    drink.Img_Path = fileName;
                }

                db.Drinks.Add(drink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", drink.Grade);
            return View(drink);
        }

        // GET: Drinks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }

            if (Session["Username"] != null)
            {
                drink.Admin = Session["Username"].ToString();
            }
            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", drink.Grade);
            return View(drink);
        }

        // POST: Drinks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Company,Grade,Admin,Img_Path")] Drink drink, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Contents/Images"), fileName);
                    file.SaveAs(path);
                    drink.Img_Path = fileName;
                }

                db.Entry(drink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", drink.Grade);
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = db.Drinks.Find(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Drink drink = db.Drinks.Find(id);
            db.Drinks.Remove(drink);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
