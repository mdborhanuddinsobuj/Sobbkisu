using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EV.Data;
using EV.Models;

namespace EV.Controllers
{
    public class CategoriesController : Controller
    {
        private EvDbMahmudSabujContext db = new EvDbMahmudSabujContext();

        // GET: Categories
        public ActionResult Index()
        {
            if (Session["fname"] != null)
            {
                ViewBag.Cactive = "active";
                return View(db.Categories.ToList());
            }
            else
            {
                return Redirect("/User/Login");
            }
            
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Cactive = "active";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.Cactive = "active";
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cId,cName")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(categories);
                if (db.SaveChanges()>0)
                {
                    return PartialView("_success");
                }
                else
                {
                    return PartialView("_error");
                }
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Cactive = "active";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cId,cName")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categories).State = EntityState.Modified;
                if (db.SaveChanges()>0)
                {
                    return PartialView("_success");
                }
                else
                {
                    return PartialView("_error");
                }
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Categories.Remove(categories);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //// POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Categories categories = db.Categories.Find(id);
        //    db.Categories.Remove(categories);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
