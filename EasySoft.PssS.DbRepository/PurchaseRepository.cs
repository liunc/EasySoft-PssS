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
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                paras.Add(DbHelper.SetParameter("Category", DbType.String, 10, category));
            }
            if (!string.IsNullOrEmpty(item))
            {
                conditions.Add("[Item] = @Item");
                paras.Add(DbHelper.SetParameter("Item", DbType.String, 20, item));
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override Purchase SetEntity(DbDataReader reader)
        {
            Purchase entity = new Purchase
            {
                Id = reader["Id"].ToString(),
                Date = Convert.ToDateTime(reader["Date"]),
                Category = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), reader["Category"].ToString()),
                Item = reader["Item"].ToString(),
                Quantity = Convert.ToDecimal(reader["Quantity"]),
                Unit = reader["Unit"].ToString(),
                Supplier = reader["Supplier"].ToString(),
                Allowance = Convert.ToDecimal(reader["Allowance"]),
                Cost = Convert.ToDecimal(reader["Cost"]),
                ProfitLoss = Convert.ToDecimal(reader["ProfitLoss"]),
                Remark = reader["Remark"].ToString(),
                Status = (PurchaseStatus)Enum.Parse(typeof(PurchaseStatus), reader["Status"].ToString())
            };
            entity.SetCreator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"]));
            entity.SetMender(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]));
            return entity;
        }

        #endregion

    }
}
