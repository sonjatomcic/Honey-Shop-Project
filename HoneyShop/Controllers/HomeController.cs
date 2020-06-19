using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoneyShop.Models;

namespace HoneyShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*
            Korisnik korisnik = (Korisnik)Session["korisnik"];
            if (korisnik == null)
            {
                return RedirectToAction("Index", "Authentication");
            }*/

            //dodaj sesiju za shopping cart ako ima

            //List<Proizvod> proizvodi = (List<Proizvod>)HttpContext.Application["proizvodi"];
            return RedirectToAction("Index","Proizvodi");
        }
        
    }
}