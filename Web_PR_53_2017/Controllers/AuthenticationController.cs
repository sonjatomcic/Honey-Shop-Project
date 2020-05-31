using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Web_PR_53_2017.Models;
using Web_PR_53_2017.Models.Podaci;


namespace Web_PR_53_2017.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View("Login");
        }

        //GET Authentication/Register
        public ActionResult Registracija()
        {

            //napravi proveru sta ako smo vec ulogovani
           // Korisnik korisnik = new Korisnik();
            //Session["korisnik"] = korisnik;
            return View();
        }

        //POST Authentication/Registracija
        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag.Greska = "Polja nisu validna";
                Validacija(korisnik);
                return View();
            }

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            foreach(var kor in korisnici)
            {
                if (kor.KorisnickoIme.Equals(korisnik.KorisnickoIme) && kor.Aktivan==true)
                {
                    ViewBag.Greska = "Korisnik sa korisnickim imenom " + korisnik.KorisnickoIme + " vec postoji!";
                    return View();
                }
            }

            korisnik.Uloga = Uloga.KUPAC;
            korisnik.Aktivan = true;
            korisnici.Add(korisnik);
            KorisniciPodaci.SacuvajKorisnikaUDatoteku(korisnik);
            //Session["korisnik"] = korisnik; msm da ovo ne treba jer korisik treba da se tek uloguje
            return RedirectToAction("Index");
        }


        //GET Authentication/Login
        public ActionResult Login()
        {
            //ako korisnik nije ulogovan prikazuje se forma
            //ako je korisnik ulogovan salji gresku 
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null)
            {
                return RedirectToAction("Index", "Authentication");
            }

            ViewBag.Greska = "Vec ste ulogovani kao " + korisnik.KorisnickoIme;
            return View();
        }

        //POST Authentication/Login
        [HttpPost]
        public ActionResult Login(string korisnickoIme, string lozinka)
        {
            //ako je vec ulogovan ne moze se logovati 
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik != null)
            {
                ViewBag.Greska = "Vec ste ulogovani kao " + korisnik.KorisnickoIme;
                return View();
            }

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> korisnici2 = korisnici.FindAll(k => k.KorisnickoIme == korisnickoIme && k.Lozinka == lozinka);
            foreach(Korisnik kor in korisnici2)
            {
                if (kor != null && kor.Aktivan == true)
                {
                    Session["korisnik"] = kor;
                    return RedirectToAction("Index", "Proizvodi");
                }
                else if(kor!=null && kor.Aktivan==false)
                {
                    continue;
                    //ViewBag.Greska = "Korisnik sa korisnickim imenom " + korisnickoIme +
                    //    " i lozinkom " + lozinka + " ne postoji";
                    //return View();
                }
            }
            ViewBag.Greska = "Korisnik sa korisnickim imenom " + korisnickoIme +
                       " i lozinkom " + lozinka + " ne postoji";
            return View();
        }

        //GET Authentication/LogOut
        public ActionResult LogOut()
        {
            Session["korisnik"] = null;
            //Session["proizvodi"] = null;
            return RedirectToAction("Index");
        }

        public void Validacija(Korisnik korisnik)
        {
            if (korisnik.KorisnickoIme == null || korisnik.KorisnickoIme == "")
                ViewBag.korIme = "Polje 'Korisnicko Ime' ne sme biti prazno!";
            else if (korisnik.KorisnickoIme.Length < 3)
                ViewBag.korIme = "Polje 'Korisnicko Ime' mora imati najmanje 3 karaktera!";
            if (korisnik.Lozinka == null || korisnik.Lozinka == "")
                ViewBag.lozinka = "Polje 'Lozinka' ne sme biti prazno!";
            else if (korisnik.Lozinka.Length < 8)
                ViewBag.lozinka = "Polje 'Lozinka' mora imati najmanje 8 karaktera!";
            else
            {
                string pattern = @"^[a-zA-Z0-9]{8,}$";
                if (!Regex.IsMatch(korisnik.Lozinka, pattern))
                    ViewBag.lozinka = "Polje 'Lozinka' moze da sadrzi samo slova i brojeve!";
            }
            if (korisnik.Ime == null || korisnik.Ime == "")
                ViewBag.ime = "Polje 'Ime' ne sme biti prazno!";
            if (korisnik.Prezime == null || korisnik.Prezime == "")
                ViewBag.prezime = "Polje 'Prezime' ne sme biti prazno!";
            if (korisnik.Email == null || korisnik.Email == "")
                ViewBag.email = "Polje 'Email' ne sme biti prazno!";
            if (korisnik.DatumRodjenja.ToString("dd/mm/yyyy").Equals("01/00/0001"))
                ViewBag.datum = "Polje 'Datum' ne sme biti prazno!";
            
        }
    }
}