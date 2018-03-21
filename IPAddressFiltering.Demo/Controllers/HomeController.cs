using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPAddressFiltering;
using IPAddressFiltering.Configuration;

namespace IPAddressFiltering.Demo.Controllers
{
   
    public class HomeController : Controller
    {

        [HttpGet]
        [IPAddressFilterAttribute("ipFIlter")]
        public ActionResult Index()
        {
            return View();
        }
    }
}