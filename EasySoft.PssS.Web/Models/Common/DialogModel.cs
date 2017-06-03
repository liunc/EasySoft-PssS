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
namespace EasySoft.PssS.Web.Models.Common
{
    /// <summary>
    /// 对话框视图模型类
    /// </summary>
    public class DialogModel
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置消息Id
        /// </summary>
        public string MessageBodyId { get; set; }

        /// <summary>
        /// 获取或设置确认字符串
        /// </summary>
        public string ConfirmString { get; set; }

        /// <summary>
        /// 获取或设置第一个按钮显示文本
        /// </summary>
        public string OkButton { get; set; }

        /// <summary>
        /// 获取或设置第二个按钮显示文本
        /// </summary>
        public string CancelButton { get; set; }

        /// <summary>
        /// 获取或设置第一个按钮显示文本
        /// </summary>
        public string OkButtonClick { get; set; }

        /// <summary>
        /// 获取标题Id
        /// </summary>
        public string GetTitleId()
        {
            return string.Format("modalLabel{0}", this.Id);
        }
    }
}