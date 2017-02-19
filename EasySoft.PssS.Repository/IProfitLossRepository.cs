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
    /// 益损仓储接口
    /// </summary>
    public interface IProfitLossRepository
    {
        /// <summary>
        /// 新增益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        void Insert(DbTransaction trans, ProfitLoss entity);

        /// <summary>
        /// 删除益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        void Delete(DbTransaction trans, string id);

        /// <summary>
        /// 根据Id获取一条益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回益损实体对象</returns>
        ProfitLoss Select(DbTransaction trans, string id);

        /// <summary>
        /// 根据记录Id获取益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        List<ProfitLoss> SearchByRecordId(DbTransaction trans, string recordId);
    }
}
