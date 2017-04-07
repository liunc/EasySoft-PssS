// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据库仓储类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Persistence.Stereotype
{
    using System;
    using System.Data.Common;
    using System.Reflection;
    using System.Collections.Generic;

    /// <summary>
    /// 对象关系映射解析类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class PersistenceResolve<TEntity, TKey>
    {
        #region 变量

        private BindingFlags bindingFlag = BindingFlags.Public | BindingFlags.Instance;
        private Type entityType = null;
        private string tableName = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 获取BindingFlags
        /// </summary>
        protected BindingFlags BindingFlag
        {
            get
            {
                return BindingFlags.Public | BindingFlags.Instance;
            }
        }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        protected Type EntityType
        {
            get
            {
                if (this.entityType == null)
                {
                    this.entityType = typeof(TEntity);
                }
                return this.entityType;
            }
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        protected string TableName
        {

            get
            {
                if (string.IsNullOrWhiteSpace(this.tableName))
                {
                    string key = string.Format("{0}_TableName", this.EntityType.ToString());
                    this.tableName = PersistenceCache.GetStringObjects(key);
                    if (string.IsNullOrWhiteSpace(this.tableName))
                    {
                        object[] customAttributes = this.EntityType.GetCustomAttributes(typeof(TableAttribute), true);
                        if (customAttributes != null && customAttributes.Length > 0)
                        {
                            TableAttribute attribute = customAttributes[0] as TableAttribute;
                            this.tableName = attribute.Name;
                            PersistenceCache.SetStringObjects(key, this.tableName);
                        }
                    }
                }
                return this.tableName;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PersistenceResolve()
        {
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        public virtual void Insert(DbTransaction trans, TEntity entity)
        {
            List<string> columnNames = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    ColumnAttribute colAttr = customAttributes[0] as ColumnAttribute;
                    if (!colAttr.Identity)
                    {
                        string columnName = this.GetColumnName(colAttr.Name, info.Name);
                        columnNames.Add(columnName);
                        parameters.Add(DbHelper.SetParameter(columnName, colAttr.DataType, colAttr.Size, this.GetPropertyValue(entity, info.Name)));
                    }
                }
            }
            string cmdText = string.Format("INSERT INTO {0}({1}) VALUES(@{2});", this.TableName, string.Join(", ", columnNames), string.Join(", @", columnNames));
            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameters.ToArray());
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameters.ToArray());
        }

        /// <summary>
        /// 更新数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体泛型对象</param>
        public virtual void Update(DbTransaction trans, TEntity entity)
        {
            List<string> updateColumns = new List<string>();
            List<string> primaryKeys = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    ColumnAttribute colAttr = customAttributes[0] as ColumnAttribute;
                    string columnName = this.GetColumnName(colAttr.Name, info.Name);
                    if (colAttr.PrimaryKey)
                    {
                        primaryKeys.Add(string.Format("{0} = @{0}", columnName));
                    }
                    else if (colAttr.AllowEdit)
                    {
                        updateColumns.Add(string.Format("{0} = @{0}", columnName));
                    }
                    else
                    {
                        continue;
                    }
                    parameters.Add(DbHelper.SetParameter(columnName, colAttr.DataType, colAttr.Size, this.GetPropertyValue(entity, info.Name)));
                }
            }
            string cmdText = string.Format("UPDATE {0} SET {1} WHERE {2};", this.TableName, string.Join(", ", updateColumns), string.Join(" AND ", primaryKeys));
            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameters.ToArray());
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameters.ToArray());
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键值</param>
        public virtual void Delete(DbTransaction trans, TKey primaryKey)
        {
            List<string> primaryKeys = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            this.GetPrimaryKeyParameter(primaryKey, out primaryKeys, out parameters);
            string cmdText = string.Format("DELETE FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", primaryKeys));
            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameters.ToArray());
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameters.ToArray());
        }

        /// <summary>
        /// 按主键查询一条数据记录
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="primaryKey">主键值</param>
        /// <param name="setEntity">转换为实体对象的方法</param>
        /// <returns>返回实体对象</returns>
        public virtual TEntity Select(DbTransaction trans, TKey primaryKey, Func<DbDataReader, TEntity> setEntity)
        {
            List<string> selectColumns = new List<string>();
            List<string> primaryKeys = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    ColumnAttribute colAttr = customAttributes[0] as ColumnAttribute;
                    string columnName = this.GetColumnName(colAttr.Name, info.Name);
                    if (colAttr.PrimaryKey)
                    {
                        primaryKeys.Add(string.Format("{0} = @{0}", columnName));
                        parameters.Add(DbHelper.SetParameter(columnName, colAttr.DataType, colAttr.Size, primaryKey));
                    }
                    selectColumns.Add(columnName);
                }
            }
            string cmdText = string.Format("SELECT {0} FROM {1} WHERE {2}", string.Join(", ", selectColumns), this.TableName, string.Join(" AND ", primaryKeys));

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, parameters.ToArray());
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, parameters.ToArray());
            }
            TEntity entity = default(TEntity);
            while (reader.Read())
            {
                entity = setEntity(reader);
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
        /// <param name="primaryKey">主键值</param>
        /// <returns>返回数据记录是否存在</returns>
        public virtual bool Exists(DbTransaction trans, TKey primaryKey)
        {
            List<string> primaryKeys = new List<string>();
            List<DbParameter> parameters = new List<DbParameter>();
            this.GetPrimaryKeyParameter(primaryKey, out primaryKeys, out parameters);
            string cmdText = string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", primaryKeys));
            object obj = null;
            if (trans == null)
            {
                obj = DbHelper.ExecuteScalar(cmdText, parameters.ToArray());
            }
            else
            {
                obj = DbHelper.ExecuteScalar(trans, cmdText, parameters.ToArray());
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
        public virtual List<TEntity> Paging(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr, Func<DbDataReader, TEntity> setEntity, params DbParameter[] parameters)
        {
            cmdText = this.GetPagingSqlString(cmdText, pageSize, totalCount, pageIndex, orderByStr);
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取列名称
        /// </summary>
        /// <param name="colAttrName">在实体对象中定义的列属性名称</param>
        /// <param name="propertyName">实体对象属性名</param>
        /// <returns></returns>
        private string GetColumnName(string colAttrName, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(colAttrName))
            {
                return propertyName;
            }
            return colAttrName;
        }

        /// <summary>
        /// 获取实体属性值
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="propertyName">实体对象属性名</param>
        /// <returns>返回属性值</returns>
        private object GetPropertyValue(TEntity entity, string propertyName)
        {
            return entity.GetType().InvokeMember(propertyName, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, entity, new object[0]);
        }

        /// <summary>
        /// 获取主键列与参数
        /// </summary>
        /// <param name="primaryKeyValue">实体对象</param>
        /// <param name="primaryKeys">主键列</param>
        /// <param name="parameters">参数</param>
        private void GetPrimaryKeyParameter(TKey primaryKeyValue, out List<string> primaryKeys, out List<DbParameter> parameters)
        {
            primaryKeys = new List<string>();
            parameters = new List<DbParameter>();
            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                IEnumerable<ColumnAttribute> columnAttributes = info.GetCustomAttributes<ColumnAttribute>(true);
                foreach (ColumnAttribute colAttr in columnAttributes)
                {
                    if (colAttr.PrimaryKey)
                    {
                        string columnName = this.GetColumnName(colAttr.Name, info.Name);
                        primaryKeys.Add(string.Format("{0} = @{0}", columnName));
                        parameters.Add(DbHelper.SetParameter(columnName, colAttr.DataType, colAttr.Size, primaryKeyValue));
                    }
                }
            }
        }

        private void Init()
        {
            List<string> allFields = new List<string>();
            List<string> insertFields = new List<string>();
            List<string> updateFields = new List<string>();
            List<string> primaryKeys = new List<string>();
            List<DbParameter> insertParameters = new List<DbParameter>();
            List<DbParameter> updateParameters = new List<DbParameter>();
            List<DbParameter> primaryParameters = new List<DbParameter>();

            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    ColumnAttribute colAttr = customAttributes[0] as ColumnAttribute;
                    string columnName = this.GetColumnName(colAttr.Name, info.Name);
                    allFields.Add(columnName);
                    if (!colAttr.Identity)
                    {
                        insertFields.Add(columnName);
                    }
                    if (colAttr.PrimaryKey)
                    {
                        primaryKeys.Add(columnName);
                    }
                    else
                    {
                        if (colAttr.AllowEdit)
                        {
                            updateFields.Add(columnName);
                        }
                    }


                    
                      //  parameters.Add(DbHelper.SetParameter(columnName, colAttr.DataType, colAttr.Size, this.GetPropertyValue(entity, info.Name)));
                    
                }
            }
        }
        #endregion

        #region 抽象方法，由子类实现

        /// <summary>
        /// MSSQL Server2005以上版本获取分页的数据集(不能用distinct)
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <returns>返回分页Sql字符串</returns>
        protected abstract string GetPagingSqlString(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr);

        #endregion
    }
}