// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-13
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Web.Models;
    using EasySoft.PssS.Web.Models.User;
    using EasySoft.PssS.Web.Resources;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// 用户控制器类
    /// </summary>
    public class UserController : Controller
    {
        private UserService userService = null;

        public UserController()
        {
            this.userService = new UserService();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            JsonResultModel result = new JsonResultModel();
            User user = this.userService.Login(model.Moblie, model.Password);
            if (user == null)
            {
                result.Message = WebResource.UserLogin_LoginError;
            }
            else
            {
                this.Session["Moblie"] = model.Moblie;
                this.Session["Role"] = user.Role;
                this.Session["Name"] = user.Name;
                FormsAuthentication.SetAuthCookie(model.Moblie, false);
                result.Result = true;
                result.Data = string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/": model.ReturnUrl;
            }
            return Json(result);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Clear();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}