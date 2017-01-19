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
        /// <returns>返回视图</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取AddProduct的视图 
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult AddProduct()
        {
            return View(this.GetPurchaseAddModel(PurchaseCategory.Product));
        }

        /// <summary>
        /// 提交登录
        /// </summary>
        /// <param name="model">登录数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult AddProduct(PurchaseAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            DateTime date = new DateTime();
            if (model == null)
            {
                result.Message = WebResource.ArgumentNull;
            }
            else
            {
                List<string> errorMessages = new List<string>();
                if (!DateTime.TryParse(model.Date, out date))
                {
                    errorMessages.Add(WebResource.PurchaseAdd_DateTip);
                }
                if (string.IsNullOrWhiteSpace(model.Item))
                {
                    errorMessages.Add(WebResource.PurchaseAdd_ItemTip);
                }
                if(model.Quantity <= 0)
                {
                    errorMessages.Add(WebResource.PurchaseAdd_QuantityTip);
                }
            }
            if (!string.IsNullOrWhiteSpace(result.Message))
            {
                return Json(result);
            }
            Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
            foreach(CostModel cost in model.Costs)
            {
                costs.Add(cost.ItemCode, cost.Money);
            }
            this.purchaseService.Add(date, PurchaseCategory.Product, model.Item.Trim(), model.Quantity, model.Unit, model.Supplier.Trim(),model.Remark.Trim(), costs, this.Session["Moblie"].ToString());
            result.Result = true;
            result.Message = "提交成功。";
            return Json(result);
        }
        private PurchaseAddModel GetPurchaseAddModel(PurchaseCategory category)
        {
            PurchaseAddModel model = new PurchaseAddModel();
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(category, true);
            model.Costs = ParameterHelper.GetCostItem(CostCategory.PurchaseInput, true);
            return model;
        }

        #endregion
    }
}