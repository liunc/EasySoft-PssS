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
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models.Home;
    using System.Web.Mvc;

    /// <summary>
    /// 主页控制器类
    /// </summary>
    public class HomeController : Controller
    {
        #region 方法

        /// <summary>
        /// 获取Index视图
        /// </summary>
        /// <returns></returns>
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.Name = this.Session["Name"].ToString();
            model.Moblie = this.Session["Mobile"].ToString();
            return View(model);
        }

        #endregion


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