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
    using EasySoft.PssS.Repository;
    using EasySoft.PssS.XmlRepository;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// 配置参数领域服务类
    /// </summary>
    public class ParameterService
    {
        #region 变量

        private IPurchaseItemRepository purchaseItemRepository;
        private ICostItemRepository costItemRepository;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterService()
        {
            this.purchaseItemRepository = new PurchaseItemRepository();
            this.costItemRepository = new CostItemRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="valid">是否有效</param>
        /// <returns>返回采购项信息</returns>
        public List<PurchaseItem> GetPurchaseItem(string category, bool valid)
        {
            return this.purchaseItemRepository.GetPurchaseItem(category, valid);
        }

        /// <summary>
        /// 获取成本项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="valid">是否有效</param>
        /// <returns>返回成本项信息</returns>
        public List<CostItem> GetCostItem(string category, bool valid)
        {
            return this.costItemRepository.GetCostItem(category, valid);
        }

        #endregion
    }
}
