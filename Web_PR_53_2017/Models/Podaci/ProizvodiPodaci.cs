using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Web_PR_53_2017.Models.Podaci
{
    public class ProizvodiPodaci
    {
        public static List<Proizvod> proizvodi = new List<Proizvod>();
        static string putanja = HostingEnvironment.MapPath("~/App_Data/Proizvodi.txt");

        public static void UcitajProizvodeIzDatoteke()
        {
            if (File.Exists(putanja))
            {
                string linija;
                using (StreamReader sr = new StreamReader(putanja))
                {
                    while ((linija = sr.ReadLine()) != null)
                    {
                        Proizvod proizvod = PretvoriUObjekat(linija);
                        proizvodi.Add(proizvod);
                    }
                }
            }
        }

        public static Proizvod PretvoriUObjekat(string linija)
        {
            
            string[] str = linija.Split(';');
            Proizvod proizvod = new Proizvod()
            {
                Id = Int32.Parse(str[0]),
                Naziv = str[1],
                Vrsta = (VrstaMeda)Enum.Parse(typeof(VrstaMeda),str[2]),
                Proizvodjac = str[3],
                AdresaProizvodjaca = str[4],
                Boja = str[5],
                Opis = str[6],
                CenaPoTegli = Double.Parse(str[7]),
                BrojTegli = Int32.Parse(str[8])
            };
            return proizvod;
        }

        public static void SacuvajProizvodUDatoteku(Proizvod proizvod)
        {
            if (File.Exists(putanja))
            {
                using (StreamWriter sw = new StreamWriter(putanja, true))
                {
                    //foreach(Korisnik kor in korisnici)
                    //// {
                    sw.WriteLine(proizvod.ToString());
                    //}
                }
            }
        }

        public static void UpdateProizvodi()
        {
            if (File.Exists(putanja))
            {
                using (StreamWriter sw = new StreamWriter(putanja))
                {
                    foreach (Proizvod p in proizvodi)
                    {
                        sw.WriteLine(p.ToString());
                    }
                }
            }
        }
    }
}