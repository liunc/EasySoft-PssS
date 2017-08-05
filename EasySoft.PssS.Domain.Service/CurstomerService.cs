// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域服务类库
// 创 建 人：刘年超
// 创建时间：2017-05-13
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Service
{
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.Core.Util;
    using EasySoft.PssS.Application.DataTransfer.Customer;
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 客户领域服务类
    /// </summary>
    public class CustomerService
    {
        #region 变量

        private ICustomerGroupRepository customerGroupRepository = null;
        private ICustomerAddressRepository customerAddressRepository = null;
        private ICustomerRepository customerRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerService()
        {
            this.customerGroupRepository = new CustomerGroupRepository();
            this.customerAddressRepository = new CustomerAddressRepository();
            this.customerRepository = new CustomerRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="nickname">呢称</param>
        /// <param name="mobile">手机号</param>
        /// <param name="address">地址</param>
        /// <param name="weChatId">微信Id</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="creator">创建人</param>
        /// <returns>返回客户地址Id</returns>
        public string Add(string name, string nickname, string mobile, string address, string weChatId, string groupId, string creator)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }
                    if (!this.customerGroupRepository.Exists(trans, groupId))
                    {
                        throw new EasySoftException(BusinessResource.Customer_NoFoundGroup);
                    }

                    Customer entity = new Customer();
                    entity.Create(name, nickname, mobile, address, weChatId, groupId, creator);
                    this.customerRepository.Insert(trans, entity);

                    if (entity.Addresses != null && entity.Addresses.Count > 0)
                    {
                        this.customerAddressRepository.Insert(trans, entity.Addresses[0]);
                    }
                    trans.Commit();

                    return entity.Addresses[0].Id;
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 修改客户分组
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">姓名</param>
        /// <param name="nickname">呢称</param>
        /// <param name="mobile">手机号</param>
        /// <param name="weChatId">微信Id</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="mender">修改人</param>
        public void Update(string id, string name, string nickname, string mobile, string weChatId, string groupId, string mender)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }
                    Customer entity = this.Select(trans, id);
                    if (entity.GroupId != groupId)
                    {
                        if (!this.customerGroupRepository.Exists(trans, groupId))
                        {
                            throw new EasySoftException(BusinessResource.Customer_NoFoundGroup);
                        }
                    }
                    entity.Update(name, nickname, mobile, weChatId, groupId, mender);
                    this.customerRepository.Update(trans, entity);

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 删除客户分组
        /// </summary>
        /// <param name="id">Id</param>
        public void Delete(string id)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();
                    if (trans == null)
                    {
                        throw new ArgumentNullException("DbTransaction");
                    }

                    if (!this.customerRepository.Exists(trans, id))
                    {
                        throw new EasySoftException(BusinessResource.Customer_NoFound);
                    }

                    // 检查是否有订单，有订单不让删除

                    this.customerAddressRepository.DeleteByCustomerId(trans, id);
                    this.customerRepository.Delete(trans, id);

                    trans.Commit();
                }
                catch
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询客户表信息，用于列表分页显示
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户数据集合</returns>
        public List<Customer> Search(string name, string groupId, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.customerRepository.Search(name, groupId, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 提供下单选择客户的数据
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组Id</param>
        /// <returns>返回数据</returns>
        public List<CustomerForOrderDTO> GetCustomerListForOrder(string name, string groupId)
        {
            return this.customerRepository.GetCustomerListForOrder(name, groupId);
        }

        /// <summary>
        /// 根据Id查找一条客户数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回客户分组对象</returns>
        public Customer Select(string id)
        {
            return this.Select(null, id);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回客户分组对象</returns>
        internal Customer Select(DbTransaction trans, string id)
        {
            Customer entity = this.customerRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Customer_NoFound);
            }
            return entity;
        }

        /// <summary>
        /// 批量修改客户的分组Id
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="oldGroupId">旧客户分组Id</param>
        /// <param name="newGroupId">新客户分组Id</param>
        internal void BatUpdateGroupId(DbTransaction trans, string oldGroupId, string newGroupId)
        {
            this.customerRepository.BatUpdateGroupId(trans, oldGroupId, newGroupId);
        }

        #endregion
    }
}
