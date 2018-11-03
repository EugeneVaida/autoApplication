using autoApp.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace autoApp.Controllers
{
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult Index()
        {
            using (var db = new CarContext())
            {

            }
            return View();
        }


    }
}