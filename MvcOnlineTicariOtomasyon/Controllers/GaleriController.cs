using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
		Context c = new Context();
		// GET: Galeri
		public ActionResult Index()
        {
            var degerler = c.Uruns.Where(x=>x.Durum==true).ToList();
            return View(degerler);
        }
    }
}