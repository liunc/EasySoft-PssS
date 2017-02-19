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
    using EasySoft.PssS.DbRepository;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using EasySoft.PssS.Util;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;

    /// <summary>
    /// 采购领域服务类
    /// </summary>
    public class PurchaseService
    {
        #region 变量

        private IPurchaseRepository purchaseRepository = null;
        private CostService costService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseService()
        {
            this.purchaseRepository = new PurchaseRepository();
            this.costService = new CostService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 新增采购记录
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>
        /// <param name="unit">单位</param>
        /// <param name="supplier">供应方</param>
        /// <param name="remark">备注</param>
        /// <param name="costs">成本</param>
        /// <param name="creator">创建人</param>
        public void IntoDepot(DateTime date, PurchaseCategory category, string item, decimal quantity, string unit, string supplier, string remark, Dictionary<string, decimal> costs, string creator)
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

                    Purchase entity = new Purchase();
                    entity.IntoDepot(date, category, item, quantity, unit, supplier, remark, costs, creator);

                    this.purchaseRepository.Insert(trans, entity);
                    foreach (Cost cost in entity.Costs)
                    {
                        this.costService.Insert(trans, cost);
                    }

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
        /// 修改采购记录
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>
        /// <param name="unit">单位</param>
        /// <param name="supplier">供应方</param>
        /// <param name="remark">备注</param>
        /// <param name="costs">成本</param>
        /// <param name="creator">创建人</param>
        public void Update(string id, DateTime date, decimal quantity, string supplier, string remark, Dictionary<string, decimal> costs, string mender)
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

                    Purchase oldEntity = this.Select(trans, id);
                    if (oldEntity.Status != PurchaseStatus.None)
                    {
                        throw new EasySoftException(BusinessResource.Purchase_NotAllowEdit);
                    }
                    List<Cost> oldCosts = this.costService.SearchByRecordId(trans, id);
                    oldEntity.Costs = new List<Cost>();
                    decimal costTotal = 0;
                    foreach (Cost cost in oldCosts)
                    {
                        decimal money = costs[cost.Id];
                        if (money != cost.Money)
                        {
                            this.costService.Update(trans, cost.Id, money);
                        }
                        costTotal += money;
                    }
                    oldEntity.Update(date, quantity, supplier, remark, costTotal, mender);
                    this.Update(trans, oldEntity);

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
        /// 删除采购记录
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

                    Purchase oldEntity = this.Select(trans, id);
                    if (oldEntity.Status != PurchaseStatus.None)
                    {
                        throw new EasySoftException(BusinessResource.Purchase_NotAllowDelete);
                    }
                    this.costService.DeleteByRecordId(trans, id);
                    this.purchaseRepository.Delete(trans, id);

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
            return this.purchaseRepository.Search(category, item, pageIndex, pageSize, ref totalCount);
        }

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        public Purchase Selete(string id)
        {
            return this.Select(null, id);
        }

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 根据Id查找一条数据
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回采购记录</returns>
        internal Purchase Select(DbTransaction trans, string id)
        {
            Purchase entity = this.purchaseRepository.Select(trans, id);
            if (entity == null)
            {
                throw new EasySoftException(BusinessResource.Purchase_NoFoundById);
            }
            return entity;
        }

        /// <summary>
        /// 更新采购信息
        /// </summary>
        /// <param name="trans">数据库事务</param>
        /// <param name="entity">数据实体对象</param>
        internal void Update(DbTransaction trans, Purchase entity)
        {
            this.purchaseRepository.Update(trans, entity);
        }

        #endregion
    }
}
