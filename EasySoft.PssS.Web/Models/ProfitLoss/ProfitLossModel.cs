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
namespace EasySoft.PssS.Web.Models.ProfitLoss
{
    /// <summary>
    /// 益损视图模型类
    /// </summary>
    public class ProfitLossModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置关联Id
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置目标类型
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        #endregion
    }
}