using EasySoft.PssS.Web.Models;
using EasySoft.PssS.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasySoft.PssS.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            JsonResultModel result = new JsonResultModel();
            return Json(result);
        }
    }
}