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
    /// 采购项领域服务类
    /// </summary>
    public class PurchaseItemService
    {
        #region 变量

        private IPurchaseItemRepository purchaseItemRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseItemService()
        {
            this.purchaseItemRepository = new PurchaseItemRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增采购项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="category">分类</param>
        /// <param name="inUnit">入库单位</param>
        /// <param name="outUnit">出库单位</param>
        /// <param name="inOutRate">入库出库单位换算比例</param>
        /// <param name="price">销售单价</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string code, string category, string inUnit, string outUnit, decimal inOutRate, decimal price, string creator)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    if (this.purchaseItemRepository.HasSameCode(trans, string.Empty, code))
                    {
                        throw new EasySoftException(BusinessResource.PurchaseItem_ExistsSameCode);
                    }

                    PurchaseItem entity = new PurchaseItem();
                    entity.Create(name, code, category, inUnit, outUnit, inOutRate, price, creator);
                    this.purchaseItemRepository.Insert(trans, entity);

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
        /// 新增采购项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="category">分类</param>
        /// <param name="inUnit">入库单位</param>
        /// <param name="outUnit">出库单位</param>
        /// <param name="inOutRate">入库出库单位换算比例</param>
        /// <param name="price">销售单价</param>
        /// <param name="mender">创建人</param>
        public void Update(string id, string name, string inUnit, string outUnit, decimal inOutRate, decimal price, string isValid, string mender)
        {
            using (DbConnection conn = DbHelper.CreateConnection())
            {
                DbTransaction trans = null;
                try
                {
                    conn.Open();
                    trans = conn.BeginTransaction();

                    PurchaseItem entity = this.Select(trans, id);
                    entity.Update(name, inUnit, outUnit, inOutRate, price, isValid, mender);
                    this.purchaseItemRepository.Update(trans, entity);

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
        /// 删除采购项
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

                    PurchaseItem oldEntity = this.Select(trans, id);
                    // 检查Code是否已使用
                    this.purchaseItemRepository.Delete(trans, id);

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
        /// 查询采购项表信息，用于列表分页显示
        /// </summary>
        /// <param name="category">采购项分类</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">数据源中每页要显示的行的数目</param>
        /// <param name="totalCount">数据源总记录数</param>
        /// <returns>返回数据表</returns>
        public List<PurchaseItem> Search(string category, int pageIndex, int pageSize, ref int totalCount)
        {
            return this.purchaseItemRepository.Search(category, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        public PurchaseItem Select(string id)
        {
            return this.Select(null, id);
        }

        /// <summary>
        /// 查询采购项表信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回采购项数据集合</returns>
        public List<PurchaseItem> Search(string category, string isValid)
        {
            return this.purchaseItemRepository.Search(category, isValid);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        internal PurchaseItem Select(DbTransaction trans, string id)
        {
            PurchaseItem entity = this.purchaseItemRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Common_NoFoundById);
            }
            return entity;
        }

        #endregion
    }
}
