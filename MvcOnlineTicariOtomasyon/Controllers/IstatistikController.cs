using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context c = new Context();
        // GET: Istatistik
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.dgr1 = deger1;
			var deger2 = c.Uruns.Count().ToString();
			ViewBag.dgr2 = deger2;
			var deger3 = c.Personels.Count().ToString();
			ViewBag.dgr3 = deger3;
			var deger4 = c.Kategoris.Count().ToString();
			ViewBag.dgr4 = deger4;
			var deger5 = c.Uruns.Sum(x=>x.Stok).ToString();
			ViewBag.dgr5 = deger5;
			var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
			ViewBag.dgr6 = deger6;
			var deger7 = c.Uruns.Count(x => x.Stok<=20).ToString();
			ViewBag.dgr7 = deger7;
			var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
			ViewBag.dgr8 = deger8;
			var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
			ViewBag.dgr9 = deger9;
			var deger10 = c.Uruns.Count(x => x.UrunAd=="Buzdolabı").ToString();
			ViewBag.dgr10 = deger10;
			var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
			ViewBag.dgr11 = deger11;
			var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
			ViewBag.dgr12 = deger12;
			var deger13=c.Uruns.Where(u=>u.UrunID==(c.SatisHarekets.GroupBy(x=>x.Urunid).OrderByDescending(z=>z.Count()).Select(y=>y.Key).FirstOrDefault())).Select(k=>k.UrunAd).FirstOrDefault();
			ViewBag.dgr13 = deger13;
			var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
			ViewBag.dgr14 = deger14;

			DateTime bugun = DateTime.Today;
			var deger15 = c.SatisHarekets.Count(x => x.Tarih ==bugun).ToString();
			ViewBag.dgr15 = deger15;
			var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => y.ToplamTutar).ToString();
			ViewBag.dgr16 = deger16;
			return View();
        }
		public ActionResult KolayTablolar()
		{
			var sorgu = from x in c.Carilers
						group x by x.CariSehir into g
						orderby g.Count() descending
						select new SinifGrup
						{
							Sehir = g.Key,
							Sayi = g.Count()
						};
			return View(sorgu);
		}
		public PartialViewResult Partial1()
		{
			var sorgu2 = from x in c.Personels
						 group x by x.Departman.DepartmanAdi into g
						 orderby g.Count() descending
						 select new SinifGrup2
						 {
							 Departman = g.Key,
							 Sayi = g.Count()
					   };
			return PartialView(sorgu2);
		}
		public PartialViewResult Partial2()
		{
			var sorgu = c.Carilers.ToList();
			return PartialView(sorgu);
		}
		public PartialViewResult Partial3()
		{
			var sorgu = c.Uruns.ToList();
			return PartialView(sorgu);
		}
		public PartialViewResult Partial4()
		{
			var sorgu3 = from x in c.Uruns
						 group x by x.Marka into g
						 
						 select new SinifGrup3
						 {
							 Marka = g.Key,
							 Sayi = g.Count()
						 };
			
			return PartialView(sorgu3);

		}
	}
}