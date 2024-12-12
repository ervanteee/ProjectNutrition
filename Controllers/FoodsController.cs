using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectNutrition.Models;

namespace ProjectNutrition.Controllers
{
    public class FoodsController : Controller
    {
        private Nutrition_DBEntities db = new Nutrition_DBEntities();

        // GET: Foods
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.Grade1);
            return View(foods.ToList());
        }

        // GET: Foods/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            var food = new Food();
            if (Session["Username"] != null)
            {
                food.Admin = Session["Username"].ToString();
            }
            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1");
            return View(food);
        }

        // POST: Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Company,Grade,Admin")] Food food, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Contents/Images"), fileName);
                    file.SaveAs(path);
                    food.Img_Path = fileName;
                }

                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", food.Grade);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }

            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", food.Grade);
            return View(food);
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Company,Grade,Admin,Img_Path")] Food food, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Contents/Images"), fileName);
                    file.SaveAs(path);
                    food.Img_Path = fileName;
                }

                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Grade = new SelectList(db.Grades, "Grade1", "Grade1", food.Grade);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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
