using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_PR_53_2017.Models;

namespace Web_PR_53_2017.Controllers
{
    public class ProizvodiController : Controller
    {
        // GET: Proizvodi
        public ActionResult Index()
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
  
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if ((korisnik == null) || (korisnik != null && korisnik.Uloga.Equals(Uloga.KUPAC)))
            {
                proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            }
            else
            {
               proizvodi = pr;
             }

            ViewBag.Proizvodi = proizvodi;
            return View();
        }

        //GET: Proizvodi/Dodaj
        public ActionResult Dodaj()
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if(korisnik!=null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {

                return View();
            }
            else
            {
                //stavi ispis
                return RedirectToAction("Index", "Authentication");
            }
        }

        //POST Proizvod/Dodaj
        public ActionResult Dodaj(Proizvod prozivod)
        {
            return View();
        }



        //GET: Proizvodi/Sortiraj
        public ActionResult Sortiraj(string vrsta="NAZIV", string nacin="RASTUCE")
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
            
            
                if (vrsta == "NAZIV" && nacin == "RASTUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderBy(p => p.Naziv).ToList<Proizvod>();
                else if (vrsta == "NAZIV" && nacin == "OPADAJUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderByDescending(p => p.Naziv).ToList<Proizvod>();
                else if (vrsta == "VRSTA MEDA" && nacin == "RASTUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderBy(p => p.Vrsta.ToString()).ToList<Proizvod>();
                else if (vrsta == "VRSTA MEDA" && nacin == "OPADAJUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderByDescending(p => p.Vrsta.ToString()).ToList<Proizvod>();
                else if (vrsta == "CENA" && nacin == "RASTUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderBy(p => p.CenaPoTegli).ToList<Proizvod>();
                else if (vrsta == "CENA" && nacin == "OPADAJUCE")
                    proizvodi = pr.Where(p => p.BrojTegli > 0).OrderByDescending(p => p.CenaPoTegli).ToList<Proizvod>();
                else
                    proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();  //ako ne unese nista ili unese sam nesto
            

            ViewBag.Proizvodi = proizvodi;
            return View("Index"); 

        }

        //GET Proizvodi/PretragaPoNazivu
        public ActionResult PretragaPoNazivu(string naziv)
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
            if(naziv=="")
                proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            else
                proizvodi = pr.FindAll(p => p.Naziv.ToLower().Equals(naziv.ToLower())).Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            ViewBag.Proizvodi = proizvodi;
            return View("Index");
        }

        //GET Proizvodi/PretragaPoVrsti
        public ActionResult PretragaPoVrsti(string vrsta)
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
            if (vrsta == "")
                proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            else
                proizvodi = pr.FindAll(p => p.Vrsta.ToString().Equals(vrsta)).Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            ViewBag.Proizvodi = proizvodi;
            return View("Index");
        }

        //GET Proizvodi/PretragaPoCeni
        public ActionResult PretragaPoCeni(int? minCena,int? maxCena)
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
            if (minCena==null || maxCena==null)
                proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            else            
                proizvodi = pr.FindAll(p => p.CenaPoTegli >= minCena && p.CenaPoTegli<=maxCena).Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            ViewBag.Proizvodi = proizvodi;
            return View("Index");
        }



    }
}