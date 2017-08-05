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
    using Core.Persistence.RepositoryImplement;
    using Core.Util;

    /// <summary>
    /// 益损领域服务类
    /// </summary>
    public class ProfitLossService
    {
        #region 变量

        private IProfitLossRepository profitLossRepository = null;
        private PurchaseService purchaseService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProfitLossService()
        {
            this.profitLossRepository = new ProfitLossRepository();
            this.purchaseService = new PurchaseService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据记录Id获取益损集合
        /// </summary>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回益损集合</returns>
        public List<ProfitLoss> GetList(string recordId)
        {
            return this.profitLossRepository.SearchByRecordId(null, recordId);
        }

        /// <summary>
        /// 新增益损信息
        /// </summary>
        /// <param name="recordId">记录Id</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="category">分类</param>
        /// <param name="quantity">数量</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Add(string recordId, string targetType, string category, decimal quantity, string remark, string creator)
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
                    if (targetType == ProfitLossTargetType.Purchase)
                    {
                        Purchase purchase = this.purchaseService.Select(trans, recordId);
                        purchase.AddProfitLoss(category, quantity);
                        if (purchase.Inventory < 0)
                        {
                            throw new EasySoftException(BusinessResource.Purchase_LowStocks);
                        }
                        this.purchaseService.Update(trans, purchase);
                    }
                    else if (targetType == ProfitLossTargetType.Sale)
                    {
                        // 销售
                        throw new NotSupportedException();
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    ProfitLoss entity = new ProfitLoss();
                    entity.Create(recordId, targetType, category, quantity, remark, creator);
                    this.profitLossRepository.Insert(trans, entity);

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
