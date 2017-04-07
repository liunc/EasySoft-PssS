// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据库仓储实现库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Persistence.RepositoryImplement
{
    using Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Reflection;

    /// <summary>
    /// 数据库仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
    {
        #region 变量

        private PersistenceResolver resolver = null;

        #endregion

        #region 属性

        /// <summary>
        /// 获取PersistenceResolver
        /// </summary>
        protected PersistenceResolver Resolver
        {
            get
            {
                if (this.resolver == null)
                {
                    this.resolver = PersistenceResolverFactory.CreateResolver(typeof(TEntity));
                }
                return this.resolver;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        public void Insert(DbTransaction trans, TEntity entity)
        {
            string cmdText = this.Resolver.InsertCommandText;
            DbParameter[] parameters = this.GetParameters(entity, this.Resolver.InsertParameters);

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameters);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameters);
        }

        /// <summary>
        /// 更新数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        public void Update(DbTransaction trans, TEntity entity)
        {
            string cmdText = this.Resolver.UpdateCommandText;
            DbParameter[] parameters = this.GetParameters(entity, this.Resolver.UpdateParameters);

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameters);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameters);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        public void Delete(DbTransaction trans, TKey primaryKey)
        {
            string cmdText = this.Resolver.DeleteCommandText;
            DbParameter parameter = this.GetPrimaryKeyParameter(primaryKey, this.Resolver.PrimaryKeyParameters);

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameter);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameter);
        }

        /// <summary>
        /// 按主键查询一条数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        /// <returns>返回实体对象</returns>
        public TEntity Select(DbTransaction trans, TKey primaryKey)
        {
            string cmdText = this.Resolver.SelectByPrimaryKeysCommandText;
            DbParameter parameter = this.GetPrimaryKeyParameter(primaryKey, this.Resolver.PrimaryKeyParameters);

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, parameter);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, parameter);
            }
            TEntity entity = default(TEntity);
            while (reader.Read())
            {
                entity = this.SetEntity(reader);
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entity;
        }

        /// <summary>
        /// 按主键查询一条数据记录是否存在
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键字段</param>
        /// <returns>返回数据记录是否存在</returns>
        public bool Exists(DbTransaction trans, TKey primaryKey)
        {
            string cmdText = this.Resolver.CountByPrimaryKeysCommandText;
            DbParameter parameter = this.GetPrimaryKeyParameter(primaryKey, this.Resolver.PrimaryKeyParameters);

            object obj = null;
            if (trans == null)
            {
                obj = DbHelper.ExecuteScalar(cmdText, parameter);
            }
            else
            {
                obj = DbHelper.ExecuteScalar(trans, cmdText, parameter);
            }
            if (obj == null || obj == DBNull.Value)
            {
                return false;
            }
            return Convert.ToInt32(obj) == 0 ? false : true;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <param name="setEntity">转换为实体对象的方法</param>
        /// <param name="parameters">DbParameter 参数集合</param>
        /// <returns>返回分页查询结果</returns>
        protected List<TEntity> Paging(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr, Func<DbDataReader, TEntity> setEntity, params DbParameter[] parameters)
        {
            cmdText = this.Resolver.GetPagingSqlString(cmdText, pageSize, totalCount, pageIndex, orderByStr);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, parameters);
            List<TEntity> entities = new List<TEntity>();
            while (reader.Read())
            {
                entities.Add(setEntity(reader));
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entities;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <param name="parameters">DbParameter 参数集合</param>
        /// <returns>返回分页查询结果</returns>
        protected List<TEntity> Paging(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr, params DbParameter[] parameters)
        {
            return this.Paging(cmdText,  pageSize,  totalCount,  pageIndex,  orderByStr, this.SetEntity, parameters);
        }

        /// <summary>
        /// 获取实体属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">实体对象属性名</param>
        /// <returns>返回属性值</returns>
        protected virtual TEntity SetEntity(DbDataReader reader)
        {
            return default(TEntity);
        }
        

        private DbParameter[] GetParameters(TEntity entity, Dictionary<string, DbParameter> source)
        {
            DbParameter[] parameters = new DbParameter[source.Count];
            source.Values.CopyTo(parameters, 0);
            int i = 0;
            foreach (KeyValuePair<string, DbParameter> pair in source)
            {
                parameters[i].Value = entity.GetType().InvokeMember(pair.Key, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, entity, new object[0]);
                i++;
            }
            return parameters;
        }

        private DbParameter GetPrimaryKeyParameter(TKey primaryKey, Dictionary<string, DbParameter> source)
        {
            DbParameter[] parameters = null;
            source.Values.CopyTo(parameters, 0);
            parameters[0].Value = primaryKey;
            return parameters[0];
        }

        #endregion
    }
}
