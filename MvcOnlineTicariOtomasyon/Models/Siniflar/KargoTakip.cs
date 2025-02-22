using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
	public class KargoTakip
	{
        [Key]
        public int KargoTakipid { get; set; }
        [Column(TypeName ="Varchar")]
		[StringLength(10)]
		public string Takipkodu { get; set; }
		[Column(TypeName = "Varchar")]
		[StringLength(100)]
		public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}