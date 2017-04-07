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
    /// 成本项仓储接口
    /// </summary>
    public interface ICostRepository:IBaseRepository<Cost, string>
    {

        /// <summary>
        /// 根据记录Id获取成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        void DeleteByRecordId(DbTransaction trans, string recordId);

        /// <summary>
        /// 根据记录Id获取成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        List<Cost> SearchByRecordId(DbTransaction trans, string recordId);
    }
}
