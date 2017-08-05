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
    using Core.Persistence.Repository;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 采购项仓储接口
    /// </summary>
    public interface IPurchaseRepository : IBaseRepository<Purchase, string>
    {
        /// <summary>
        /// 查询采购表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="item">产品项</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回采购数据集合</returns>
        List<Purchase> Search(string category, string item, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 查询可交付的采购数据
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <returns>返回采购数据集合</returns>
        List<Purchase> GetDeliverable(string category);

    }
}
