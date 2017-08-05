// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据传输对象类库
// 创 建 人：刘年超
// 创建时间：2017-07-18
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Application.DataTransfer.Delivery
{
    using EasySoft.Core.Util;

    /// <summary>
    /// 新增交付明细数据传输对象类
    /// </summary>
    public class DeliveryDetailAddDTO
    {
        #region 属性

        /// <summary>
        /// 获取或设置采购Id
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// 获取或设置采购项分类
        /// </summary>
        public string ItemCategory { get; set; }

        /// <summary>
        /// 获取或设置采购项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置采购项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 获取或设置交付数量
        /// </summary>
        public decimal DeliveryQuantity { get; set; }

        /// <summary>
        /// 获取或设置包装数量
        /// </summary>
        public decimal PackQuantity { get; set; }

        /// <summary>
        /// 获取或设置交付单位
        /// </summary>
        public string PackUnit { get; set; }

        #endregion
    }
}
