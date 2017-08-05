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
    /// 客户分组领域服务类
    /// </summary>
    public class CustomerGroupService
    {
        #region 变量

        private ICustomerGroupRepository customerGroupRepository = null;
        private ICustomerRepository customerRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupService()
        {
            this.customerGroupRepository = new CustomerGroupRepository();
            this.customerRepository = new CustomerRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增客户分组
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string remark, string creator)
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
                    if (this.customerGroupRepository.HasSameName(trans, string.Empty, name))
                    {
                        throw new EasySoftException(BusinessResource.Customer_ExistsSameGroupName);
                    }
                    CustomerGroup entity = new CustomerGroup();
                    entity.Create(name, remark, creator);
                    this.customerGroupRepository.Insert(trans, entity);

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
        /// 修改客户分组
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">分组名称</param>
        /// <param name="remark">备注</param>
        /// <param name="mender">修改人</param>
        public void Update(string id, string name, string remark, string mender)
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
                    if (this.customerGroupRepository.HasSameName(trans, id, name))
                    {
                        throw new EasySoftException(BusinessResource.Customer_ExistsSameGroupName);
                    }

                    CustomerGroup entity = this.Select(trans, id);
                    entity.Update(name, remark, mender);
                    this.customerGroupRepository.Update(trans, entity);

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

                    CustomerGroup entity = this.Select(trans, id);
                    if (!entity.CanDelete())
                    {
                        throw new EasySoftException(BusinessResource.Customer_NotAllowDeleteCustomerGroup);
                    }
                    // 要将删除的客户分组中的客户移到默认分组中
                    string defaultGroupId = this.customerGroupRepository.GetDefaultGroupId(trans);
                    if (string.IsNullOrWhiteSpace(defaultGroupId))
                    {
                        throw new EasySoftException(BusinessResource.Customer_NoFoundDefaultGroupId);
                    }
                    this.customerRepository.BatUpdateGroupId(trans, id, defaultGroupId);
                    this.customerGroupRepository.Delete(trans, id);
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

        /// <summary>
        /// 根据Id查找一条客户分组数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回客户分组对象</returns>
        public CustomerGroup Select(string id)
        {
            return this.Select(null, id);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <returns>返回实体对象</returns>
        public List<CustomerGroup> All()
        {
            return this.customerGroupRepository.All(null);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回客户分组对象</returns>
        internal CustomerGroup Select(DbTransaction trans, string id)
        {
            CustomerGroup entity = this.customerGroupRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Customer_NoFoundGroup);
            }
            return entity;
        }

        /// <summary>
        /// 判断指定的分组是否存在
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="id">Id</param>
        /// <returns>返回布尔值</returns>
        internal bool Exists(DbTransaction trans, string id)
        {
            return this.customerGroupRepository.Exists(trans, id);
        }

        /// <summary>
        /// 获取默认分组Id
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <returns>返回默认分组Id</returns>
        internal string GetDefaultGroupId(DbTransaction trans)
        {
            string defaultGroupId = this.customerGroupRepository.GetDefaultGroupId(trans);
            if (string.IsNullOrWhiteSpace(defaultGroupId))
            {
                throw new EasySoftException(BusinessResource.Customer_NoFoundDefaultGroupId);
            }
            return defaultGroupId;
        }

        #endregion
    }
}
