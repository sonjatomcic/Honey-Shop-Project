using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_PR_53_2017.Models
{
    public class Kupovina
    {
        public int Id { get; set; }
        public String KorisnickoImeKupca { get; set; }
        public Proizvod Proizvod{ get; set; }
        public int ProizvodId { get; set; }
        public DateTime DatumKupovine { get; set; }
        public int BrojNarucenihTegli { get; set; }
        public double UkupnaCena { get; set; }
    }
}