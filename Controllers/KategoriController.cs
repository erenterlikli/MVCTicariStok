using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity; //kütüphane olarak tanıttık.
//Sayfalama için kütüphaneler
using PagedList;
using PagedList.Mvc;

namespace MVCStok.Controllers
{
    public class KategoriController : Controller
    {
        StokTakipEntities db = new StokTakipEntities(); //veritabanı tablosunun adından nesne türettik.
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.Kategoriler.ToList().ToPagedList(sayfa,10); //bu tablodaki değerleri "degerler" diye bir değişkende tuttuk ve bunları listeledik.
            return View(degerler);
        }

        
        [HttpPost] //butona basılınca ekleme yapsın.
        public ActionResult YeniKategori(Kategoriler p1)
        {
            if(!ModelState.IsValid) //boşsa.
            {
                return View("YeniKategori");
            }

            db.Kategoriler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       public ActionResult KategoriGetir(int id)
        {
            var ktg = db.Kategoriler.Find(id);
            return View("KategoriGetir", ktg);
        }

       public ActionResult Guncelle(Kategoriler k1)
        {
            var ktgr = db.Kategoriler.Find(k1.KategoriId);
            ktgr.KategoriAd = k1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet] //sayfa yüklenince
        public ActionResult YeniKategori()
        {
            return View();
        }

    }
}