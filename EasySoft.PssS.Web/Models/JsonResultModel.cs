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
namespace EasySoft.PssS.Web.Models
{
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
        /// 构造函数
        /// </summary>
        public JsonResultModel()
        {
            this.Result = false;
        }
    }
}