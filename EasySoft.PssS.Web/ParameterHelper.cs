// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.CostItem;
    using EasySoft.PssS.Web.Resources;
    using EasySoft.PssS.Web.Models.Delivery;
    using EasySoft.PssS.Web.Models.PurchaseItem;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Web;
    using System.Web.Caching;

    /// <summary>
    /// 参数工具类
    /// </summary>
    public class ParameterHelper
    {
        #region 变量

        private static ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
        private static PurchaseItemService purchaseItemService = new PurchaseItemService();
        private static ExpressCompanyService expressCompanyService = new ExpressCompanyService();
        private static CostItemService costItemService = new CostItemService();
        private static CustomerGroupService customerGroupService = new CustomerGroupService();

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项分类
        /// </summary>
        /// <returns>返回采购项分类数据</returns>
        public static Dictionary<string, string> GetPurchaseItemCatetory()
        {
            string cacheName = "PurchaseItemCatetory";

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                lockSlim.EnterWriteLock();

                models = new Dictionary<string, string>();
                models.Add(PurchaseItemCategory.Product, WebResource.PurchaseItemCategory_Product);
                models.Add(PurchaseItemCategory.Pack, WebResource.PurchaseItemCategory_Pack);
                HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                lockSlim.ExitWriteLock();
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取益损分类
        /// </summary>
        /// <returns>返回采购项分类数据</returns>
        public static Dictionary<string, string> GetProfitLossCatetory()
        {
            string cacheName = "ProfitLossCatetory";

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                lockSlim.EnterWriteLock();

                models = new Dictionary<string, string>();
                models.Add(ProfitLossCategory.Profit, WebResource.Field_Profit);
                models.Add(ProfitLossCategory.Loss, WebResource.Field_Loss);
                HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                lockSlim.ExitWriteLock();
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取益损目标分类
        /// </summary>
        /// <returns>返回采购项分类数据</returns>
        public static Dictionary<string, string> GetProfitLossTargetType()
        {
            string cacheName = "ProfitLossTargetType";

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                lockSlim.EnterWriteLock();

                models = new Dictionary<string, string>();
                models.Add(ProfitLossTargetType.Purchase, WebResource.Field_Purchase);
                models.Add(ProfitLossTargetType.Sale, WebResource.Field_Sale);
                HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                lockSlim.ExitWriteLock();
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项数据</returns>
        public static Dictionary<string, PurchaseItemCacheModel> GetPurchaseItem(string category, bool onlyValid = true)
        {
            string cacheName = string.Format("PurchaseItem_{0}{1}", category, onlyValid ? string.Empty : "_All");

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, PurchaseItemCacheModel> models = (Dictionary<string, PurchaseItemCacheModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                models = new Dictionary<string, PurchaseItemCacheModel>();
                List<PurchaseItem> purchaseItems = purchaseItemService.Search(category, onlyValid ? Constant.COMMON_Y : string.Empty);
                if (purchaseItems != null)
                {
                    lockSlim.EnterWriteLock();

                    foreach (PurchaseItem item in purchaseItems)
                    {
                        models.Add(item.Code, new PurchaseItemCacheModel(item));
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                    lockSlim.ExitWriteLock();
                }
            }

            lockSlim.ExitUpgradeableReadLock();
            return models;
        }

        /// <summary>
        /// 获取快递公司
        /// </summary>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回快递公司数据</returns>
        public static Dictionary<string, string> GetExpressCompany(bool onlyValid = true)
        {
            string cacheName = string.Format("ExpressCompany{0}", onlyValid ? string.Empty : "_All");

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                models = new Dictionary<string, string>();

                List<ExpressCompany> expressCompanies = expressCompanyService.Search(onlyValid ? Constant.COMMON_Y : string.Empty);
                if (expressCompanies != null)
                {
                    lockSlim.EnterWriteLock();

                    foreach (ExpressCompany item in expressCompanies)
                    {
                        models.Add(item.Code, item.Name);
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                    lockSlim.ExitWriteLock();
                }
            }
            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取成本项
        /// </summary>
        /// <param name="category">成本分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项数据</returns>
        public static Dictionary<string, CostItemCacheModel> GetCostItem(string category, bool onlyValid = true)
        {
            string cacheName = string.Format("CostItem_{0}{1}", category.ToString(), onlyValid ? string.Empty : "_All");

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, CostItemCacheModel> models = (Dictionary<string, CostItemCacheModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                models = new Dictionary<string, CostItemCacheModel>(); ;
                List<CostItem> costItems = costItemService.Search(category, onlyValid ? Constant.COMMON_Y : string.Empty);
                if (costItems != null)
                {
                    lockSlim.EnterWriteLock();

                    foreach (CostItem item in costItems)
                    {
                        models.Add(item.Code, new CostItemCacheModel(item));
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                    lockSlim.ExitWriteLock();
                }
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取客户分组
        /// </summary>
        /// <returns>返回客户分组数据</returns>
        public static Dictionary<string, string> GetCustomerGroup()
        {
            string cacheName = "CustomerGroup";

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                models = new Dictionary<string, string>();
                List<CustomerGroup> customerGroups = customerGroupService.All();
                if (customerGroups != null)
                {
                    lockSlim.EnterWriteLock();

                    foreach (CustomerGroup item in customerGroups)
                    {
                        models.Add(item.Id, item.Name);
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                    lockSlim.ExitWriteLock();
                }
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <returns>返回采购项分类数据</returns>
        public static Dictionary<string, string> GetSaleOrderStatus()
        {
            string cacheName = "SaleOrderStatus";

            lockSlim.EnterUpgradeableReadLock();

            Dictionary<string, string> models = (Dictionary<string, string>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                lockSlim.EnterWriteLock();

                models = new Dictionary<string, string>();
                models.Add(SaleOrderStatus.Ordered, WebResource.Field_Ordered);
                models.Add(SaleOrderStatus.Sent, WebResource.Field_Sent);
                models.Add(SaleOrderStatus.Received, WebResource.Field_Received);
                models.Add(SaleOrderStatus.Finished, WebResource.Field_Finished);

                HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);

                lockSlim.ExitWriteLock();
            }

            lockSlim.ExitUpgradeableReadLock();

            return models;
        }

        #endregion
    }
}