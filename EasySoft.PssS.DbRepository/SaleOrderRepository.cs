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
    public class SaleOrderRepository : BaseRepository<SaleOrder, string>, ISaleOrderRepository
    {
        #region 方法

        /// <summary>
        /// 查询销售订单表信息，用于列表分页显示
        /// </summary>
        /// <param name="item">产品项</param>
        /// <param name="status">状态</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回采购数据集合</returns>
        public List<SaleOrder> Search(string item, string status, int pageIndex, int pageSize, ref int totalCount)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (!string.IsNullOrEmpty(item))
            {
                conditions.Add("[Item] = @Item");
                para = DbHelper.CreateParameter("Item", DbType.String, Constant.STRING_LENGTH_10);
                para.Value = item;
                paras.Add(para);
            }
            if (!string.IsNullOrEmpty(status))
            {
                conditions.Add("[Status] = @Status");
                para = DbHelper.CreateParameter("Status", DbType.String, Constant.STRING_LENGTH_1);
                para.Value = status;
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
            return this.Paging(cmdText, pageSize, totalCount, pageIndex, "[Date] DESC, [CreateTime] DESC", paras.ToArray());
        }

        /// <summary>
        /// 查询需要快递的销售订单表信息
        /// </summary>
        /// <returns>返回采购数据集合</returns>
        public List<SaleOrder> SearchNeedExpress()
        {
            string cmdText = string.Format("{0} WHERE [NeedExpress] = '{1}' ORDER BY [Date] ASC", this.Resolver.SelectAllCommandText, Constant.COMMON_Y);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText);

            List<SaleOrder> entities = new List<SaleOrder>();
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
        protected override SaleOrder SetEntity(DbDataReader reader)
        {
            return new SaleOrder
            (
                reader["Id"].ToString(),
                Convert.ToDateTime(reader["Date"]),
                reader["CustomerId"].ToString(),
                reader["Address"].ToString(),
                reader["Mobile"].ToString(),
                reader["Linkman"].ToString(),
                reader["Item"].ToString(),
                Convert.ToDecimal(reader["Quantity"]),
                reader["Unit"].ToString(),
                reader["NeedExpress"].ToString(),
                reader["RecordId"].ToString(),
                Convert.ToDecimal(reader["Price"]),
                Convert.ToDecimal(reader["ActualAmount"]),
                Convert.ToDecimal(reader["Discount"]),
                reader["Status"].ToString(),
                reader["Remark"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"]),
                reader["Mender"].ToString(),
                Convert.ToDateTime(reader["ModifyTime"])
            );
        }

        #endregion

    }
}
