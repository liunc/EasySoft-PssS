// ----------------------------------------------------------
// 系统名称：EasySoft PssS
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
namespace EasySoft.PssS.DbRepository
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 数据库工具类
    /// </summary>
    public sealed class DbHelper
    {
        #region 变量

        private static readonly string CONNECTION_STRING = "SQLSERVER_CONNECTION_STRING";

        #endregion

        #region 属性

        /// <summary>
        /// 获取提供程序对数据源类的实现的实例
        /// </summary>
        private static DbProviderFactory DbProvider
        {
            get
            {
                return DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings[CONNECTION_STRING].ProviderName.Trim()); 
            }
        }

        #endregion

        #region 创建数据库连接

        /// <summary>
        /// 返回实现 DbConnection 类的提供程序的类的一个新实例。
        /// </summary>
        /// <returns>返回 DbConnection 的新实例。</returns>
        public static DbConnection CreateConnection()
        {
            DbConnection conn = DbProvider.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING].ConnectionString.Trim();
            return conn;
        }

        #endregion

        #region 执行SQL语句，返回数据集

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbDataAdapter adapter = DbProvider.CreateDataAdapter())
            {
                adapter.SelectCommand = DbProvider.CreateCommand();
                PrepareCommand(adapter.SelectCommand, trans.Connection, trans, cmdType, cmdText, paras);
                using (DataSet ds = new DataSet())
                {
                    adapter.Fill(ds);
                    adapter.SelectCommand.Parameters.Clear();
                    return ds;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(DbTransaction trans, string cmdText, params DbParameter[] paras)
        {
            return ExecuteDataSet(trans, CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(DbTransaction trans, CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(trans, CommandType.Text, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(DbTransaction trans, string cmdText)
        {
            return ExecuteDataSet(trans, CommandType.Text, cmdText);
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbDataAdapter da = DbProvider.CreateDataAdapter())
                {
                    da.SelectCommand = DbProvider.CreateCommand();
                    PrepareCommand(da.SelectCommand, conn, null, cmdType, cmdText, paras);
                    using (DataSet ds = new DataSet())
                    {
                        da.Fill(ds);
                        da.SelectCommand.Parameters.Clear();
                        return ds;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText)
        {
            return ExecuteDataSet(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(string cmdText, params DbParameter[] paras)
        {
            return ExecuteDataSet(CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回数据集
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据集</returns>
        public static DataSet ExecuteDataSet(string cmdText)
        {
            return ExecuteDataSet(cmdText, null);
        }

        #endregion

        #region 执行SQL语句，返回数据表

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbDataAdapter adapter = DbProvider.CreateDataAdapter())
            {
                adapter.SelectCommand = DbProvider.CreateCommand();
                PrepareCommand(adapter.SelectCommand, trans.Connection, trans, cmdType, cmdText, paras);
                using (DataTable dt = new DataTable())
                {
                    adapter.Fill(dt);
                    adapter.SelectCommand.Parameters.Clear();
                    return dt;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(DbTransaction trans, string cmdText, params DbParameter[] paras)
        {
            return ExecuteDataTable(trans, CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(DbTransaction trans, CommandType cmdType, string cmdText)
        {
            return ExecuteDataTable(trans, CommandType.Text, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(DbTransaction trans, string cmdText)
        {
            return ExecuteDataTable(trans, CommandType.Text, cmdText);
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbDataAdapter adapter = DbProvider.CreateDataAdapter())
                {
                    adapter.SelectCommand = DbProvider.CreateCommand();
                    PrepareCommand(adapter.SelectCommand, conn, null, cmdType, cmdText, paras);
                    using (DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);
                        adapter.SelectCommand.Parameters.Clear();
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(CommandType cmdType, string cmdText)
        {
            return ExecuteDataTable(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText, params DbParameter[] paras)
        {
            return ExecuteDataTable(CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回数据表
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回数据表</returns>
        public static DataTable ExecuteDataTable(string cmdText)
        {
            return ExecuteDataTable(cmdText, null);
        }

        #endregion

        #region 执行SQL语句，返回DataReader

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = DbProvider.CreateCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, paras);
                    DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    cmd.Parameters.Clear();
                    return dr;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText)
        {
            return ExecuteReader(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(string cmdText, params DbParameter[] paras)
        {
            return ExecuteReader(CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, null);
        }

        #endregion

        #region 执行SQL语句，返回结果集中的第一行第一列

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbCommand cmd = DbProvider.CreateCommand())
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, paras);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(DbTransaction trans, string cmdText, params DbParameter[] paras)
        {
            return ExecuteScalar(trans, CommandType.Text, cmdText, paras);
        }

        /// <summary>
        ///  执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(trans, cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(DbTransaction trans, string cmdText)
        {
            return ExecuteScalar(trans, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = DbProvider.CreateCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, paras);
                    object val = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(string cmdText, params DbParameter[] paras)
        {
            return ExecuteScalar(CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行第一列
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回结果集中的第一行第一列</returns>
        public static object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, null);
        }

        #endregion

        #region 执行SQL语句，返回受影响的行数

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbCommand cmd = DbProvider.CreateCommand())
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, paras);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(trans, cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction trans, string cmdText, params DbParameter[] paras)
        {
            return ExecuteNonQuery(trans, CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(DbTransaction trans, string cmdText)
        {
            return ExecuteNonQuery(trans, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbConnection conn = CreateConnection())
            {
                conn.Open();
                using (DbCommand cmd = DbProvider.CreateCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, paras);
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }


        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText, params DbParameter[] paras)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, paras);
        }
        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return ExecuteNonQuery(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回受影响的行数
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>执行SQL语句，返回受影响的行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化DbCommand 命令
        /// </summary>
        /// <param name="cmd">DbCommand 实例</param>
        /// <param name="conn">DbConnection 实例</param>
        /// <param name="trans">DbTransaction 实例</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            if (paras != null)
            {
                if (paras.Length > 0)
                {
                    foreach (DbParameter para in paras)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((para.Direction == ParameterDirection.InputOutput || para.Direction == ParameterDirection.Input || para.Direction == ParameterDirection.Output) && para.Value == null)
                        {
                            para.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(para);
                    }
                }
            }
        }

        #endregion

        #region 设置Parameter

        /// <summary>
        /// 设置SqlCommand参数
        /// </summary>
        /// <param name="parameter">SqlCommand参数对象</param>
        /// <param name="parameterValue">SqlCommand参数的值</param>
        /// <returns>返回SqlCommand参数</returns>
        public static DbParameter SetParameter(DbParameter parameter, object parameterValue)
        {
            if (string.IsNullOrEmpty(parameter.ParameterName))
            {
                throw new ArgumentNullException(("DbParameter.ParameterName"));
            }
            parameter.Value = parameterValue;
            return parameter;
        }

        /// <summary>
        /// 设置SqlCommand参数
        /// </summary>
        /// <param name="parameter">SqlCommand参数对象</param>
        /// <param name="parameterValue">SqlCommand参数的值</param>
        /// <param name="parameterValue">SqlCommand参数的默认值，如果传入的参数值等于默认值，则SqlCommand参数的值为DBNull.Value</param>
        /// <returns>返回SqlCommand参数</returns>
        public static DbParameter SetParameter(DbParameter parameter, object parameterValue, object defaultValue)
        {
            if (object.Equals(parameterValue, defaultValue))
            {
                return SetParameter(parameter, DBNull.Value);
            }
            return SetParameter(parameter, parameterValue);
        }

        #endregion

        #region 分页

        /// <summary>
        /// MSSQL Server2005以上版本获取分页的数据集(不能用distinct)
        /// </summary>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句，不包含order by</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <param name="pageIndex">要获取记录的页码</param>
        /// <param name="orderByStr">排序字符串，如"SERIALNO ASC,NAME DESC"</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回数据表</returns>
        public static DataTable Paging(string cmdText, int pageSize, int totalCount, int pageIndex, string orderByStr, params DbParameter[] paras)
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
            cmdText = string.Format("SELECT *FROM ({0} , ROW_NUMBER() OVER (ORDER BY {1}) AS RN {2}) RLT WHERE RLT.RN BETWEEN {3} AND {4}", cmdText1, orderByStr, cmdText2, start, end);
            return ExecuteDataSet(cmdText, paras).Tables[0];
        }

        #endregion
    }
}
