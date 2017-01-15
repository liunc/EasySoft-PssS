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
        #region 变量

        private UserService userService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserController()
        {
            this.userService = new UserService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取Login视图
        /// </summary>
        /// <param name="returnUrl">跳转页面Url</param>
        /// <returns></returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 提交登录
        /// </summary>
        /// <param name="model">登录数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            JsonResultModel result = new JsonResultModel();
            if(model == null)
            {
                result.Message = WebResource.ArgumentNull;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Moblie))
                {
                    result.Message = WebResource.UserLogin_MoblieTip;
                }
                else if (string.IsNullOrWhiteSpace(model.Password))
                {
                    result.Message = WebResource.UserLogin_PasswordTip;
                }
            }
            if (!string.IsNullOrWhiteSpace(result.Message))
            {
                return Json(result);
            }

            User user = this.userService.Login(model.Moblie.Trim(), model.Password.Trim());
            if (user == null)
            {
                result.Message = WebResource.UserLogin_LoginError;
            }
            else
            {
                this.Session["Moblie"] = model.Moblie;
                this.Session["Roles"] = user.Roles;
                this.Session["Name"] = user.Name;
                FormsAuthentication.SetAuthCookie(model.Moblie, false);

                result.Result = true;
                result.Data = string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/": model.ReturnUrl;
            }
            return Json(result);
        }

        /// <summary>
        /// 用户退回
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Clear();
            return Redirect(Request.UrlReferrer.ToString());
        }

        #endregion
    }
}