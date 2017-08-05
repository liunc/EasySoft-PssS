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
    using EasySoft.PssS.Application.DataTransfer.Customer;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 客户仓储接口
    /// </summary>
    public interface ICustomerRepository : IBaseRepository<Customer, string>
    {
        /// <summary>
        /// 批量修改客户分组
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="oldGroupId">旧分组Id</param>
        /// <param name="newGroupId">新分组Id</param>
        void BatUpdateGroupId(DbTransaction trans, string oldGroupId, string newGroupId);

        /// <summary>
        /// 查询客户表信息，用于列表分页显示
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户数据集合</returns>
        List<Customer> Search(string name, string groupId, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 提供下单选择客户的数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <returns>返回数据</returns>
        List<CustomerForOrderDTO> GetCustomerListForOrder(string name, string groupId);
    }
}
