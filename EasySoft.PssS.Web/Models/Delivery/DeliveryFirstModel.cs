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
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// 出库第一步视图模型类
    /// </summary>
    public class DeliveryFirstModel : DeliveryAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置产品
        /// </summary>
        public List<PurchaseDeliveryModel> Products { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryFirstModel()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        public DeliveryFirstModel(string id) : base(id)
        {
            this.Products = new List<PurchaseDeliveryModel>();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 生成新Id
        /// </summary>
        public void NewId()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }
        #endregion
    }
}