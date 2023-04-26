using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaAPI.Areas.AdminPage.Controllers
{
    public class HomeController : Controller
    {
        // GET: AdminPage/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}