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
            List<Proizvod> proizvodi = (List<Proizvod>)HttpContext.Application["proizvodi"];
            return View(proizvodi);
        }
    }
}