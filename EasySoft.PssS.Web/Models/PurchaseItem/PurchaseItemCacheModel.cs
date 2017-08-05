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
namespace EasySoft.PssS.Web.Models.PurchaseItem
{
    using EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 采购项视图模型类
    /// </summary>
    public class PurchaseItemCacheModel
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
        public string InUnit { get; set; }

        /// <summary>
        /// 获取或设置出库单位
        /// </summary>
        public string OutUnit { get; private set; }

        /// <summary>
        /// 获取或设置入库出库单位换算比例
        /// </summary>
        public decimal InOutRate { get; private set; }

        /// <summary>
        /// 获取或设置销售单价
        /// </summary>
        public decimal Price { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseItemCacheModel()
        {
           
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">采购项领域实体对象</param>
        public PurchaseItemCacheModel(PurchaseItem entity)
        {
            this.Code = entity.Code;
            this.Name = entity.Name;
            this.InUnit = entity.InUnit;
            this.OutUnit = entity.OutUnit;
            this.InOutRate = entity.InOutRate;
            this.Price = entity.Price;
        }

        #endregion
    }
}