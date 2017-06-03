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
namespace EasySoft.Core.Persistence.RepositoryImplement
{
    using System.Collections.Generic;

    /// <summary>
    /// MSSQL对象关系映射解析类
    /// </summary>
    /// <typeparam name="TEntity">实体对象类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public class SqlClientPersistenceResolver : PersistenceResolver
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlClientPersistenceResolver()
        {

        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取Insert SQL命令
        /// </summary>
        public override string InsertCommandText
        {
            get
            {
                return string.Format("INSERT INTO {0}({1}) VALUES(@{2});", this.TableName, string.Join(", ", this.InsertColumns), string.Join(", @", this.InsertColumns));
            }
        }

        /// <summary>
        /// 获取Update SQL命令
        /// </summary>
        public override string UpdateCommandText
        {
            get
            {
                return string.Format("UPDATE {0} SET {1} WHERE {2};", this.TableName, string.Join(", ", this.FormatColumn(this.UpdateColumns)), string.Join(" AND ", this.FormatColumn(this.PrimaryKeyColumns)));
            }
        }

        /// <summary>
        /// 获取Update SQL命令
        /// </summary>
        public override string DeleteCommandText
        {
            get
            {
                return string.Format("DELETE FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", this.FormatColumn(this.PrimaryKeyColumns)));
            }
        }

        /// <summary>
        /// 获取Select By Primary Keys SQL命令
        /// </summary>
        public override string SelectByPrimaryKeysCommandText
        {
            get
            {
                return string.Format("SELECT {0} FROM {1} WHERE {2}", string.Join(", ", this.AllColumns), this.TableName, string.Join(" AND ", this.FormatColumn(this.PrimaryKeyColumns)));
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public override string SelectAllCommandText
        {
            get
            {
                return string.Format("SELECT {0} FROM {1}", string.Join(", ", this.AllColumns), this.TableName);
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public override string CountByPrimaryKeysCommandText
        {
            get
            {
                return string.Format("SELECT COUNT(1) FROM {0} WHERE {1}", this.TableName, string.Join(" AND ", this.FormatColumn(this.PrimaryKeyColumns)));
            }
        }

        /// <summary>
        /// 获取Select All SQL命令
        /// </summary>
        public override string CountAllCommandText
        {
            get
            {
                return string.Format("SELECT COUNT(1) FROM {0}", this.TableName);
            }
        }

        #endregion

        #region 分页Sql字符串

        /// <summary>
        /// MSSQL Server2005以上版本获取分页的数据集(不能用distinct)
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <returns>返回分页Sql字符串</returns>
        public override string GetPagingSqlString(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr)
        {
            int index = cmdText.ToUpper().IndexOf("FROM");
            string cmdText1 = cmdText.Substring(0, index);
            string cmdText2 = cmdText.Substring(index);

            int pageCount = totalCount == 0 ? 1 : (totalCount / pageSize) + (totalCount % pageSize == 0 ? 0 : 1);
            if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            int start = (pageIndex - 1) * pageSize + 1;
            int end = 0;
            if (pageIndex == pageCount)
            {
                end = totalCount;
            }
            else
            {
                end = pageSize * pageIndex;
            }
            return string.Format("SELECT * FROM ({0} , ROW_NUMBER() OVER (ORDER BY {1}) AS RN {2}) RLT WHERE RLT.RN BETWEEN {3} AND {4}", cmdText1, orderByStr, cmdText2, start, end);
        }

        #endregion

        #region 私有方法

        private List<string> FormatColumn(List<string> columns)
        {
            List<string> formatedColumns = new List<string>();
            foreach (string column in columns)
            {
                formatedColumns.Add(string.Format("{0} = @{0}", column));
            }
            return formatedColumns;
        }

        #endregion
    }
}
