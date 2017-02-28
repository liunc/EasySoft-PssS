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
    using System.Collections.Generic;
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
        /// <returns>返回视图对象</returns>
        public ActionResult Login(string returnUrl)
        {
            LoginModel model = new LoginModel { ReturnUrl = returnUrl };
            return View(model);
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
            List<string> errorMessages = new List<string>();
            if (!ValidateHelper.CheckObjectArgument<LoginModel>("model", model, ref errorMessages))
            {
                result.BuilderErrorMessage(errorMessages);
                return Json(result);
            }
            ValidateHelper.CheckInputString(WebResource.Field_Moblie, model.Moblie, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Password, model.Password, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                result.BuilderErrorMessage(errorMessages);
                return Json(result);
            }

            User user = this.userService.Login(model.Moblie.Trim(), model.Password.Trim());
            if (user == null)
            {
                result.Message = WebResource.Message_LoginError;
            }
            else
            {
                this.Session["Moblie"] = model.Moblie;
                this.Session["Roles"] = user.Roles;
                this.Session["Name"] = user.Name;
                FormsAuthentication.SetAuthCookie(model.Moblie, false);

                result.Result = true;
                result.Data = string.IsNullOrWhiteSpace(model.ReturnUrl) ? "/" : model.ReturnUrl;
            }
            return Json(result);
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <returns>用户退出</returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            this.Session.Clear();
            return Redirect(Request.UrlReferrer.ToString());
        }

        #endregion
    }
}