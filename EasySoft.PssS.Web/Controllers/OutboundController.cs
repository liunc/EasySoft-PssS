// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Controllers
{
    using Domain.Entity;
    using Domain.Service;
    using Domain.ValueObject;
    using EasySoft.PssS.Web.Models.Outbound;
    using Filters;
    using Models.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// 出库控制器类
    /// </summary>
    public class OutboundController : Controller
    {
        #region 变量

        private PurchaseService purchaseService = null;
        private CostService costService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public OutboundController()
        {
            this.purchaseService = new PurchaseService();
            this.costService = new CostService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取Index视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult First()
        {
            OutboundFirstModel model = new OutboundFirstModel();
            int totalCount = 0;
            List<Purchase> entities = this.purchaseService.Search(PurchaseCategory.Product.ToString(), string.Empty, 1, int.MaxValue, ref totalCount);
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(PurchaseCategory.Product, false);
            foreach (Purchase entity in entities)
            {
                PurchaseItemModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    entity.Item = purchaseItem.Name;
                }
                model.Products.Add(new PurchasePageModel(entity));
            }
            return View(model);
        }

        #endregion
    }
}