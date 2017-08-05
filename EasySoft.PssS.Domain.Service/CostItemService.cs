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
    using EasySoft.Core.Persistence.RepositoryImplement;
    using EasySoft.Core.Util;
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 成本项领域服务类
    /// </summary>
    public class CostItemService
    {
        #region 变量

        private ICostItemRepository costItemRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CostItemService()
        {
            this.costItemRepository = new CostItemRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增成本项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="category">分类</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string code, string category, short orderNumber, string remark, string creator)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    if (this.costItemRepository.HasSameCode(trans, string.Empty, code))
                    {
                        throw new EasySoftException(BusinessResource.CostItem_ExistsSameCode);
                    }

                    CostItem entity = new CostItem();
                    entity.Create(name, code, category, orderNumber, remark, creator);
                    this.costItemRepository.Insert(trans, entity);

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
        /// 新增成本项
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="mender">创建人</param>
        public void Update(string id, string name, string isValid, short orderNumber, string remark, string mender)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    CostItem entity = this.Select(trans, id);
                    entity.Update(name, isValid, orderNumber, remark, mender);
                    this.costItemRepository.Update(trans, entity);

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
        /// 删除成本项
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

                    CostItem oldEntity = this.Select(trans, id);
                    // 检查Code是否已使用
                    this.costItemRepository.Delete(trans, id);

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
        /// 查询成本项表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">成本项分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<CostItem> Search(string category, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.costItemRepository.Search(category, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回成本记录</returns>
        public CostItem Select(string id)
        {
            return this.Select(null, id);
        }

        /// <summary>
        /// 查询成本项表信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回成本项数据集合</returns>
        public List<CostItem> Search(string category, string isValid)
        {
            return this.costItemRepository.Search(category, isValid);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回成本记录</returns>
        internal CostItem Select(DbTransaction trans, string id)
        {
            CostItem entity = this.costItemRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Common_NoFoundById);
            }
            return entity;
        }

        #endregion
    }
}
