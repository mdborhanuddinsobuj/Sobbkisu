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
    public class UserController : Controller
    {
        private EvDbMahmudSabujContext db = new EvDbMahmudSabujContext();

        // GET: User
        public ActionResult Index()
        {
            return View(db.Registers.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Register register = db.Registers.Find(id);
            if (register == null)
            {
                return HttpNotFound();
            }
            return View(register);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["fname"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }   
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailID,FirstName,LastName,DateOfBirth,Password,ConfirmPassword")] Register register)
        {
            if (ModelState.IsValid)
            {
                db.Registers.Add(register);
                db.SaveChanges();
                Session["fname"] = register.FirstName.ToString();
                Session["lname"] = register.LastName.ToString();
                return RedirectToAction("Index","Medicines");
            }

            return View(register);
        }

        public ActionResult Login()
        {
            if (Session["fname"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Medicines");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Registers.Where(a => a.EmailID.Equals(login.Username) && a.Password.Equals(login.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["fname"] = obj.FirstName.ToString();
                    Session["lname"] = obj.LastName.ToString();
                    return Redirect("/Medicines");
                }
            }
            return View(login);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
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
