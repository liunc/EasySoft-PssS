// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Outbound
{
    using EasySoft.PssS.Web.Models.Purchase;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 出库第一步视图模型类
    /// </summary>
    public class OutboundFirstModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id        
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置产品
        /// </summary>
        public List<PurchasePageModel> Products { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public OutboundFirstModel()
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.Products = new List<PurchasePageModel>();
        }
        
        #endregion
    }
}