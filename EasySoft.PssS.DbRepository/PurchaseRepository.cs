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
            string cmdText = @"INSERT INTO [dbo].[Purchase]
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
                                   (@Id
                                   ,@Date 
                                   ,@Category 
                                   ,@Item 
                                   ,@Quantity 
                                   ,@Unit 
                                   ,@Supplier 
                                   ,@Allowance 
                                   ,@Remark
                                   ,@Creator 
                                   ,@CreateTime 
                                   ,@Mender 
                                   ,@ModifyTime)";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter(new SqlParameter("@Id", SqlDbType.Char, 32), entity.Id),
                    DbHelper.SetParameter(new SqlParameter("@Date", SqlDbType.Date), entity.Date),
                    DbHelper.SetParameter(new SqlParameter("@Category", SqlDbType.VarChar, 10), entity.Category.ToString()),
                    DbHelper.SetParameter(new SqlParameter("@Item", SqlDbType.VarChar, 20), entity.Item),
                    DbHelper.SetParameter(new SqlParameter("@Quantity", SqlDbType.Decimal, 18), entity.Quantity),
                    DbHelper.SetParameter(new SqlParameter("@Unit", SqlDbType.NVarChar, 5), entity.Unit),
                    DbHelper.SetParameter(new SqlParameter("@Supplier", SqlDbType.NVarChar, 50), entity.Supplier),
                    DbHelper.SetParameter(new SqlParameter("@Allowance", SqlDbType.Decimal, 18), entity.Allowance),
                    DbHelper.SetParameter(new SqlParameter("@Remark", SqlDbType.NVarChar, 120), entity.Remark),
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

        /// <summary>
        /// 根据Id获取一条采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回采购实体对象</returns>
        public Purchase Select(DbTransaction trans, string id)
        {
            string cmdText = @"SELECT [Id]
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
                                  ,[ModifyTime]
                              FROM [dbo].[Purchase]";

            DbParameter paras = DbHelper.SetParameter(new SqlParameter("@Id", SqlDbType.Char, 32), id);

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, paras);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, paras);
            }
            Purchase entity = null;
            while (reader.Read())
            {
                entity = new Purchase
                {
                    Id = reader["Id"].ToString(),
                    Date = Convert.ToDateTime(reader["Date"]),
                    Category = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), reader["Category"].ToString()),
                    Item = reader["Item"].ToString(),
                    Quantity = Convert.ToDecimal(reader["Quantity"]),
                    Unit = reader["Unit"].ToString(),
                    Supplier = reader["Supplier"].ToString(),
                    Allowance = Convert.ToDecimal(reader["Allowance"]),
                    Remark = reader["Remark"].ToString(),
                    Creator = new Operator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"])),
                    Mender = new Operator(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]))
                };
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entity;
        }

        /// <summary>
        /// 查询采购表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">产品分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<Purchase> Search(string category, int pageIndex, int pageSize, ref int totalCount)
        {
            string cmdText = @"SELECT [Id]
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
                                  ,[ModifyTime]
                              FROM [dbo].[Purchase]";
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            if (!string.IsNullOrEmpty(category))
            {
                conditions.Add("[Category] = @Category");
                paras.Add(DbHelper.SetParameter(new SqlParameter("@Category", SqlDbType.VarChar, 10), category));
            }

            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            cmdText = string.Format("{0} {1}", cmdText, whereCmdText);
            string totalCmdText = string.Format("SELECT COUNT(1) FROM [dbo].[Purchase] {0}", whereCmdText);
            totalCount = Convert.ToInt32(DbHelper.ExecuteScalar(totalCmdText, paras.ToArray()));
            DbDataReader reader = DbHelper.Paging(cmdText, pageSize, totalCount, pageIndex, "[CreateTime] DESC", paras.ToArray());

            List<Purchase> entitys = new List<Purchase>();
            while (reader.Read())
            {
                entitys.Add(new Purchase
                {
                    Id = reader["Id"].ToString(),
                    Date = Convert.ToDateTime(reader["Date"]),
                    Category = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), reader["Category"].ToString()),
                    Item = reader["Item"].ToString(),
                    Quantity = Convert.ToDecimal(reader["Quantity"]),
                    Unit = reader["Unit"].ToString(),
                    Supplier = reader["Supplier"].ToString(),
                    Allowance = Convert.ToDecimal(reader["Allowance"]),
                    Remark = reader["Remark"].ToString(),
                    Creator = new Operator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"])),
                    Mender = new Operator(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]))
                });
            }
            if (!reader.IsClosed)
            {
                reader.Close();
            }
            return entitys;
        }
        #endregion


    }
}
