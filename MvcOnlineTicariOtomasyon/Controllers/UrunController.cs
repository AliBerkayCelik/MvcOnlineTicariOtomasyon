using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x=>x.Durum==true).ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList() select new SelectListItem
            {
                Text =x.KetegoriAd,
                Value=x.KategoriID.ToString()
            }).ToList();
            ViewBag.dgr1 = deger1;
           return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
			List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
										   select new SelectListItem
										   {
											   Text = x.KetegoriAd,
											   Value = x.KategoriID.ToString()
										   }).ToList();
			ViewBag.dgr1 = deger1;
			var urunDeger = c.Uruns.Find(id);
            return View("UrunGetir", urunDeger);
        }
        
        public ActionResult UrunGuncelle(Urun u)
        {
            var urun=c.Uruns.Find(u.UrunID);
            urun.AlisFiyat = u.AlisFiyat;
            urun.Durum = u.Durum;
            urun.Kategoriid = u.Kategoriid;
            urun.Marka=u.Marka;
            urun.SatisFiyat=u.SatisFiyat;
            urun.Stok = u.Stok;
            urun.UrunAd = u.UrunAd;
            urun.UrunGorsel = u.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}