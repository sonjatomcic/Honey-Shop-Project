using System;
using System.Collections.Generic;
using System.Linq;
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
           // Korisnik korisnik = new Korisnik();
            //Session["korisnik"] = korisnik;
            return View();
        }

        //POST Authentication/Register
        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest
            //}

            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            foreach(var kor in korisnici)
            {
                if (kor.KorisnickoIme.Equals(korisnik.KorisnickoIme))
                {
                    ViewBag.Greska = "Korisnik sa korisnickim imenom " + korisnik.KorisnickoIme + " vec postoji!";
                    return View();
                }
            }

            korisnik.Uloga = Uloga.KUPAC;
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
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik kor = korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme && k.Lozinka == lozinka);
            if (kor != null)
            {
                Session["korisnik"] = kor;
                return RedirectToAction("Index", "Proizvodi");
            }
            else
            {
                ViewBag.Greska = "Korisnik sa korisnickim imenom " + korisnickoIme +
                    " i lozinkom " + lozinka + " ne postoji";
                return View();
            }

        }

        //GET Authentication/LogOut
        public ActionResult LogOut()
        {
            Session["korisnik"] = null;
            //Session["proizvodi"] = null;
            return RedirectToAction("Index");
        }
    }
}