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
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 采购项仓储实现类
    /// </summary>
    public class PurchaseRepository : IPurchaseRepository
    {
        #region 常量

        private static readonly string SELECT_SQLSTRING = @"SELECT [Id], [Date], [Category], [Item], [Quantity], [Unit], [Supplier], [Allowance], [Cost], [ProfitLoss], [Remark], [Status], [Creator], [CreateTime], [Mender], [ModifyTime] FROM [dbo].[Purchase] {0}";
        
        #endregion

        #region 方法

        /// <summary>
        /// 新增采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        public void Insert(DbTransaction trans, Purchase entity)
        {
            string cmdText = @"INSERT INTO [dbo].[Purchase]([Id], [Date], [Category], [Item], [Quantity], [Unit], [Supplier], [Allowance], [Cost], [ProfitLoss], [Remark], [Status], [Creator], [CreateTime], [Mender], [ModifyTime])
                             VALUES(@Id, @Date, @Category, @Item, @Quantity, @Unit, @Supplier, @Allowance, @Cost, @ProfitLoss, @Remark, @Status, @Creator, @CreateTime, @Mender, @ModifyTime)";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter("Id", DbType.String, 32, entity.Id),
                    DbHelper.SetParameter("Date", DbType.DateTime, entity.Date),
                    DbHelper.SetParameter("Category", DbType.String, 10, entity.Category.ToString()),
                    DbHelper.SetParameter("Item", DbType.String, 20, entity.Item),
                    DbHelper.SetParameter("Quantity", DbType.Decimal, 18, entity.Quantity),
                    DbHelper.SetParameter("Unit", DbType.String, 5, entity.Unit),
                    DbHelper.SetParameter("Supplier", DbType.String, 50, entity.Supplier),
                    DbHelper.SetParameter("Allowance", DbType.Decimal, 18, entity.Allowance),
                    DbHelper.SetParameter("Cost", DbType.Decimal, 18, entity.Cost),
                    DbHelper.SetParameter("ProfitLoss", DbType.Decimal, 18, entity.ProfitLoss),
                    DbHelper.SetParameter("Remark", DbType.String, 120, entity.Remark),
                    DbHelper.SetParameter("Status", DbType.Int16, entity.Status),
                    DbHelper.SetParameter("Creator", DbType.String, 20, entity.Creator.UserId),
                    DbHelper.SetParameter("CreateTime", DbType.DateTime, entity.Creator.Time),
                    DbHelper.SetParameter("Mender", DbType.String, 20, entity.Mender.UserId),
                    DbHelper.SetParameter("ModifyTime", DbType.DateTime, entity.Mender.Time)};

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 更新采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        public void Update(DbTransaction trans, Purchase entity)
        {
            string cmdText = @"UPDATE [dbo].[Purchase]
                                SET [Date] = @Date
                                   ,[Quantity] = @Quantity
                                   ,[Supplier] = @Supplier
                                   ,[Allowance] = @Allowance
                                   ,[Cost] = @Cost
                                   ,[ProfitLoss] = @ProfitLoss
                                   ,[Remark] = @Remark
                                   ,[Status] = @Status
                                   ,[Mender] = @Mender
                                   ,[ModifyTime] = @ModifyTime
                                WHERE [Id] = @Id";

            DbParameter[] paras = new DbParameter[] {
                    DbHelper.SetParameter("Id", DbType.String, 32, entity.Id),
                    DbHelper.SetParameter("Date", DbType.DateTime, entity.Date),
                    DbHelper.SetParameter("Quantity", DbType.Decimal, 18, entity.Quantity),
                    DbHelper.SetParameter("Supplier", DbType.String, 50, entity.Supplier),
                    DbHelper.SetParameter("Allowance", DbType.Decimal, 18, entity.Allowance),
                    DbHelper.SetParameter("Cost", DbType.Decimal, 18, entity.Cost),
                    DbHelper.SetParameter("ProfitLoss", DbType.Decimal, 18, entity.ProfitLoss),
                    DbHelper.SetParameter("Remark", DbType.String, 120, entity.Remark),
                    DbHelper.SetParameter("Status", DbType.Int16, entity.Status),
                    DbHelper.SetParameter("Mender", DbType.String, 20, entity.Mender.UserId),
                    DbHelper.SetParameter("ModifyTime", DbType.DateTime, entity.Mender.Time)};

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, paras);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, paras);
        }

        /// <summary>
        /// 删除采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        public void Delete(DbTransaction trans, string id)
        {
            string cmdText = @"DELETE FROM [dbo].[Purchase] WHERE [Id] = @Id";

            DbParameter paras = DbHelper.SetParameter("Id", DbType.String, 32, id);

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
            Purchase entity = null;
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
            string cmdText = string.Format(SELECT_SQLSTRING, whereCmdText);
            string totalCmdText = string.Format("SELECT COUNT(1) FROM [dbo].[Purchase] {0}", whereCmdText);
            totalCount = Convert.ToInt32(DbHelper.ExecuteScalar(totalCmdText, paras.ToArray()));
            DbDataReader reader = DbHelper.Paging(cmdText, pageSize, totalCount, pageIndex, "[CreateTime] DESC", paras.ToArray());

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
        private Purchase SetEntity(DbDataReader reader)
        {
            return new Purchase
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
                Status = (PurchaseStatus)Enum.Parse(typeof(PurchaseStatus), reader["Status"].ToString()),
                Creator = new Operator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"])),
                Mender = new Operator(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]))
            };
        }

        #endregion

    }
}
