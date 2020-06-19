using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HoneyShop.Models;
using HoneyShop.Models.Podaci;

namespace HoneyShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            KorisniciPodaci.UcitajKorisnikeIzDatoteke();
            List<Korisnik> korisnici = KorisniciPodaci.korisnici;
            HttpContext.Current.Application["korisnici"] = korisnici;

            ProizvodiPodaci.UcitajProizvodeIzDatoteke();
            List<Proizvod> proizvodi = ProizvodiPodaci.proizvodi;
            HttpContext.Current.Application["proizvodi"] = proizvodi;

            KupovinePodaci.UcitajKupovineIzDatoteke();
            List<Kupovina> kupovine = KupovinePodaci.kupovine;
            HttpContext.Current.Application["kupovine"] = kupovine;

        }
    }
}
