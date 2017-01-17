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

    /// <summary>
    /// 采购控制器类
    /// </summary>
    public class PurchaseController : Controller
    {
        #region 变量

        private PurchaseService purchaseService = null;
        private ParameterService parameterService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseController()
        {
            this.purchaseService = new PurchaseService();
            this.parameterService = new ParameterService();
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

        public ActionResult AddProduct()
        {
            return View(this.GetPurchaseAddModel(PurchaseCategory.Product));
        }

        private PurchaseAddModel GetPurchaseAddModel(PurchaseCategory category)
        {
            PurchaseAddModel model = new PurchaseAddModel();
            List<KeyValueModel> purchaseItems = new List<KeyValueModel>();
            foreach(PurchaseItem item in this.parameterService.GetPurchaseItem(category.ToString(), true))
            {
                purchaseItems.Add(new KeyValueModel { Key = item.Code, Value = item.Name });
            }
            model.PurchaseItems = purchaseItems;
            List<CostDetailModel> costDetails = new List<CostDetailModel>();
            foreach (CostItem item in this.parameterService.GetCostItem(CostCategory.PurchaseInput.ToString(), true))
            {
                costDetails.Add(new CostDetailModel { ItemCode=item.Code, ItemName = item.Name, Money = 0 });
            }
            model.CostDetails = costDetails;
            return model;
        }

        #endregion
    }
}