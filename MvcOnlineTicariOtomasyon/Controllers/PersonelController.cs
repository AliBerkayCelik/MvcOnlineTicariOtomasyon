using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
			List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
										   select new SelectListItem
										   {
											   Text = x.DepartmanAdi,
											   Value = x.DepartmanID.ToString()
										   }).ToList();
			ViewBag.dgr1 = deger1;
			return View();
			
        }
		[HttpPost]
		public ActionResult PersonelEkle(Personel p)
		{
			if (Request.Files.Count > 0)
			{
				string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
				string uzanti = Path.GetExtension(Request.Files[0].FileName);

				
				string klasorYolu = Server.MapPath("~/Image/");

				if (!Directory.Exists(klasorYolu))
				{
					Directory.CreateDirectory(klasorYolu);
				}

				
				string tamDosyaYolu = Path.Combine(klasorYolu, dosyaAdi);

				
				Request.Files[0].SaveAs(tamDosyaYolu);

				
				p.PersonelGorsel = "/Image/" + dosyaAdi;
			}

			c.Personels.Add(p);
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult PersonelGetir(int id)
		{
			List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
										   select new SelectListItem
										   {
											   Text = x.DepartmanAdi,
											   Value = x.DepartmanID.ToString()
										   }).ToList();
			ViewBag.dgr1 = deger1;
			var personel = c.Personels.Find(id);
			return View("PersonelGetir", personel);
		}

		public ActionResult PersonelGuncelle(Personel p)
		{
			if (Request.Files.Count > 0)
			{
				string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
				string uzanti = Path.GetExtension(Request.Files[0].FileName);


				string klasorYolu = Server.MapPath("~/Image/");

				if (!Directory.Exists(klasorYolu))
				{
					Directory.CreateDirectory(klasorYolu);
				}


				string tamDosyaYolu = Path.Combine(klasorYolu, dosyaAdi);


				Request.Files[0].SaveAs(tamDosyaYolu);


				p.PersonelGorsel = "/Image/" + dosyaAdi;
			}
			var personel = c.Personels.Find(p.PersonelID);
			personel.PersonelAd = p.PersonelAd;
			personel.PersonelSoyad = p.PersonelSoyad;
			personel.PersonelGorsel = p.PersonelGorsel;
			personel.Departmanid = p.Departmanid;
			c.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult PersonelListe()
		{
			var sorgu = c.Personels.ToList();
			return View(sorgu);
		}
	}
}