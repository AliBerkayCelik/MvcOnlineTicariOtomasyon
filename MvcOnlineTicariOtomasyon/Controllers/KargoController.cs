using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();
        // GET: Kargo
        public ActionResult Index(string p)
        {
			var values = from x in c.KargoDetays select x;
			if (!string.IsNullOrEmpty(p))
			{
				values = values.Where(y => y.TakipKodu.Contains(p));
			}
			return View(values.ToList());
		}
		[HttpGet]
		public ActionResult KargoEkle()
		{
			Random rnd = new Random();
			string[] karakterler = { "A", "B", "C", "D" };
			int k1, k2, k3;
			k1=rnd.Next(0,4);
			k2 = rnd.Next(0, 4);
			k3 = rnd.Next(0, 4);
			int s1, s2, s3;
			s1 = rnd.Next(10, 100);
			s2 = rnd.Next(10, 100);
			s3 = rnd.Next(10, 100);
			string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
			ViewBag.takipkod = kod;
			return View();
		}
		[HttpPost]
		public ActionResult KargoEkle(KargoDetay k)
		{
			c.KargoDetays.Add(k);
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult KargoTakip(string id)
		{
			var degerler = c.KargoTakips.Where(x => x.Takipkodu == id).ToList();
			return View(degerler);
		}
	}
}