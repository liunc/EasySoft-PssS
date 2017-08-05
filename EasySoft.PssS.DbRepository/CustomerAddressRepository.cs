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
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// 客户分组仓储实现类
    /// </summary>
    public class CustomerAddressRepository : BaseRepository<CustomerAddress, string>, ICustomerAddressRepository
    {
        #region 方法
        
        /// <summary>
        /// 获取默认地址对象
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回默认地址对象</returns>
        public CustomerAddress GetDefaultAddres(DbTransaction trans, string customerId)
        {
            string cmdText = string.Format("{0} WHERE [CustomerId] = @CustomerId AND [IsDefault] = '{1}'", this.Resolver.SelectAllCommandText, Constant.COMMON_Y);
            
            DbParameter parameter = DbHelper.CreateParameter("CustomerId", DbType.String, 32);
            parameter.Value = customerId;

            DbDataReader reader = null;
            if (trans == null)
            {
                reader = DbHelper.ExecuteReader(cmdText, parameter);
            }
            else
            {
                reader = DbHelper.ExecuteReader(trans, cmdText, parameter);
            }
            CustomerAddress entity = default(CustomerAddress);
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
        /// 根据客户ID删除数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="customerId">客户Id</param>
        public void DeleteByCustomerId(DbTransaction trans, string customerId)
        {
            string cmdText = string.Format("DELETE FROM {0} WHERE [CustomerId] = @CustomerId", this.Resolver.TableName);

            DbParameter parameter = DbHelper.CreateParameter("CustomerId", DbType.String, 32);
            parameter.Value = customerId;

            if (trans == null)
            {
                DbHelper.ExecuteNonQuery(cmdText, parameter);
                return;
            }
            DbHelper.ExecuteNonQuery(trans, cmdText, parameter);
        }

        /// <summary>
        /// 查询客户地址信息
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回客户地址数据集合</returns>
        public List<CustomerAddress> SearchByCustomerId(string customerId)
        {
            string cmdText = string.Format("{0} WHERE [CustomerId] = @CustomerId  ORDER BY [IsDefault] DESC, [CreateTime] DESC", this.Resolver.SelectAllCommandText);
            
            DbParameter paras = DbHelper.CreateParameter("CustomerId", DbType.String, 32);
            paras.Value = customerId;
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, paras);

            List<CustomerAddress> entities = new List<CustomerAddress>();
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

        /// <summary>
        /// 查询客户地址信息
        /// </summary>
        /// <param name="linkMan">联系人</param>
        /// <returns>返回客户地址数据集合</returns>
        public List<CustomerAddress> SearchByLinkMan(string linkMan)
        {
            List<string> conditions = new List<string>();
            List<DbParameter> paras = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(linkMan))
            {
                conditions.Add("[Linkman] = @Linkman");
                DbParameter para = DbHelper.CreateParameter("Linkman", DbType.String, Constant.STRING_LENGTH_10);
                para.Value = linkMan;
                paras.Add(para);
            }
            string whereCmdText = string.Empty;
            if (conditions.Count > 0)
            {
                whereCmdText = string.Format("WHERE {0}", string.Join(" AND ", conditions.ToArray()));
            }
            string cmdText = string.Format("{0} {1}  ORDER BY [Linkman] ASC, [IsDefault] DESC", this.Resolver.SelectAllCommandText, whereCmdText);
            DbDataReader reader = DbHelper.ExecuteReader(cmdText, paras.ToArray());

            List<CustomerAddress> entities = new List<CustomerAddress>();
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
        protected override CustomerAddress SetEntity(DbDataReader reader)
        {
            return new CustomerAddress
            (
                reader["Id"].ToString(),
                reader["CustomerId"].ToString(),
                reader["Address"].ToString(),
                reader["Mobile"].ToString(),
                reader["Linkman"].ToString(),
                reader["IsDefault"].ToString(),
                reader["Creator"].ToString(),
                Convert.ToDateTime(reader["CreateTime"]),
                reader["Mender"].ToString(),
                Convert.ToDateTime(reader["ModifyTime"])
            );
        }

        #endregion

    }
}
