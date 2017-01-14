using EasySoft.PssS.Web.Filters;
using EasySoft.PssS.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySoft.PssS.Web.Controllers
{
    public class HomeController : Controller
    {
        [MyAuthorize(Roles ="Admin")]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.Name = this.Session["Name"].ToString();
            model.Moblie = this.Session["Moblie"].ToString();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}