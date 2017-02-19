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
            List<CostModel> models = new List<CostModel>();
            List<Cost> entities = this.costService.GetList(id);
            List<CostItemModel> costItems = ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), false);
            foreach (Cost entity in entities)
            {
                string itemName = entity.Item;
                var query = costItems.Where(i => i.ItemCode == entity.Item).FirstOrDefault();
                if (query != null)
                {
                    itemName = query.ItemName;
                }
                models.Add(new CostModel { Id = entity.Id, ItemCode = entity.Item, ItemName = itemName, Money = entity.Money });
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
        
    }
}