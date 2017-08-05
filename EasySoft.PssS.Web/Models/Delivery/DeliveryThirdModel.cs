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
namespace EasySoft.PssS.Web.Models.Delivery
{
    using EasySoft.PssS.Web.Models.Purchase;
    using SaleOrder;
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// 出库第三步视图模型类
    /// </summary>
    public class DeliveryThirdModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public DeliveryAddModel AddModel { get; set; }

        /// <summary>
        /// 获取或设置应用
        /// </summary>
        public List<SaleOrderNeedExpressModel> SaleOrders { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryThirdModel()
        {
            this.SaleOrders = new List<SaleOrderNeedExpressModel>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        public DeliveryThirdModel(string id) : this()
        {
            this.Id = id;
        }

        #endregion
    }
}