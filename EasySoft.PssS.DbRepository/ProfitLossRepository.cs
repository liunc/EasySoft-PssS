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
    using Core.Persistence.RepositoryImplement;
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
    public class ProfitLossRepository : BaseRepository<ProfitLoss, string>, IProfitLossRepository
    {
         #region 方法 

        /// <summary>
        /// 根据记录Id获取益损信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回成本信息</returns>
        public List<ProfitLoss> SearchByRecordId(DbTransaction trans, string recordId)
        {
            string cmdText = string.Format("{0} WHERE [RecordId] = @RecordId", this.Resolver.SelectAllCommandText);

            DbParameter paras = DbHelper.CreateParameter("RecordId", DbType.String, 32);
            paras.Value = recordId;
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
        protected override ProfitLoss SetEntity(DbDataReader reader)
        {
            return new ProfitLoss
            (
                reader["Id"].ToString(),
                reader["RecordId"].ToString(),
                reader["TargetType"].ToString(),
                reader["Category"].ToString(),
                Convert.ToDecimal(reader["Quantity"]),
                reader["Remark"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"])
            );
        }

        #endregion

    }
}
