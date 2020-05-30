using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_PR_53_2017.Models;
using Web_PR_53_2017.Models.Podaci;

namespace Web_PR_53_2017.Controllers
{
    public class KorisniciController : Controller
    {
        // GET: Korisnici
        public ActionResult Index()
        {
            List<Korisnik> kor = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> korisnici = new List<Korisnik>();

            //ako je admin moze da vidi ostale korisnike
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if(korisnik!=null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
               
                 korisnici=kor.Where(k => k.Uloga.Equals(Uloga.KUPAC) && k.Aktivan==true).ToList<Korisnik>();
                return View(korisnici);
                //ViewBag.Korisnici = korisnici;
            }
            else
            {
                ViewBag.Greska = "Samo admin moze da vidi korisnike!";
                return View();
            }           
        }

        //GET: Korisnici/Obrisi/korisnickoIme
        public ActionResult Obrisi(string korisnickoIme)
        {
            List<Korisnik> kor = (List<Korisnik>)HttpContext.Application["korisnici"];
            
            //admin moze da brise korisnike
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
                Korisnik k = kor.FirstOrDefault(kk => kk.KorisnickoIme.Equals(korisnickoIme));
                k.Aktivan = false;
                for(int i = 0; i < kor.Count; i++)
                {
                    if (kor[i].Uloga.Equals(Uloga.KUPAC) && kor[i].KorisnickoIme.Equals(korisnickoIme))
                    {
                        kor[i] = k;
                        break;
                    }
                }
                KorisniciPodaci.korisnici = kor;
                KorisniciPodaci.UpdateKorisnici();
                return RedirectToAction("Index");
                
            }
            else
            {
                return RedirectToAction("Index","Authentication");
            }
            
        }
        

    }
}