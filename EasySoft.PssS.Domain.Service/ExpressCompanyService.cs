// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域服务类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Service
{
    using Core.Persistence.RepositoryImplement;
    using Core.Util;
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 快递公司领域服务类
    /// </summary>
    public class ExpressCompanyService
    {
        #region 变量

        private IExpressCompanyRepository expressCompanyRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ExpressCompanyService()
        {
            this.expressCompanyRepository = new ExpressCompanyRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增快递公司
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string code, string creator)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    if (this.expressCompanyRepository.HasSameCode(trans, string.Empty, code))
                    {
                        throw new EasySoftException(BusinessResource.ExpressCompany_ExistsSameCode);
                    }

                    ExpressCompany entity = new ExpressCompany();
                    entity.Create(name, code, creator);
                    this.expressCompanyRepository.Insert(trans, entity);

                    trans.Commit();
                }
                catch (EasySoftException ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 修改快递公司
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="mender">创建人</param>
        public void Update(string id, string name, string isValid, string mender)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    ExpressCompany entity = this.Select(trans, id);
                    entity.Update(name, isValid, mender);
                    this.expressCompanyRepository.Update(trans, entity);

                    trans.Commit();
                }
                catch (EasySoftException ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 删除快递公司
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

                    ExpressCompany oldEntity = this.Select(trans, id);
                    // 检查Code是否已使用
                    this.expressCompanyRepository.Delete(trans, id);

                    trans.Commit();
                }
                catch (EasySoftException ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 查询快递公司表信息，用于列表分页显示
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<ExpressCompany> Search(string name, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.expressCompanyRepository.Search(name, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        public ExpressCompany Select(string id)
        {
            return this.Select(null, id);
        }

        /// <summary>
        /// 查询快递公司表信息
        /// </summary>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回快递公司数据集合</returns>
        public List<ExpressCompany> Search(string isValid)
        {
            return this.expressCompanyRepository.Search(isValid);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        internal ExpressCompany Select(DbTransaction trans, string id)
        {
            ExpressCompany entity = this.expressCompanyRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Common_NoFoundById);
            }
            return entity;
        }

        #endregion
    }
}
