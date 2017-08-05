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
    /// 销售仓储实现类
    /// </summary>
    public class SaleRepository : BaseRepository<Sale, string>, ISaleRepository
    {
        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override Sale SetEntity(DbDataReader reader)
        {
            return new Sale
            (
                reader["Id"].ToString(),
                reader["DeliveryId"].ToString(),
                reader["Item"].ToString(),
                Convert.ToDecimal(reader["Quantity"]),
                reader["Unit"].ToString(),
                Convert.ToDecimal(reader["Inventory"]),
                Convert.ToDecimal(reader["ProfitLoss"]),
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
