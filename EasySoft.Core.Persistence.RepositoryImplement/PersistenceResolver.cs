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
    using System;
    using System.Data.Common;
    using System.Reflection;
    using System.Collections.Generic;
    using Persistence;
    using System.Data;

    /// <summary>
    /// 持久化解析器类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public abstract class PersistenceResolver
    {
        #region 变量
        
        private string tableName = string.Empty;
        private List<string> allColumns = new List<string>();
        private List<string> insertColumns = new List<string>();
        private List<string> updateColumns = new List<string>();
        private List<string> primaryKeyColumns = new List<string>();
        private Dictionary<string, DbParameter> insertParameters = new Dictionary<string, DbParameter>();
        private Dictionary<string, DbParameter> updateParameters = new Dictionary<string, DbParameter>();
        private Dictionary<string, DbParameter> primaryKeyParameters = new Dictionary<string, DbParameter>();

        #endregion

        #region 属性

        /// <summary>
        /// 获取BindingFlags
        /// </summary>
        private BindingFlags BindingFlag
        {
            get
            {
                return BindingFlags.Public | BindingFlags.Instance;
            }
        }

        /// <summary>
        /// 获取实体类型
        /// </summary>
        public Type EntityType
        {
            get;
            set;
        }

        /// <summary>
        /// 获取数据表名称
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.tableName))
                {
                    string key =  this.GetCacheKey("TableName");
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

        private string AllColumnsKey
        {
            get
            {
                return this.GetCacheKey("AllColumns"); 
            }
        }

        private string InsertColumnsKey
        {
            get
            {
                return this.GetCacheKey("InsertColumns"); 
            }
        }

        private string UpdateColumnsKey
        {
            get
            {
                return this.GetCacheKey("UpdateColumns");
            }
        }

        private string PrimaryKeyColumnsKey
        {
            get
            {
                return this.GetCacheKey("PrimaryKeyColumns");
            }
        }

        private string InsertParametersKey
        {
            get
            {
                return this.GetCacheKey("InsertParameters");
            }
        }

        private string UpdateParametersKey
        {
            get
            {
                return this.GetCacheKey("UpdateParameters");
            }
        }

        private string PrimaryKeyParametersKey
        {
            get
            {
                return this.GetCacheKey("PrimaryKeyParameters");
            }
        }

        public List<string> AllColumns
        {
            get
            {
                if (this.allColumns == null || this.allColumns.Count == 0)
                {
                    this.allColumns = PersistenceCache.GetListObjects(this.AllColumnsKey);
                    if (this.allColumns == null || this.allColumns.Count == 0)
                    {
                        this.allColumns = new List<string>();
                        this.Init();
                    }
                }
                return this.allColumns;
            }
        }

        public List<string> InsertColumns
        {
            get
            {
                if (this.insertColumns == null || this.insertColumns.Count == 0)
                {
                    this.insertColumns = PersistenceCache.GetListObjects(this.InsertColumnsKey);
                    if (this.insertColumns == null || this.insertColumns.Count == 0)
                    {
                        this.insertColumns = new List<string>();
                        this.Init();
                    }
                }
                return this.insertColumns;
            }
        }

        public List<string> UpdateColumns
        {
            get
            {
                if (this.updateColumns == null || this.updateColumns.Count == 0)
                {
                    this.updateColumns = PersistenceCache.GetListObjects(this.UpdateColumnsKey);
                    if (this.updateColumns == null || this.updateColumns.Count == 0)
                    {
                        this.updateColumns = new List<string>();
                        this.Init();
                    }
                }
                return this.updateColumns;
            }
        }

        public List<string> PrimaryKeyColumns
        {
            get
            {
                if (this.primaryKeyColumns == null || this.primaryKeyColumns.Count == 0)
                {
                    this.primaryKeyColumns = PersistenceCache.GetListObjects(this.PrimaryKeyColumnsKey);
                    if (this.primaryKeyColumns == null || this.primaryKeyColumns.Count == 0)
                    {
                        this.primaryKeyColumns = new List<string>();
                        this.Init();
                    }
                }
                return this.primaryKeyColumns;
            }
        }

        public Dictionary<string, DbParameter> InsertParameters
        {
            get
            {
                if (this.insertParameters == null || this.insertParameters.Count == 0)
                {
                    this.insertParameters = PersistenceCache.GetDbParameters(this.InsertParametersKey);
                    if (this.insertParameters == null || this.insertParameters.Count == 0)
                    {
                        this.insertParameters = new Dictionary<string, DbParameter>();
                        this.Init();
                    }
                }
                return this.insertParameters;
            }
        }

        public Dictionary<string, DbParameter> UpdateParameters
        {
            get
            {
                if (this.updateParameters == null || this.updateParameters.Count == 0)
                {
                    this.updateParameters = PersistenceCache.GetDbParameters(this.UpdateParametersKey);
                    if (this.updateParameters == null || this.updateParameters.Count == 0)
                    {
                        this.updateParameters = new Dictionary<string, DbParameter>();
                        this.Init();
                    }
                }
                return this.updateParameters;
            }
        }

        public Dictionary<string, DbParameter> PrimaryKeyParameters
        {
            get
            {
                if (this.primaryKeyParameters == null || this.primaryKeyParameters.Count == 0)
                {
                    this.primaryKeyParameters = PersistenceCache.GetDbParameters(this.PrimaryKeyParametersKey);
                    if (this.primaryKeyParameters == null || this.primaryKeyParameters.Count == 0)
                    {
                        this.primaryKeyParameters = new Dictionary<string, DbParameter>();
                        this.Init();
                    }
                }
                return this.primaryKeyParameters;
            }
        }

        /// <summary>
        /// 获取Insert SQL命令
        /// </summary>
        public virtual string InsertCommandText
        {
            get
            {
                return string.Format("INSERT INTO {0}({1}) VALUES(@{2});", this.TableName, string.Join(", ", this.InsertColumns), string.Join(", @", this.InsertColumns));
            }
        }

        /// <summary>
        /// 获取Update SQL命令
        /// </summary>
        public virtual string UpdateCommandText
        {
            get
            {
                return string.Format("UPDATE {0} SET {1} WHERE {2};", this.TableName, string.Join(", ", this.UpdateColumns), string.Join(" AND ", this.PrimaryKeyColumns)); 
            }
        }

        /// <summary>
        /// 获取Update SQL命令
        /// </summary>
        public virtual string DeleteCommandText
        {
            get
            {
                return string.Format("DELETE FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", this.PrimaryKeyColumns));
            }
        }

        /// <summary>
        /// 获取Select By Primary Keys SQL命令
        /// </summary>
        public virtual string SelectByPrimaryKeysCommandText
        {
            get
            {
                return string.Format("SELECT {0} FROM {1} WHERE {2}", string.Join(", ", this.AllColumns), this.TableName, string.Join(" AND ", this.PrimaryKeyColumns));
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public virtual string SelectAllCommandText
        {
            get
            {
                return string.Format("SELECT {0} FROM {1}", string.Join(", ", this.AllColumns), this.TableName);
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public virtual string CountByPrimaryKeysCommandText
        {
            get
            {
                return string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", this.PrimaryKeyColumns));
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public virtual string CountAllCommandText
        {
            get
            {
                return string.Format("SELECT COUNT(1) FROM {0}", this.TableName);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// MSSQL Server2005以上版本获取分页的数据集(不能用distinct)
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <returns>返回分页Sql字符串</returns>

        public abstract string GetPagingSqlString(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr);

        /// <summary>
        /// 获取缓存关键字
        /// </summary>
        /// <param name="flag">标志</param>
        /// <returns>返回组成的缓存关键字符串</returns>
        private string GetCacheKey(string flag)
        {
            return string.Format("{0}_{1}", this.EntityType.ToString(), flag);
        }

        /// <summary>
        /// 获取列名称
        /// </summary>
        /// <param name="colAttrName">在实体对象中定义的列属性名称</param>
        /// <param name="propertyName">实体对象属性名</param>
        /// <returns>如何定义了列属性名称，则返回列属性名称，否则返回对象属性命名</returns>
        private string GetColumnName(string colAttrName, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(colAttrName))
            {
                return propertyName;
            }
            return colAttrName;
        }

        private void Init()
        {
            foreach (PropertyInfo info in this.EntityType.GetProperties(this.BindingFlag))
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (customAttributes != null && customAttributes.Length > 0)
                {
                    ColumnAttribute colAttr = customAttributes[0] as ColumnAttribute;
                    string columnName = this.GetColumnName(colAttr.Name, info.Name);
                    DbParameter parameter = DbHelper.CreateParameter(columnName, colAttr.DataType, colAttr.Size);
                    this.allColumns.Add(columnName);
                    if (!colAttr.Identity)
                    {
                        this.insertColumns.Add(columnName);
                        this.insertParameters.Add(info.Name, parameter);
                    }
                    if (colAttr.PrimaryKey)
                    {
                        this.primaryKeyColumns.Add(columnName);
                        this.primaryKeyParameters.Add(info.Name, parameter);
                        this.updateParameters.Add(info.Name, parameter);
                    }
                    else
                    {
                        if (colAttr.AllowEdit)
                        {
                            this.updateColumns.Add(columnName);
                            this.updateParameters.Add(info.Name, parameter);
                        }
                    }
                }
            }
            
            PersistenceCache.SetListObjects(this.AllColumnsKey, this.allColumns);
            PersistenceCache.SetListObjects(this.InsertColumnsKey, this.insertColumns);
            PersistenceCache.SetListObjects(this.UpdateColumnsKey, this.updateColumns);
            PersistenceCache.SetListObjects(this.PrimaryKeyColumnsKey, this.primaryKeyColumns);
            PersistenceCache.SetDbParameters(this.InsertParametersKey, this.insertParameters);
            PersistenceCache.SetDbParameters(this.UpdateParametersKey, this.updateParameters);
            PersistenceCache.SetDbParameters(this.PrimaryKeyParametersKey, this.primaryKeyParameters);
        }
        

        #endregion

    }
}