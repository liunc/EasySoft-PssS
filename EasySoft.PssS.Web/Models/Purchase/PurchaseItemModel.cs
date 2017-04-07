// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Purchase
{
    /// <summary>
    /// 采购项视图模型类
    /// </summary>
    public class PurchaseItemModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置入库单位
        /// </summary>
        public string Unit { get; set; }

        #endregion
    }
}