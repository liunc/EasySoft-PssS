// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域服务类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Service
{
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 成本领域服务类
    /// </summary>
    public class CostService
    {
        #region 变量

        private ICostRepository costRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CostService()
        {
            this.costRepository = new CostRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据Id获取成本项明细
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回成本项明细集合</returns>
        public List<Cost> GetList(string id)
        {
            return this.SearchByRecordId(null, id);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 新增成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        internal void Insert(DbTransaction trans, Cost entity)
        {
            this.costRepository.Insert(trans, entity);
        }

        /// <summary>
        /// 修改成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <param name="money">金额</param>
        internal void Update(DbTransaction trans, string id, decimal money)
        {
            this.costRepository.Update(trans, new Cost { Id = id, Money = money });
        }

        /// <summary>
        /// 根据记录Id获取成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        internal List<Cost> SearchByRecordId(DbTransaction trans, string recordId)
        {
            return this.costRepository.SearchByRecordId(trans, recordId);
        }

        /// <summary>
        /// 根据记录Id删除成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        internal void DeleteByRecordId(DbTransaction trans, string recordId)
        {
            this.costRepository.DeleteByRecordId(trans, recordId);
        }

        #endregion
    }
}
