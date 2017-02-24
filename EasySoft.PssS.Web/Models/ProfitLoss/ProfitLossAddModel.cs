// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-02-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
using System.ComponentModel.DataAnnotations;

namespace EasySoft.PssS.Web.Models.ProfitLoss
{
    /// <summary>
    /// 新增益损视图模型类
    /// </summary>
    public class ProfitLossAddModel : ProfitLossModel
    {

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置父级页面标题
        /// </summary>
        public string ParentPageTitle { get; set; }

        /// <summary>
        /// 获取或设置父级页面Url
        /// </summary>
        public string ParentPageUrl { get; set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }
    }
}