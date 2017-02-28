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

    /// <summary>
    /// 益损仓储实现类
    /// </summary>
    public class ProfitLossRepository : IProfitLossRepository
    {
        #region 常量

        private static readonly string SELECT_SQLSTRING = @"SELECT [Id],[RecordId], [TargetType], [Category], [Quantity], [Remark], [Creator], [CreateTime] FROM [dbo].[ProfitLoss] {0}";
        #endregion

        #region 方法 

        /// <summary>
        /// 新增益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        public void Insert(DbTransaction trans, ProfitLoss entity)
        {
            string cmdText = @"INSERT INTO [dbo].[ProfitLoss]([Id], [RecordId], [TargetType],[Category], [Quantity], [Remark], [Creator], [CreateTime])
                             VALUES(@Id, @RecordId, @TargetType, @Category, @Quantity, @Remark, @Creator, @CreateTime)";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter("Id", DbType.String, 32, entity.Id),
                    DbHelper.SetParameter("RecordId", DbType.String, 32, entity.RecordId),
                    DbHelper.SetParameter("TargetType", DbType.String, 10, entity.TargetType.ToString()),
                    DbHelper.SetParameter("Category", DbType.String, 10, entity.Category.ToString()),
                    DbHelper.SetParameter("Quantity", DbType.Decimal, 18, entity.Quantity),
                    DbHelper.SetParameter("Remark", DbType.String, 120, entity.Remark),
                    DbHelper.SetParameter("Creator", DbType.String, 20, entity.Creator.UserId),
                    DbHelper.SetParameter("CreateTime", DbType.DateTime, entity.Creator.Time)};

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 删除益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        public void Delete(DbTransaction trans, string id)
        {
            string cmdText = @"DELETE FROM [dbo].[ProfitLoss] WHERE [Id] = @Id";

            DbParameter paras = DbHelper.SetParameter("Id", DbType.String, 32, id);

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 根据Id获取一条益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回益损实体对象</returns>
        public ProfitLoss Select(DbTransaction trans, string id)
        {
            string cmdText = string.Format(SELECT_SQLSTRING, "WHERE [Id] = @Id");

            DbParameter paras = DbHelper.SetParameter("Id", DbType.String, 32, id);

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, paras);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, paras);
            }
            ProfitLoss entity = null;
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
        /// 根据记录Id获取益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        public List<ProfitLoss> SearchByRecordId(DbTransaction trans, string recordId)
        {
            string cmdText = string.Format(SELECT_SQLSTRING, "WHERE [RecordId] = @RecordId");

            DbParameter paras = DbHelper.SetParameter("RecordId", DbType.String, 32, recordId);

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, paras);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, paras);
            }
            List<ProfitLoss> entities = new List<ProfitLoss>();
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
        private ProfitLoss SetEntity(DbDataReader reader)
        {
            return new ProfitLoss
            {
                Id = reader["Id"].ToString(),
                RecordId = reader["RecordId"].ToString(),
                TargetType = (ProfitLossTargetType)Enum.Parse(typeof(ProfitLossTargetType), reader["TargetType"].ToString()),
                Category = (ProfitLossCategory)Enum.Parse(typeof(ProfitLossCategory), reader["Category"].ToString()),
                Remark = reader["Remark"].ToString(),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                Creator = new Operator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"]))
            };
        }

        #endregion

    }
}
