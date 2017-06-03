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
namespace EasySoft.PssS.Web.Models.CustomerAddress
{
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 客户地址视图模型类
    /// </summary>
    public class CustomerAddressIndexModel 
    {
        #region 属性

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public string QueryGroupId { get; set; }

        /// <summary>
        /// 获取或设置当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public List<CustomerAddressPageModel> Addresses { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressIndexModel()
        {
            this.Addresses = new List<CustomerAddressPageModel>();
        }

        #endregion

    }
}