using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MiddlewareDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectSecurityPage([FromQuery]string link)
        {
            ViewBag.href = WebUtility.UrlDecode(link);
            return View();
        }
    }
}
