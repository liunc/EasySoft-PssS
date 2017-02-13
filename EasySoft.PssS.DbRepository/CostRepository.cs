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
    using Domain.ValueObject;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// 成本仓储实现类
    /// </summary>
    public class CostRepository : ICostRepository
    {
        #region 方法

        /// <summary>
        /// 获取采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        public void Insert(DbTransaction trans, Cost entity)
        {
            string cmdText = @"INSERT INTO [dbo].[Cost]
                                   ([Id]
                                   ,[RecordId]
                                   ,[Category]
                                   ,[Item]
                                   ,[Money])
                             VALUES
                                   (@Id
                                   ,@RecordId 
                                   ,@Category 
                                   ,@Item 
                                   ,@Money)";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter(new SqlParameter("@Id", SqlDbType.Char, 32), entity.Id),
                    DbHelper.SetParameter(new SqlParameter("@RecordId", SqlDbType.Char, 32), entity.RecordId),
                    DbHelper.SetParameter(new SqlParameter("@Category", SqlDbType.VarChar, 10), entity.Category.ToString()),
                    DbHelper.SetParameter(new SqlParameter("@Item", SqlDbType.VarChar, 20), entity.Item),
                    DbHelper.SetParameter(new SqlParameter("@Money", SqlDbType.Decimal, 18), entity.Money)};

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 根据记录Id获取成本信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        public List<Cost> GetCostByRecordId(DbTransaction trans, string recordId)
        {
            string cmdText = @"SELECT [Id]
                                   ,[RecordId]
                                   ,[Category]
                                   ,[Item]
                                   ,[Money]
                            FROM [dbo].[Cost] WHERE [RecordId] = @RecordId";

            DbParameter paras = DbHelper.SetParameter(new SqlParameter("@RecordId", SqlDbType.Char, 32), recordId);

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, paras);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, paras);
            }
            List<Cost> entities = new List<Cost>();
            while (reader.Read())
            {
                entities.Add(this.SetEntity(reader));
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
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        private Cost SetEntity(DbDataReader reader)
        {
            return new Cost
            {
                Id = reader["Id"].ToString(),
                RecordId = reader["RecordId"].ToString(),
                Category = (CostCategory)Enum.Parse(typeof(CostCategory), reader["Category"].ToString()),
                Item = reader["Item"].ToString(),
                Money = Convert.ToDecimal(reader["Money"])
            };
        }

        #endregion

    }
}
