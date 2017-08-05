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


namespace EasySoft.Core.ViewModel
{
    using EasySoft.Core.Util.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// JsonResult视图模型类
    /// </summary>
    public class JsonResultModel
    {
        /// <summary>
        /// 获取或设置结果
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 获取或设置消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 获取或设置返回的数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public JsonResultModel()
        {
            this.Result = false;
        }

        /// <summary>
        /// 生成错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        public void BuilderErrorMessage(string errorMessage)
        {
            this.Message = string.Format("{0}<br />{1}", UtilResource.Message_SubmitFailed, errorMessage);
        }

        /// <summary>
        /// 生成错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息集合</param>
        public void BuilderErrorMessage(List<string> errorMessages)
        {
            this.Message = string.Format("{0}<br />{1}", UtilResource.Message_SubmitFailed, string.Join("<br />", errorMessages));
        }
    }
}