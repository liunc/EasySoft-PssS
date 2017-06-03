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
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 客户地址领域服务类
    /// </summary>
    public class CustomerAddressService
    {
        #region 变量

        private ICustomerAddressRepository customerAddressRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressService()
        {
            this.customerAddressRepository = new CustomerAddressRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询客户地址信息
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <returns>返回客户地址数据集合</returns>
        public List<CustomerAddress> SearchByCustomerId(string customerId)
        {
            return this.customerAddressRepository.SearchByCustomerId(customerId);
        }


        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <param name="linkname">联系人</param>
        /// <param name="mobile">手机号</param>
        /// <param name="address">地址</param>
        /// <param name="isDefault">是否默认</param>
        /// <param name="creator">创建人</param>
        public void Add(string customerId, string linkname, string mobile, string address, string isDefault, string creator)
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
                    CustomerAddress entity = new CustomerAddress();
                    entity.Add(customerId,address, mobile, linkname, isDefault, creator);
                    if (entity.IsDefaultAddress())
                    {
                        CustomerAddress defaultEntity = this.customerAddressRepository.GetDefaultAddres(trans, customerId);
                        if(defaultEntity != null)
                        {
                            defaultEntity.CancelDefault(creator);
                            this.customerAddressRepository.Update(trans, defaultEntity);
                        }
                    }
                    this.customerAddressRepository.Insert(trans, entity);

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
        /// 修改客户地址
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">分组名称</param>
        /// <param name="remark">备注</param>
        /// <param name="mender">修改人</param>
        public void Update(string id, string linkname, string mobile, string address, string mender)
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
                    CustomerAddress entity = this.Select(trans, id);
                    entity.Update(address, mobile, linkname, mender);
                    this.customerAddressRepository.Update(trans, entity);

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
        /// 设置默认客户地址
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="mender">修改人</param>
        public void SetDefault(string id, string mender)
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
                    CustomerAddress entity = this.Select(trans, id);
                    if(!entity.IsDefaultAddress())
                    {
                        CustomerAddress defaultEntity = this.customerAddressRepository.GetDefaultAddres(trans, entity.CustomerId);
                        if(defaultEntity != null)
                        {
                            defaultEntity.CancelDefault(mender);
                            this.customerAddressRepository.Update(trans, defaultEntity);
                        }
                        entity.SetDefault(mender);
                        this.customerAddressRepository.Update(trans, entity);
                        
                        trans.Commit();
                    }
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
        /// 删除客户地址
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

                    CustomerAddress entity = this.Select(trans, id);
                    if(!entity.CanDelete())
                    {
                        throw new EasySoftException(BusinessResource.Customer_NotAllowDeleteDefaultAddress);
                    }
                    this.customerAddressRepository.Delete(trans, id);
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
        /*
        /// <summary>
        /// 查询客户分组表信息，用于列表分页显示
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回客户分组数据</returns>
        public List<CustomerGroup> Search(int pageIndex, int pageSize, ref int totalCount)
        {
            return this.customerGroupRepository.Search(pageIndex, pageSize, ref totalCount);
        }
        */

        /// <summary>
        /// 根据Id查找一条客户分组数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回客户分组对象</returns>
        public CustomerAddress Select(string id)
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
        internal CustomerAddress Select(DbTransaction trans, string id)
        {
            CustomerAddress entity = this.customerAddressRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Customer_NoFoundAddress);
            }
            return entity;
        }

        /// <summary>
        /// 新增客户地址
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">客户地址实体对象</param>
        internal void Insert(DbTransaction trans, CustomerAddress entity)
        {
            this.customerAddressRepository.Insert(trans, entity);
        }

        /// <summary>
        /// 根据客户ID删除数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="customerId">客户Id</param>
        internal void DeleteByCustomerId(DbTransaction trans, string customerId)
        {
            this.customerAddressRepository.DeleteByCustomerId(trans, customerId);
        }

        #endregion
    }
}
