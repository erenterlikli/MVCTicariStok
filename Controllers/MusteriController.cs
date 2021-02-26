using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;
//Sayfalama için kütüphaneler.
using PagedList;
using PagedList.Mvc;


namespace MVCStok.Controllers
{
    public class MusteriController : Controller
    {
        StokTakipEntities db = new StokTakipEntities();

        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.Musteriler.ToList().ToPagedList(sayfa, 10);
            return View(degerler);
            
        }

        [HttpPost] //butona basılınca eklensin
        public ActionResult YeniMusteri(Musteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Musteriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.Musteriler.Find(id);
            db.Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.Musteriler.Find(id);
            return View("MusteriGetir", mus);
        }

        public ActionResult Guncelle(Musteriler m1)
        {
            var mstr = db.Musteriler.Find(m1.MusteriId);
            mstr.MusteriAd = m1.MusteriAd;
            mstr.MusteriSoyad = m1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet] //sayfa yüklenirse
        public ActionResult YeniMusteri()
        {
            return View();
        }


    }
}