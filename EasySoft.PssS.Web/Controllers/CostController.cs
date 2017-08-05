// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-13
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Domain.Service;
    using Models;
    using Models.Purchase;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Resources;
    using System;
    using System.Configuration;
    using System.Linq;
    using Filters;
    using Models.Cost;
    using Models.CostItem;

    /// <summary>
    /// 成本控制器类
    /// </summary>
    public class CostController : Controller
    {
        #region 变量

        private CostService costService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CostController()
        {
            this.costService = new CostService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取成本数据
        /// </summary>
        /// <param name="id">分类</param>
        /// <returns>返回成本数据</returns>
        public ActionResult GetList(string id)
        {
            List<CostPageModel> models = new List<CostPageModel>();
            List<Cost> entities = this.costService.GetList(id);
            Dictionary<string, CostItemCacheModel> costItems = ParameterHelper.GetCostItem(CostItemCategory.Purchase, false);
            foreach (Cost entity in entities)
            {
                string itemName = entity.Item;
                short orderNumber = 0;
                CostItemCacheModel query = costItems[entity.Item];
                if (query != null)
                {
                    itemName = query.Name;
                    orderNumber = query.OrderNumber;
                }
                models.Add(new CostPageModel { Id = entity.Id, ItemCode = entity.Item, ItemName = itemName, Money = entity.Money, OrderNumber = orderNumber });
            }
            models = models.OrderBy(i => i.OrderNumber).ToList();
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}