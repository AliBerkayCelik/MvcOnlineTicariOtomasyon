using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
			
			var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.dgr = mail;
			var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			ViewBag.mid = mailid;
			var toplamSatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
			ViewBag.ts = toplamSatis;
			var toplamTutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
			ViewBag.tt = toplamTutar;
			var toplamUrun = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
			ViewBag.tu = toplamUrun;
			var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
			ViewBag.ad = adsoyad;
			return View(degerler);
            
        }
        public ActionResult Siparislerim()
        {
			var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
			return View(degerler);
        }
        public ActionResult GelenMesajlar()
        {
			var mail = (string)Session["CariMail"];
			var mesajlar = c.Mesajlars.Where(x=>x.Alici==mail).OrderByDescending(x => x.MesajID).ToList();
			var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidenSayisi;
			var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelenSayisi;
            return View(mesajlar);
        }
		public ActionResult GidenMesajlar()
		{
			var mail = (string)Session["CariMail"];
			var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.MesajID).ToList();
			var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelenSayisi;
			var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidenSayisi;
			return View(mesajlar);
		}
		public ActionResult MesajDetay(int id)
		{
			var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
			var mail = (string)Session["CariMail"];
			var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidenSayisi;
			var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelenSayisi;
			return View(degerler);
		}

		[HttpGet]
		public ActionResult YeniMesaj()
		{
			var mail = (string)Session["CariMail"];
			var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
			ViewBag.d2 = gidenSayisi;
			var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
			ViewBag.d1 = gelenSayisi;
			return View();
		}
		[HttpPost]
		public ActionResult YeniMesaj(mesajlar m)
		{
			var mail = (string)Session["CariMail"];
			m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
			m.Gonderici = mail;
			c.Mesajlars.Add(m);
			c.SaveChanges();
			return View();
		}
		public ActionResult KargoTakip(string p)
		{
			var values = from x in c.KargoDetays select x;
			
		
			values = values.Where(y => y.TakipKodu.Contains(p));
	
			return View(values.ToList());
			
		}
		public ActionResult CariKargoTakip(string id)
		{
			var degerler = c.KargoTakips.Where(x => x.Takipkodu == id).ToList();
			return View(degerler);
		}
		public ActionResult LogOut()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "Login");
		}
		public PartialViewResult Partial1()
		{
			var mail = (string)Session["CariMail"];
			var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
			var cariBul = c.Carilers.Find(id);
			return PartialView("Partial1",cariBul);
		}
		public ActionResult CariBilgiGuncelle(Cariler cr)
		{
			var cari = c.Carilers.Find(cr.CariID);
			cari.CariAd = cr.CariAd;
			cari.CariSoyad = cr.CariSoyad;
			cari.CariSifre = cr.CariSifre;
			c.SaveChanges();
			return RedirectToAction("Index");
		}
		
	}
}