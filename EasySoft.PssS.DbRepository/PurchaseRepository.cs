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
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    /// <summary>
    /// 采购项仓储实现类
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        #region 方法

        /// <summary>
        /// 获取采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        public void Insert(DbTransaction trans, Purchase entity)
        {
            string cmdText = @"INSERT INTO [EasysoftPssS_Dev].[dbo].[Purchase]
                                   ([Id]
                                   ,[Date]
                                   ,[Category]
                                   ,[Item]
                                   ,[Quantity]
                                   ,[Unit]
                                   ,[Supplier]
                                   ,[Allowance]
                                   ,[Remark]
                                   ,[Creator]
                                   ,[CreateTime]
                                   ,[Mender]
                                   ,[ModifyTime])
                             VALUES
                                   (@Id, 
                                   ,@Date, 
                                   ,@Category, 
                                   ,@Item, 
                                   ,@Quantity, 
                                   ,@Unit, 
                                   ,@Supplier, 
                                   ,@Allowance, 
                                   ,@Remark,
                                   ,@Creator, 
                                   ,@CreateTime, 
                                   ,@Mender, 
                                   ,@ModifyTime)";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter(new SqlParameter("@Id", SqlDbType.Char, 32), entity.Id),
                    DbHelper.SetParameter(new SqlParameter("@Date", SqlDbType.Date), entity.Date),
                    DbHelper.SetParameter(new SqlParameter("@Category", SqlDbType.VarChar, 10), entity.Category.ToString()),
                    DbHelper.SetParameter(new SqlParameter("@Item", SqlDbType.VarChar, 20), entity.Item),
                    DbHelper.SetParameter(new SqlParameter("@Quantity", SqlDbType.Decimal, 18), entity.Quantity),
                    DbHelper.SetParameter(new SqlParameter("@Unit", SqlDbType.NVarChar, 5), entity.Unit),
                    DbHelper.SetParameter(new SqlParameter("@Supplier", SqlDbType.NVarChar, 50), entity.Supplier, string.Empty),
                    DbHelper.SetParameter(new SqlParameter("@Allowance", SqlDbType.Decimal, 18), entity.Allowance),
                    DbHelper.SetParameter(new SqlParameter("@Remark", SqlDbType.NVarChar, 120), entity.Remark, string.Empty),
                    DbHelper.SetParameter(new SqlParameter("@Creator", SqlDbType.NVarChar, 20), entity.Creator.UserId),
                    DbHelper.SetParameter(new SqlParameter("@CreateTime", SqlDbType.DateTime), entity.Creator.Time),
                    DbHelper.SetParameter(new SqlParameter("@Mender", SqlDbType.NVarChar, 20), entity.Mender.UserId),
                    DbHelper.SetParameter(new SqlParameter("@ModifyTime", SqlDbType.DateTime), entity.Mender.Time)};

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        #endregion


    }
}
