using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Faturalars.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }
		[HttpPost]
		public ActionResult FaturaEkle(Faturalar f)
		{
            c.Faturalars.Add(f);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult FaturaGetir(int id) 
        {
            var fatura = c.Faturalars.Find(id);
            return View("FaturaGetir",fatura);

        }
        public ActionResult FaturaGuncelle(Faturalar f)
        {
            var fatura = c.Faturalars.Find(f.FaturaID);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSiraNo = f.FaturaSiraNo;
            fatura.Saat = f.Saat;
            fatura.Tarih = f.Tarih;
            fatura.TeslimAlan = f.TeslimAlan;
            fatura.TeslimEden = f.TeslimEden;
            fatura.VergiDairesi = f.VergiDairesi;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetay(int id)
        {
			var deger = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
			
			return View(deger);
		}
        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }
		[HttpPost]
		public ActionResult YeniKalem(FaturaKalem p)
		{

            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
		}
        public ActionResult Dinamik()
        {
            Class3 cs = new Class3();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo,DateTime Tarih,string VergiDairesi,string Saat,string TeslimEden,string TeslimAlan, string Toplam, FaturaKalem[] kalem)
        {
			Faturalar f = new Faturalar();
			f.FaturaSeriNo = FaturaSeriNo;
			f.FaturaSiraNo = FaturaSiraNo;
			f.Tarih = Tarih;
			f.VergiDairesi = VergiDairesi;
			f.Saat = Saat;
			f.TeslimEden = TeslimEden;
			f.TeslimAlan = TeslimAlan;
			f.Toplam = decimal.Parse(Toplam);
			c.Faturalars.Add(f);

			foreach (var item in kalem)
			{
				FaturaKalem fk = new FaturaKalem();
				fk.Aciklama = item.Aciklama;
				fk.BirimFiyat = item.BirimFiyat;
				fk.Faturaid = item.Faturaid;
				fk.Miktar = item.Miktar;
				fk.Tutar = item.Tutar;
				c.FaturaKalems.Add(fk);

			}
			c.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);

		}
	}
}