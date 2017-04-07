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
    using System.Collections.Generic;

    /// <summary>
    /// 采购配置领域实体类
    /// </summary>
    public class PurchaseConfig
    {
        #region 属性

        /// <summary>
        /// 获取或设置采购项
        /// </summary>

        public List<PurchaseItem> PurchaseItems { get; set; }

        /// <summary>
        /// 获取或设置成本项
        /// </summary>
        public List<CostItem> CostItems { get; set; }

        #endregion
    }
}
