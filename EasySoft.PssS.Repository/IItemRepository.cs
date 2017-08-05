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
    using EasySoft.Core.Persistence.Repository;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 成本项仓储接口
    /// </summary>
    public interface ICostItemRepository: IBaseRepository<CostItem, string>
    {
        /// <summary>
        /// 判断是否存在代码相同的采购项
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id，新增时为空字符串</param>
        /// <param name="code">采购项代码</param>
        /// <returns>返回布尔值</returns>
        bool HasSameCode(DbTransaction trans, string id, string code);

        /// <summary>
        /// 查询采购项表信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回采购项数据集合</returns>
        List<CostItem> Search(string category, string isValid);

        /// <summary>
        /// 查询采购项表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回采购项数据集合</returns>
        List<CostItem> Search(string category, int pageIndex, int pageSize, ref int totalCount);
    }
}
