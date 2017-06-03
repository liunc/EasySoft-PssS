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
    using Core.Util;
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置实体对象值
        /// </summary>
        /// <param name="reader">DbDataReader对象</param>
        /// <returns>返回实体对象</returns>
        protected override CustomerAddress SetEntity(DbDataReader reader)
        {
            CustomerAddress entity = new CustomerAddress
            {
                Id = reader["Id"].ToString(),
                Address = reader["Address"].ToString(),
                CustomerId = reader["CustomerId"].ToString(),
                Linkman = reader["Linkman"].ToString(),
                IsDefault = reader["IsDefault"].ToString(),
                Mobile = reader["Mobile"].ToString()
            };
            entity.SetCreator(reader["Creator"].ToString(), Convert.ToDateTime(reader["CreateTime"]));
            entity.SetMender(reader["Mender"].ToString(), Convert.ToDateTime(reader["ModifyTime"]));
            return entity;
        }

        #endregion

    }
}
