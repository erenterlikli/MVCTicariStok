using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity; //kütüphaneyi ekledik.

namespace MVCStok.Controllers
{
    public class SatisController : Controller
    {
        StokTakipEntities db = new StokTakipEntities();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(Satis s1)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            db.Satis.Add(s1);
            db.SaveChanges();
            return View("Index");
        }
    }
}