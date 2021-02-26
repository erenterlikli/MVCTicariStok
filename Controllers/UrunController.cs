using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStok.Models.Entity;
//PagedList kullanabilmek için kütüphaneler.
using PagedList;
using PagedList.Mvc;

namespace MVCStok.Controllers
{
    public class UrunController : Controller
    {
        StokTakipEntities db = new StokTakipEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.Urunler.ToList().ToPagedList(sayfa,10); //ilk sayı kaçıncı sayfa olduğu, ikincisi ise kaçar tane bulunduğu.
            return View(degerler);
        }

        [HttpPost]
        public ActionResult YeniUrun(Urunler p1)
        {
            var ktg = db.Kategoriler.Where(m => m.KategoriId == p1.Kategoriler.KategoriId).FirstOrDefault();
            p1.Kategoriler = ktg;        
            db.Urunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> degerim = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();

            ViewBag.deg = degerim;

            var urun = db.Urunler.Find(id);
            return View("UrunGetir", urun);
        }

        public ActionResult Guncelle(Urunler p)
        {
            var urun = db.Urunler.Find(p.UrunId);
           
            urun.UrunAd = p.UrunAd;
            urun.Fiyat = p.Fiyat;
            urun.Marka = p.Marka;
            urun.Stok = p.Stok;

            var ktg = db.Kategoriler.Where(m => m.KategoriId == p.Kategoriler.KategoriId).FirstOrDefault();  //kategoriyi de kaydetme işlemi.
            urun.UrunKategori = ktg.KategoriId;

            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            //Linq sorgusu yazarak kategorideki değerleri listeye atadık.

            ViewBag.dgr = degerler; //diğer sayfalara taşınma işlemi için yapıldı.
                                
            return View();
        }

    }
}