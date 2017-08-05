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
    using Core.Util;
    using Core.ViewModel;
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
            Validate validate = new Validate();
            validate.CheckObjectArgument<LoginModel>("model", model);
            if (validate.IsFailed)
            {
                result.BuilderErrorMessage(validate.ErrorMessages);
                return Json(result);
            }
            model.PostValidate(ref validate);
            if (validate.IsFailed)
            {
                result.BuilderErrorMessage(validate.ErrorMessages);
                return Json(result);
            }

            User user = this.userService.Login(model.Mobile.Trim(), model.Password.Trim());
            if (user == null)
            {
                result.Message = WebResource.Message_LoginError;
            }
            else
            {
                this.Session["Mobile"] = model.Mobile;
                this.Session["Roles"] = user.Roles;
                this.Session["Name"] = user.Name;
                FormsAuthentication.SetAuthCookie(model.Mobile, false);

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