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
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 交付仓储实现类
    /// </summary>
    public class DeliveryRepository : BaseRepository<Delivery, string>, IDeliveryRepository
    {
        #region 方法

        /// <summary>
        /// 查询交付表信息，用于列表分页显示
        /// </summary>
        /// <param name="startDate">查询开始日期</param>
        /// <param name="endDate">查询结束日期</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回交付数据集合</returns>
        public List<Delivery> Search(DateTime startDate, DateTime endDate, int pageIndex, int pageSize, ref int totalCount)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            DbParameter para = null;
            if (startDate != DateTime.MinValue)
            {
                conditions.Add("[Date] >= @StartDate");
                para = DbHelper.CreateParameter("StartDate", DbType.Date, Constant.INT_ZERO);
                para.Value = startDate;
                paras.Add(para);
            }
            if (endDate != DateTime.MinValue)
            {
                conditions.Add("[Date] <= @EndDate");
                para = DbHelper.CreateParameter("EndDate", DbType.Date, Constant.INT_ZERO);
                para.Value = endDate;
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override Delivery SetEntity(DbDataReader reader)
        {
            return new Delivery
            (
                reader["Id"].ToString(),
                Convert.ToDateTime(reader["Date"]),
                reader["ExpressCompany"].ToString(),
                reader["ExpressBill"].ToString(),
                reader["IncludeOrder"].ToString(),
                Convert.ToDecimal(reader["Cost"]),
                reader["Summary"].ToString(),
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
