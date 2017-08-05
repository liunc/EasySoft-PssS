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
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using Core.Util;
    using CustomerAddress;
    using EasySoft.Core.ViewModel;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 销售订单首页视图模型类
    /// </summary>
    public class SaleOrderSecondModel : SaleOrderAddModel
    {
        public List<CustomerAddressSelectModel> CustomerAddress { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderSecondModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        public SaleOrderSecondModel(string id, string linkMan) : base(id, linkMan)
        {
            this.CustomerAddress = new List<CustomerAddressSelectModel>();
        }

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