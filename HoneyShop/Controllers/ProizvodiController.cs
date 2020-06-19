using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HoneyShop.Models;
using HoneyShop.Models.Podaci;

namespace HoneyShop.Controllers
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
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
                int noviId = pr.Last().Id + 1;
                ViewBag.id = noviId;
                return View();
            }
            else
            {
                //stavi ispis
                return RedirectToAction("Index", "Authentication");
            }
        }

        //POST Proizvod/Dodaj
        [HttpPost]
        public ActionResult Dodaj(Proizvod proizvod)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
                if (!ModelState.IsValid)
                {
                    Validacija(proizvod);
                    //ViewBag.Greska = "Polja nisu validna";
                    return View();
                }

                //ako u listi vec postoji taj proizvod samo uvecam kolicinu tegli, bez dodavanja
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
                for (int i = 0; i < pr.Count(); i++)
                {
                    if (pr[i].Equals(proizvod))
                    {
                        pr[i].BrojTegli += proizvod.BrojTegli;
                        ProizvodiPodaci.proizvodi = pr;
                        ProizvodiPodaci.UpdateProizvodi();
                        return RedirectToAction("Index");
                    }
                }

                pr.Add(proizvod);
                ProizvodiPodaci.SacuvajProizvodUDatoteku(proizvod);
                return RedirectToAction("Index");
            }
            else
            {
                //stavi ispis
                return RedirectToAction("Index", "Authentication");
            }


        }

        //GET: Izmeni/5
        public ActionResult Izmeni(int id)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
                Proizvod p = pr.FirstOrDefault(pro => pro.Id == id);
                return View(p);
            }
            else
            {
                //stavi ispis
                return RedirectToAction("Index", "Authentication");
            }
        }

        //POST: Izmeni
        [HttpPost]
        public ActionResult Izmeni(Proizvod proizvod)
        {
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null && korisnik.Uloga.Equals(Uloga.ADMINISTRATOR))
            {
                if (!ModelState.IsValid)
                {
                    Validacija(proizvod);
                    //ViewBag.Greska = "Polja nisu validna";
                    return View(proizvod);
                }

                //ako u listi vec postoji taj proizvod samo uvecam kolicinu tegli, bez dodavanja
                List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];

                Update(proizvod, pr);

                return RedirectToAction("Index");
            }
            else
            {
                //stavi ispis
                return RedirectToAction("Index", "Authentication");
            }

        }

        public static void Update(Proizvod proizvod, List<Proizvod> pr)
        {
            for (int i = 0; i < pr.Count(); i++)
            {
                if (pr[i].Id == proizvod.Id)
                {
                    pr[i] = proizvod;
                    ProizvodiPodaci.proizvodi = pr;
                    ProizvodiPodaci.UpdateProizvodi();
                    break;
                }
            }
        }

        //GET: Proizvodi/Sortiraj
        public ActionResult Sortiraj(string vrsta = "NAZIV", string nacin = "RASTUCE")
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
            if (naziv == "")
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
        public ActionResult PretragaPoCeni(int? minCena, int? maxCena)
        {
            List<Proizvod> pr = (List<Proizvod>)HttpContext.Application["proizvodi"];
            List<Proizvod> proizvodi = new List<Proizvod>();
            if (minCena == null || maxCena == null)
                proizvodi = pr.Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            else
                proizvodi = pr.FindAll(p => p.CenaPoTegli >= minCena && p.CenaPoTegli <= maxCena).Where(p => p.BrojTegli > 0).ToList<Proizvod>();
            ViewBag.Proizvodi = proizvodi;
            return View("Index");
        }

        public void Validacija(Proizvod proizvod)
        {
            if (proizvod.Naziv == null || proizvod.Naziv == "")
                ViewBag.naziv = "Polje 'Naziv' ne sme biti prazno!";
            else if (proizvod.Naziv.Length < 3)
                ViewBag.naziv = "Polje 'Naziv' mora imati najmanje 3 karaktera!";
            if (proizvod.Proizvodjac == null || proizvod.Proizvodjac == "")
                ViewBag.proizvodjac = "Polje 'Proizvodjac' ne sme biti prazno!";
            if (proizvod.AdresaProizvodjaca == null || proizvod.AdresaProizvodjaca == "")
                ViewBag.adresaPr = "Polje 'Adresa Proizvodjaca' ne sme biti prazno!";
            if (proizvod.Boja == null || proizvod.Boja == "")
                ViewBag.boja = "Polje 'Boja' ne sme biti prazno!";
            if (proizvod.Opis == null || proizvod.Opis == "")
                ViewBag.opis = "Polje 'Opis' ne sme biti prazno!";
            if ( proizvod.CenaPoTegli.ToString() == "")
                ViewBag.cena = "Polje 'Cena po tegli' ne sme biti prazno!";
            else
            {
                string pattern = @"^[0-9]{1,4}([.][0-9]{1,2})?$";
                if (!Regex.IsMatch(proizvod.CenaPoTegli.ToString(), pattern))
                    ViewBag.cena = "Polje 'Cena po tegli' mora da bude pozitivan broj!";
            }
            if (proizvod.BrojTegli.ToString() == "")
                ViewBag.broj = "Polje 'Broj tegli' ne sme biti prazno!";
        }

    }
}