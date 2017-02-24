// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-02-24
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// 错误控制器类
    /// </summary>
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(List<string> errorMessages)
        {
            ViewBag.ErrorMessages = errorMessages;
            return View();
        }
    }
}