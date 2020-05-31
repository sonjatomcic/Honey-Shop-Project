using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_PR_53_2017.Models;
using Web_PR_53_2017.Models.Podaci;

namespace Web_PR_53_2017.Controllers
{
    public class KupovineController : Controller
    {
       

        // GET: Kupovine
        public ActionResult Index()
        {
            List<Kupovina> ku = (List<Kupovina>)HttpContext.Application["kupovine"];
            List<Kupovina> kupovine = new List<Kupovina>();

            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.KUPAC))
            {
                kupovine = ku.Where(k => k.KorisnickoImeKupca.Equals(korisnik.KorisnickoIme)).ToList<Kupovina>();
            }
            else
            {
                return RedirectToAction("Index", "Authentication");
            }

           
            return View(kupovine);

        }

        //GET: Detalji/5
        public ActionResult Detalji(int id, string forma)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.KUPAC))
            {
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
                Proizvod proizvod = pr.FirstOrDefault(p => p.Id == id);

                TempData["forma"] = forma;
                return View(proizvod);
            }
            else
            {
                return RedirectToAction("Index", "Authentication");
            }

        }

        //GET: Kupi/5&4
        public ActionResult Kupi(int id, int? kolicina, string forma)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.KUPAC))
            {
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
                Proizvod proizvod = pr.FirstOrDefault(p => p.Id == id);

                if (kolicina == null)
                {
                    TempData["greska"] = "Unesite zeljenu kolicinu";
                    TempData["forma"] = forma;
                    return View("Detalji", proizvod);
                    
                }
                else if (proizvod.BrojTegli < kolicina || kolicina<=0)
                {
                    TempData["greska"] = "Na stanju nema " + kolicina + " proizvoda";
                    TempData["forma"] = forma;
                    return View("Detalji", proizvod);
                }

                //update proizvodi i upis u kupovine
                proizvod.BrojTegli -= (int)kolicina;
                ProizvodiController.Update(proizvod, pr);
                List<Kupovina> ku = (List<Kupovina>)HttpContext.Application["kupovine"];            
                 Kupovina kupovina = new Kupovina(){Id = ku.Last().Id + 1, KorisnickoImeKupca = korisnik.KorisnickoIme, Proizvod = proizvod.Naziv, ProizvodId = proizvod.Id,
                    DatumKupovine = DateTime.Now, BrojNarucenihTegli = (int)kolicina,UkupnaCena = proizvod.CenaPoTegli * (int)kolicina};
                ku.Add(kupovina);
                KupovinePodaci.SacuvajKupovinuUDatoteku(kupovina);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Authentication");
            }

        }



    }
}