// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-03-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Web.Models.Common;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// 公共控制器类
    /// </summary>
    public class CommonController : Controller
    {
        /// <summary>
        /// 获取404错误视图
        /// </summary>
        /// <param name="aspxerrorpath">出错页面路径</param>
        /// <returns>返回视图</returns>
        public ActionResult NotFound(string aspxerrorpath)
        {
            ViewBag.ErrorMessages = WebResource.Message_PageNotFound;
            return View();
        }

        /// <summary>
        /// 获取页脚视图
        /// </summary>
        /// <returns>返回视图</returns>
        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }

        /// <summary>
        /// 获取分页组件视图
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="totalCount">查询查询记录总数</param>
        /// <param name="url">跳转Url</param>
        /// <returns>返回视图</returns>
        public PartialViewResult PageNavigation(int pageIndex, int totalCount, string url)
        {
            return PartialView("_PageNavigation", new PageNavigationModel { PageIndex = pageIndex, TotalCount = totalCount, Url = url });
        }
        
        /// <summary>
        /// 获取对话框视图
        /// </summary>
        /// <param name="id">对话框ID</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBodyId">消息体ID</param>
        /// <param name="okButton">确定按钮显示文本</param>
        /// <returns>返回视图</returns>
        public PartialViewResult Dialog(string id, string title, string messageBodyId, string okButton)
        {
            List<string> errorMessages = new List<string>();
            ValidateHelper.CheckInputString(WebResource.Dialog_Id, id, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_Title, title, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_MessageBodyId, messageBodyId, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_OkButton, okButton, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);

            if (errorMessages.Count > 0)
            {
                throw new EasySoftException(string.Join("<br />", errorMessages));
            }

            return PartialView("_Dialog", new DialogModel { Id = id, Title = title, MessageBodyId = messageBodyId, OkButton = okButton });
        }

        /// <summary>
        /// 获取确认对话框视图
        /// </summary>
        /// <param name="id">对话框ID</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBodyId">消息体ID</param>
        /// <param name="confirmString">确认字符串</param>
        /// <param name="okButton">确定按钮显示文本</param>
        /// <param name="cancelButton">取消按钮显示文本</param>
        /// <param name="okButtonClick">确定按钮调用的JS函数</param>
        /// <returns>返回视图</returns>
        public PartialViewResult Confirm(string id, string title, string confirmString, string okButton, string cancelButton, string okButtonClick)
        {
            List<string> errorMessages = new List<string>();
            ValidateHelper.CheckInputString(WebResource.Dialog_Id, id, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_Title, title, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_ConfirmString, confirmString, true, ValidateHelper.STRING_LENGTH_120, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_OkButton, okButton, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_CancelButton, cancelButton, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Dialog_OkButton, okButtonClick, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);

            if (errorMessages.Count > 0)
            {
                throw new EasySoftException(string.Join("<br />", errorMessages));
            }

            return PartialView("_Confirm", new DialogModel { Id = id, Title = title, OkButton = okButton, CancelButton = cancelButton, ConfirmString = confirmString, OkButtonClick = okButtonClick });
        }
    }
}