using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Web_PR_53_2017.Models.Podaci
{
    public class KorisniciPodaci
    {
        public static List<Korisnik> korisnici = new List<Korisnik>();
        static string putanja = HostingEnvironment.MapPath("~/App_Data/Korisnici.txt");

        public static void UcitajKorisnikeIzDatoteke()
        {
            if (File.Exists(putanja))
            {           
                string linija;
                using (StreamReader sr = new StreamReader(putanja))
                {
                    while ((linija = sr.ReadLine()) != null)
                    {
                        Korisnik korisnik = PretvoriUObjekat(linija);
                        korisnici.Add(korisnik);
                    }
                }
            }          
        }

        public static Korisnik PretvoriUObjekat(string linija)
        {
            CultureInfo MyCultureInfo = new CultureInfo("de-DE");
            string[] str = linija.Split(';');
            Korisnik kor = new Korisnik() { KorisnickoIme=str[0], Lozinka=str[1], Ime=str[2], Prezime=str[3],
                Pol=(Pol)Enum.Parse(typeof(Pol), str[4]), Email=str[5], DatumRodjenja=DateTime.Parse(str[6],MyCultureInfo),
                Uloga=(Uloga)Enum.Parse(typeof(Uloga), str[7]), Aktivan=Boolean.Parse(str[8])};
            return kor;
        }

        public static void SacuvajKorisnikaUDatoteku(Korisnik kor)
        {
            if (File.Exists(putanja))
            {
                using(StreamWriter sw = new StreamWriter(putanja,true))
                {
                    //foreach(Korisnik kor in korisnici)
                    //// {
                    sw.WriteLine(kor.ToString());
                    //}
                }
            }
        }

        public static void UpdateKorisnici()
        {
            if (File.Exists(putanja))
            {
                using (StreamWriter sw = new StreamWriter(putanja))
                {
                    foreach(Korisnik kor in korisnici)
                    {
                        sw.WriteLine(kor.ToString());
                    }
                }
            }
        }
    }
}