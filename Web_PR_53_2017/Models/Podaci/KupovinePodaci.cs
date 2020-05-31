using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Web_PR_53_2017.Models.Podaci
{
    public class KupovinePodaci
    {
        public static List<Kupovina> kupovine = new List<Kupovina>();
        static string putanja = HostingEnvironment.MapPath("~/App_Data/Kupovine.txt");
        

        public static void UcitajKupovineIzDatoteke()
        {
            if (File.Exists(putanja))
            {
                string linija;
                using (StreamReader sr = new StreamReader(putanja))
                {
                    while ((linija = sr.ReadLine()) != null)
                    {
                        Kupovina kupovina = PretvoriUObjekat(linija);
                        kupovine.Add(kupovina);
                    }
                }
            }
        }

        public static Kupovina PretvoriUObjekat(string linija)
        {
            
            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
            string[] str = linija.Split(';');
         
            Kupovina kup = new Kupovina()
            {
                Id=Int32.Parse(str[0]), KorisnickoImeKupca=str[1], Proizvod=str[2], ProizvodId=Int32.Parse(str[3]),
                DatumKupovine=DateTime.Parse(str[4],MyCultureInfo), BrojNarucenihTegli=Int32.Parse(str[5]),
                UkupnaCena=Double.Parse(str[6])
            };
            return kup;

        }

        public static void SacuvajKupovinuUDatoteku(Kupovina kup)
        {
            if (File.Exists(putanja))
            {
                using (StreamWriter sw = new StreamWriter(putanja, true))
                {
                    //foreach(Korisnik kor in korisnici)
                    //// {
                    sw.WriteLine(kup.ToString());
                    //}
                }
            }
        }

        public static void UpdateKupovina()
        {
            if (File.Exists(putanja))
            {
                using (StreamWriter sw = new StreamWriter(putanja))
                {
                    foreach (Kupovina k in kupovine)
                    {
                        sw.WriteLine(k.ToString());
                    }
                }
            }
        }
    }
    
}