// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------

namespace EasySoft.PssS.Domain.Entity
{
    using EasySoft.PssS.Domain.ValueObject;

    /// <summary>
    /// 采购项领域实体类
    /// </summary>
    public class PurchaseItem: EntityBase
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
        /// 获取或设置有效标识
        /// </summary>
        public string Valid { get; set; }

        /// <summary>
        /// 获取或设置入库单位
        /// </summary>
        public string InputUnit { get; set; }

        /// <summary>
        /// 获取或设置出库单位
        /// </summary>
        public string OutputUnit { get; set; }

        /// <summary>
        /// 获取或设置入库单位
        /// </summary>
        public string InOutRate { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public PurchaseCategory Category { get; set; }

        #endregion
    }
}
