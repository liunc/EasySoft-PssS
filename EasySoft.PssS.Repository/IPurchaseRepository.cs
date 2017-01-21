// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：仓储接口库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Repository
{
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 采购项仓储接口
    /// </summary>
    public interface IPurchaseRepository
    {
        /// <summary>
        /// 获取采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        void Insert(DbTransaction trans, Purchase entity);

        /// <summary>
        /// 根据Id获取一条采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回采购实体对象</returns>
        Purchase Select(DbTransaction trans, string id);

        /// <summary>
        /// 查询采购表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        List<Purchase> Search(string category, int pageIndex, int pageSize, ref int totalCount);


    }
}
