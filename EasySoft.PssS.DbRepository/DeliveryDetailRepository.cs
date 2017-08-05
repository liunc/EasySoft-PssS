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
    public class DeliveryDetailRepository : BaseRepository<DeliveryDetail, string>, IDeliveryDetailRepository
    {
        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override DeliveryDetail SetEntity(DbDataReader reader)
        {
            return new DeliveryDetail
            (
                reader["Id"].ToString(),
                reader["DeliveryId"].ToString(),
                reader["PurchaseId"].ToString(),
                reader["PurchaseCategory"].ToString(),
                Convert.ToDecimal(reader["DeliveryQuantity"]),
                Convert.ToDecimal(reader["PackQuantity"]),
                reader["PackUnit"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"])
            );
        }

        #endregion

    }
}
