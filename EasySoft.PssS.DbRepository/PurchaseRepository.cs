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
    using EasySoft.Core.Util;
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 采购项仓储实现类
    /// </summary>
    public class PurchaseRepository : BaseRepository<Purchase, string>, IPurchaseRepository
    {
        #region 方法

        /// <summary>
        /// 查询采购表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="item">产品项</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<Purchase> Search(string category, string item, int pageIndex, int pageSize, ref int totalCount)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                para = DbHelper.CreateParameter("Category", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = category;
                paras.Add(para);
            }
            if (!string.IsNullOrEmpty(item))
            {
                conditions.Add("[Item] = @Item");
                para = DbHelper.CreateParameter("Item", DbType.String, Constant.STRING_LENGTH_10);
                para.Value = item;
                paras.Add(para);
            }
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            string cmdText = string.Format("{0} {1}", this.Resolver.SelectAllCommandText, whereCmdText);
            string totalCmdText = string.Format("{0} {1}", this.Resolver.CountAllCommandText, whereCmdText);
            totalCount = Convert.ToInt32(DbHelper.ExecuteScalar(totalCmdText, paras.ToArray()));
            return this.Paging(cmdText, pageSize, totalCount, pageIndex, "[CreateTime] DESC", paras.ToArray());
        }

        /// <summary>
        /// 查询可交付的采购数据
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <returns>返回采购数据集合</returns>
        public List<Purchase> GetDeliverable(string category)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                para = DbHelper.CreateParameter("Category", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = category;
                paras.Add(para);
            }
            conditions.Add(string.Format("[Status] <> '{0}'", PurchaseStatus.Finished));
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            string cmdText = string.Format("{0} {1} ORDER BY [CreateTime] DESC", this.Resolver.SelectAllCommandText, whereCmdText);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, paras.ToArray());

            List<Purchase> entities = new List<Purchase>();
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
        protected override Purchase SetEntity(DbDataReader reader)
        {
            return new Purchase
            (
                reader["Id"].ToString(),
                Convert.ToDateTime(reader["Date"]),
                reader["Category"].ToString(),
                reader["Item"].ToString(),
                Convert.ToDecimal(reader["Quantity"]),
                reader["Unit"].ToString(),
                reader["Supplier"].ToString(),
                Convert.ToDecimal(reader["Inventory"]),
                Convert.ToDecimal(reader["Cost"]),
                Convert.ToDecimal(reader["ProfitLoss"]),
                reader["Remark"].ToString(),
                reader["Status"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"]),
                reader["Mender"].ToString(),
                Convert.ToDateTime(reader["ModifyTime"])
            );
        }

        #endregion

    }
}
