using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EV.Models;
using EV.Data;

namespace EV.Controllers
{
    public class HomeController : Controller
    {
        EvDbMahmudSabujContext db = new EvDbMahmudSabujContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}