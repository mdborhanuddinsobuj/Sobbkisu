using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EV.Data;
using EV.Models;
using EV.ViewModel;

namespace EV.Controllers
{
    public class MedicinesController : Controller
    {
        private EvDbMahmudSabujContext db = new EvDbMahmudSabujContext();

        // GET: Medicines
        public ActionResult Index()
        {
            if (Session["fname"] != null)
            {
                ViewBag.Dactive = "active";

                var medicine = db.Medicines.Include(t => t.Categories);

                List<Medicines> items = medicine.ToList();

                MedicineVM medicineVM = new MedicineVM();

                List<MedicineVM> itemsVMsList = items.Select(x => new MedicineVM
                {
                    pId = x.pId,
                    pName = x.pName,
                    qty = x.qty,
                    price = x.price,
                    expiryDate = x.expiryDate,
                    pImage = x.pImage,
                    pStatus = x.pStatus,
                    cName = x.Categories.cName
                }).ToList();

                return View(itemsVMsList);
            }
            else
            {
                return Redirect("/User/Login");
            }
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            return View(medicines);
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            ViewBag.cId = new SelectList(db.Categories, "cId", "cName");
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pId,pName,qty,price,expiryDate,pImage,pStatus,cId,File")] Medicines medicines)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileName(medicines.File.FileName);//abc.jpg
                string _filename = DateTime.Now.ToString("hhmmssfff") + filename;//1235abc.jpg
                string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                medicines.pImage = _filename;

                db.Medicines.Add(medicines);
                if (db.SaveChanges() > 0)
                {
                    medicines.File.SaveAs(path);
                    return PartialView("_success");
                }
                else
                {
                    return PartialView("_error");
                }
            }

            ViewBag.cId = new SelectList(db.Categories, "cId", "cName", medicines.cId);
            return PartialView("_error");
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            ViewBag.cId = new SelectList(db.Categories, "cId", "cName", medicines.cId);
            return View(medicines);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pId,pName,qty,price,expiryDate,pImage,pStatus,cId,File")] Medicines medicines)
        {
            if (ModelState.IsValid)
            {
                if (medicines.File != null)
                {
                    string filename = Path.GetFileName(medicines.File.FileName);
                    string _filename = DateTime.Now.ToString("hhmmssfff") + filename;
                    string path = Path.Combine(Server.MapPath("~/Images/"), _filename);
                    medicines.pImage = _filename;
                    medicines.File.SaveAs(path);
                }

                db.Entry(medicines).State = EntityState.Modified;
                if (db.SaveChanges()>0)
                {
                    return RedirectToAction("Index");
                }
                
            }
            ViewBag.cId = new SelectList(db.Categories, "cId", "cName", medicines.cId);
            return View(medicines);
        }

        // GET: Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicines medicines = db.Medicines.Find(id);
            if (medicines == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Medicines.Remove(medicines);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //// POST: Medicines/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Medicines medicines = db.Medicines.Find(id);
        //    db.Medicines.Remove(medicines);
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
