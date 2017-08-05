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
    /// 客户分组仓储接口
    /// </summary>
    public interface ICustomerGroupRepository : IBaseRepository<CustomerGroup, string>
    {
        /// <summary>
        /// 判断是否存在名称相同的分组
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id，新增时为空字符串</param>
        /// <param name="name">分组名称</param>
        /// <returns>返回布尔值</returns>
         bool HasSameName(DbTransaction trans, string id, string name);

        /// <summary>
        /// 获取默认分组Id
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <returns>返回默认分组Id</returns>
        string GetDefaultGroupId(DbTransaction trans);

        /// <summary>
        /// 查询客户分组表信息，用于列表分页显示
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户分组数据集合</returns>
        List<CustomerGroup> Search(int pageIndex, int pageSize, ref int totalCount);
        
    }
}
