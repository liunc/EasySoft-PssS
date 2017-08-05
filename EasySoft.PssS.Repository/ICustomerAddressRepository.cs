// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：仓储接口库
// 创 建 人：刘年超
// 创建时间：2017-05-13
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Repository
{
    using EasySoft.Core.Persistence.Repository;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 客户地址仓储接口
    /// </summary>
    public interface ICustomerAddressRepository : IBaseRepository<CustomerAddress, string>
    {
        /// <summary>
        /// 获取默认地址对象
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回默认地址对象</returns>
        CustomerAddress GetDefaultAddres(DbTransaction trans, string customerId);

        /// <summary>
        /// 根据客户ID删除数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="customerId">客户Id</param>
        void DeleteByCustomerId(DbTransaction trans, string customerId);

        /// <summary>
        /// 查询客户地址信息
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回客户地址数据集合</returns>
        List<CustomerAddress> SearchByCustomerId(string customerId);

        /// <summary>
        /// 查询客户地址信息
        /// </summary>
        /// <param name="linkMan">联系人</param>
        /// <returns>返回客户地址数据集合</returns>
        List<CustomerAddress> SearchByLinkMan(string linkMan);
    }
}
