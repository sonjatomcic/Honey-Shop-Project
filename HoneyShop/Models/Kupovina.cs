using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoneyShop.Models
{
    public class Kupovina
    {
        public int Id { get; set; }
        public String KorisnickoImeKupca { get; set; }
        public string Proizvod{ get; set; }
        public int ProizvodId { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int BrojNarucenihTegli { get; set; }
        public double UkupnaCena { get; set; }

        public override string ToString()
        {
            return this.Id.ToString() + ";" + this.KorisnickoImeKupca + ";" + this.Proizvod + ";" + this.ProizvodId.ToString() +
                ";" + this.DatumKupovine.Date.ToString("dd/MM/yyyy") + ";" + this.BrojNarucenihTegli.ToString() + ";" + this.UkupnaCena.ToString();
        }
    }
}