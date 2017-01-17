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
    using EasySoft.PssS.Repository;
    using Entity;
    using System.Data.Common;
    using System;
    using System.Collections.Generic;
    using ValueObject;

    /// <summary>
    /// 采购领域服务类
    /// </summary>
    public class PurchaseService
    {
        #region 变量

        private IPurchaseRepository purchaseRepository = null;
        private ICostRepository costRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseService()
        {
            this.purchaseRepository = new PurchaseRepository();
            this.costRepository = new CostRepository();
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
        public void Add(DateTime date, string category, string item, decimal quantity, string unit, string supplier, string remark, Dictionary<string, decimal> costs, string creator)
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
                    entity.Add(date, category, item, quantity, unit, supplier, remark, costs, creator);

                    this.purchaseRepository.Insert(trans, entity);
                    foreach(Cost cost in entity.Costs)
                    {
                        this.costRepository.Insert(trans, cost);
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

        #endregion
    }
}
