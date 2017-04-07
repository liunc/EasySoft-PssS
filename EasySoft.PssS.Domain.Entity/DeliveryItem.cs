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
    /// 交付项领域实体类
    /// </summary>
    public class DeliveryItem
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
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置是否需要包装
        /// </summary>
        public bool NeedPack { get; set; }

        /// <summary>
        /// 获取或设置包装前后单位换算的比例
        /// </summary>
        public string PackRate { get; set; }

        #endregion
    }
}
