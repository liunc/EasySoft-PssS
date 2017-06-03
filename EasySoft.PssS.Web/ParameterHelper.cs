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
    using Models;

    /// <summary>
    /// 参数工具类
    /// </summary>
    public class ParameterHelper
    {
        #region 变量

        private static readonly object syncLock = new object();
        private static ParameterService parameterService = new ParameterService();
        private static CustomerGroupService customerGroupService = new CustomerGroupService();
        private static PurchaseConfig purchaseConfig;
        private static DeliveryConfig deliveryConfig;
        private static int pageSize = 0;

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public static Dictionary<string, PurchaseItemModel> GetPurchaseItem(PurchaseCategory category, bool onlyValid)
        {
            Dictionary<string, PurchaseItemModel> models = new Dictionary<string, PurchaseItemModel>();
            string cacheName = string.Format("PurchaseItem_{0}{1}", category.ToString(), onlyValid ? string.Empty : "_All");
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
                        if (item.Category == category)
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

        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public static Dictionary<string, PurchaseItemModel> GetPurchaseItem(string category, bool onlyValid)
        {
            PurchaseCategory enumCategory = default(PurchaseCategory);
            if (Enum.TryParse<PurchaseCategory>(category, out enumCategory))
            {
                return GetPurchaseItem(enumCategory, onlyValid);
            }
            throw new ArgumentException("PurchaseCategory");
        }

        /// <summary>
        /// 获取成本项
        /// </summary>
        /// <param name="category">成本分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        public static Dictionary<string, CostItemModel> GetCostItem(CostCategory category, bool onlyValid)
        {
            Dictionary<string, CostItemModel> models = new Dictionary<string, CostItemModel>();
            string cacheName = string.Format("CostItem_{0}{1}", category.ToString(), onlyValid ? string.Empty : "_All");
            models = (Dictionary<string, CostItemModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                List<CostItem> costs = new List<CostItem>();
                if (category == CostCategory.Purchase)
                {
                    if (purchaseConfig == null)
                    {
                        purchaseConfig = parameterService.GetPurchaseConfig();
                    }
                    costs = purchaseConfig.CostItems;
                }
                else if (category == CostCategory.Outbound)
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
        /// 获取成本项
        /// </summary>
        /// <param name="category">成本分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        public static Dictionary<string, CostItemModel> GetCostItem(string category, bool onlyValid)
        {
            CostCategory enumCategory = default(CostCategory);
            if (Enum.TryParse<CostCategory>(category, out enumCategory))
            {
                return GetCostItem(enumCategory, onlyValid);
            }
            throw new ArgumentException("CostCategory");
        }

        /// <summary>
        /// 获取分页大小
        /// </summary>
        /// <returns>返回分页大小</returns>
        public static int GetPageSize()
        {
            if (pageSize <= 0)
            {
                pageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"].ToString());
            }
            return pageSize;
        }

        /// <summary>
        /// 获取客户分组数据
        /// </summary>
        /// <returns>返回客户分组</returns>
        public static List<ValueTextModel> GetCustomerGroup()
        {
            List<ValueTextModel> models = new List<ValueTextModel>();
            string cacheName = "CustomerGroup";
            models = (List<ValueTextModel>)HttpRuntime.Cache[cacheName];
            if (models == null)
            {
                List<CustomerGroup> groups = customerGroupService.All();

                lock (syncLock)
                {
                    models = new List<ValueTextModel>();
                    foreach (CustomerGroup item in groups)
                    {
                        models.Add(new ValueTextModel { Value = item.Id, Text = item.Name });
                    }
                    HttpRuntime.Cache.Insert(cacheName, models, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                }
            }
            return models;
        }

        #endregion
    }
}