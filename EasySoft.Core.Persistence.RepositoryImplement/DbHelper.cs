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
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 数据库工具类
    /// </summary>
    public sealed class DbHelper
    {
        #region 变量

        private static readonly string CONNECTION_STRING = "SQLSERVER_CONNECTION_STRING";
        private static string providerName = string.Empty;
        private static DbProviderFactory dbProvider = null;

        #endregion

        #region 属性

        /// <summary>
        /// 获取ProviderName
        /// </summary>
        public static string ProviderName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(providerName))
                {
                    providerName = ConfigurationManager.ConnectionStrings[CONNECTION_STRING].ProviderName;
                    if (string.IsNullOrWhiteSpace(providerName))
                    {
                        throw new ArgumentNullException("ProviderName");
                    }
                    providerName = providerName.Trim();
                }
                return providerName;
            }
        }

        /// <summary>
        /// 获取提供程序对数据源类的实现的实例
        /// </summary>
        private static DbProviderFactory DbProvider
        {
            get
            {
                if (dbProvider == null)
                {
                    dbProvider = DbProviderFactories.GetFactory(ProviderName);
                    if (dbProvider == null)
                    {
                        throw new ArgumentNullException("DbProviderFactory");
                    }
                }
                return dbProvider;
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
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            using (DbCommand cmd = DbProvider.CreateCommand())
            {
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, paras);
                DbDataReader dr = cmd.ExecuteReader();
                cmd.Parameters.Clear();
                return dr;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(DbTransaction trans, string cmdText, params DbParameter[] paras)
        {
            return ExecuteReader(trans, CommandType.Text, cmdText, paras);
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(DbTransaction trans, string cmdText)
        {
            return ExecuteReader(trans, cmdText, null);
        }

        /// <summary>
        /// 执行SQL语句，返回DataReader
        /// </summary>
        /// <param name="cmdType">DbCommand 命令类型</param>
        /// <param name="cmdText">要执行的存储过程名或T-SQL语句</param>
        /// <param name="paras">DbParameter 参数集合</param>
        /// <returns>返回DataReader</returns>
        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] paras)
        {
            DbConnection conn = CreateConnection();
            conn.Open();
            DbCommand cmd = DbProvider.CreateCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, paras);
            DbDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return dr;
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

        #region 设置DbParameter

        /// <summary>
        /// 设置DbParameter
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">最大长度</param>
        /// <param name="value">DbParameter的值</param>
        /// <returns>返回DbParameter</returns>
        public static DbParameter CreateParameter(string name, DbType dbType, int size)
        {
            DbParameter parameter = DbProvider.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = dbType;
            if (size > 0)
            {
                parameter.Size = size;
            }
            return parameter;
        }

        /// <summary>
        /// 设置DbParameter
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">最大长度</param>
        /// <param name="value">DbParameter的值</param>
        /// <returns>返回DbParameter</returns>
        public static DbParameter SetParameter(string name, DbType dbType, int size, object value)
        {
            DbParameter parameter = CreateParameter(name, dbType, size);
            parameter.Value = value;
            return parameter;
        }

        /// <summary>
        /// 设置DbParameter
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">最大长度</param>
        /// <param name="value">DbParameter的值</param>
        /// <returns>返回DbParameter</returns>
        public static DbParameter SetParameter(string name, DbType dbType, object value)
        {
            return SetParameter(name, dbType, 0, value);
        }

        #endregion
    }
}