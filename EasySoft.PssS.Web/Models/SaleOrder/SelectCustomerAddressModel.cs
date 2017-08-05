// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-08-03
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using EasySoft.PssS.Application.DataTransfer.Customer;
    using System.Collections.Generic;

    /// <summary>
    /// 选择客户地址视图模型类
    /// </summary>
    public class SelectCustomerAddressModel
    {
        /// <summary>
        /// 获取或设置查询条件客户分组Id
        /// </summary>
        public string QueryGroupId { get; set; }

        /// <summary>
        /// 获取或设置查询条件客户名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置客户及地址信息
        /// </summary>
        public List<CustomerForOrderDTO> Customers { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SelectCustomerAddressModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupId">查询条件客户分组Id</param>
        /// <param name="name">查询条件客户名称</param>
        public SelectCustomerAddressModel(string groupId, string name)
        {
            this.QueryGroupId = groupId;
            this.QueryName = name;
        }
    }
}