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
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Web.Models.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Caching;
    using System.Linq;
    using Domain.ValueObject;

    /// <summary>
    /// 参数工具类
    /// </summary>
    public class ParameterHelper
    {
        #region 变量

        private static readonly object syncLock = new object();
        private static ParameterService parameterService = new ParameterService();
        private static PurchaseConfig purchaseConfig;
        private static DeliveryConfig deliveryConfig;

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public static Dictionary<string, PurchaseItemModel> GetPurchaseItem(string category, bool onlyValid)
        {
            Dictionary<string, PurchaseItemModel> models = new Dictionary<string, PurchaseItemModel>();
            string cacheName = string.Format("PurchaseItem_{0}{1}", category, onlyValid ? string.Empty : "_All");
            models = (Dictionary<string, PurchaseItemModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                if (purchaseConfig == null)
                {
                    purchaseConfig = parameterService.GetPurchaseConfig();
                }
                lock (syncLock)
                {
                    models = new Dictionary<string, PurchaseItemModel>();
                    foreach (PurchaseItem item in purchaseConfig.PurchaseItems)
                    {
                        if (item.Category == (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), category))
                        {
                            if (onlyValid)
                            {
                                if (item.Valid != "1")
                                {
                                    continue;
                                }
                            }
                            models.Add(item.Code, new PurchaseItemModel { Code = item.Code, Name = item.Name, Unit = item.Unit });
                        }
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                }
            }
            return models;
        }

        /*
        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public static Dictionary<string, DeliveryItemModel> GetDeliveryItem(string category, bool onlyValid)
        {
            Dictionary<string, DeliveryItemModel> models = new Dictionary<string, DeliveryItemModel>();
            string cacheName = string.Format("DeliveryItem_{0}{1}", category, onlyValid ? string.Empty : "_All");
            models = (Dictionary<string, PurchaseItemModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                if (deliveryConfig == null)
                {
                    deliveryConfig = parameterService.GetDeliveryConfig();
                }
                lock (syncLock)
                {
                    models = new Dictionary<string, DeliveryItemModel>();
                    foreach (DeliveryItem item in deliveryConfig.Products)
                    {
                            if (onlyValid)
                            {
                                if (item.Valid != "1")
                                {
                                    continue;
                                }
                            }
                            models.Add(item.Code, new DeliveryItemModel { Code = item.Code, Name = item.Name, Unit = item.Unit });
                       
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                }
            }
            return models;
        }
        */

        /// <summary>
        /// 获取成本项
        /// </summary>
        /// <param name="category">成本分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        public static Dictionary<string, CostItemModel> GetCostItem(string category, bool onlyValid)
        {
            Dictionary<string, CostItemModel> models = new Dictionary<string, CostItemModel>();
            string cacheName = string.Format("CostItem_{0}{1}", category, onlyValid ? string.Empty : "_All");
            models = (Dictionary<string, CostItemModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                List<CostItem> costs = new List<CostItem>();
                if (category.Equals(CostCategory.Purchase.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (purchaseConfig == null)
                    {
                        purchaseConfig = parameterService.GetPurchaseConfig();
                    }
                    costs = purchaseConfig.CostItems;
                }
                else if (category.Equals(CostCategory.Delivery.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (deliveryConfig == null)
                    {
                        deliveryConfig = parameterService.GetDeliveryConfig();
                    }
                    costs = deliveryConfig.CostItems;
                }


                lock (syncLock)
                {
                    models = new Dictionary<string, CostItemModel>();
                    foreach (CostItem item in costs)
                    {
                        if (onlyValid)
                        {
                            if (item.Valid != "1")
                            {
                                continue;
                            }
                        }
                        models.Add(item.Code, new CostItemModel { ItemCode = item.Code, ItemName = item.Name });
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                }
            }
            return models;
        }

        /// <summary>
        /// 获取分页大小
        /// </summary>
        /// <returns>返回分页大小</returns>
        public static int GetPageSize()
        {
            return int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
        }

        #endregion
    }
}