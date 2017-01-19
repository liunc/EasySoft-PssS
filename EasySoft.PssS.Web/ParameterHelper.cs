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
    using EasySoft.PssS.Domain.ValueObject;
    using Models;
    using Models.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Caching;

    /// <summary>
    /// 参数工具类
    /// </summary>
    public class ParameterHelper
    {
        #region 变量

        private static readonly object syncLock = new object();

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public static List<PurchaseItemModel> GetPurchaseItem(PurchaseCategory category, bool onlyValid)
        {
            List<PurchaseItemModel> models = new List<PurchaseItemModel>();
            foreach (PurchaseItem item in GetParameter<PurchaseItem>("PurchaseItem", category.ToString(), onlyValid, new ParameterService().GetPurchaseItem))
            {
                models.Add(new PurchaseItemModel { Code = item.Code, Name = item.Name, InputUnit = item.InputUnit, OutputUnit = item.OutputUnit, InOutRate = item.InOutRate });
            }
            return models;
        }

        /// <summary>
        /// 获取成本项
        /// </summary>
        /// <param name="category">成本分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        public static List<CostModel> GetCostItem(CostCategory category, bool onlyValid)
        {
            List<CostModel> models = new List<CostModel>();
            foreach (CostItem item in GetParameter<CostItem>("CostItem", category.ToString(), onlyValid, new ParameterService().GetCostItem))
            {
                models.Add(new CostModel { ItemCode = item.Code, ItemName = item.Name, Money = 0 });
            }
            return models;
        }

        /// <summary>
        /// 获取参数项
        /// </summary>
        /// <param name="category">参数分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回参数项信息</returns>
        private static List<T> GetParameter<T>(string name, string category, bool onlyValid, Func<string, bool, List<T>> getItems)
        {
            List<T> items = new List<T>();
            string cacheName = string.Format("{0}_{1}{2}", name, category, onlyValid ? string.Empty : "_All");
            items = (List<T>)HttpRuntime.Cache[cacheName];
            if (items == null)
            {
                lock (syncLock)
                {
                    items = getItems(category, onlyValid);
                    HttpRuntime.Cache.Insert(cacheName, items, null, DateTime.Now.AddMinutes(60), Cache.NoSlidingExpiration);
                }
            }
            return items;
        }

        #endregion
    }
}