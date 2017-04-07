// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据库仓储接口库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Persistence.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 数据库仓储基接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键列类型</typeparam>
    public interface IBaseRepository<TEntity, TKey>
    {
        /// <summary>
        /// 新增数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        void Insert(DbTransaction trans, TEntity entity);

        /// <summary>
        /// 更新数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        void Update(DbTransaction trans, TEntity entity);

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        void Delete(DbTransaction trans, TKey primaryKey);

        /// <summary>
        /// 按主键查询一条数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        /// <returns>返回实体对象</returns>
        TEntity Select(DbTransaction trans, TKey primaryKey);

        /// <summary>
        /// 按主键查询一条数据记录是否存在
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        /// <returns>返回数据记录是否存在</returns>
        bool Exists(DbTransaction trans, TKey primaryKey);
    }
}
