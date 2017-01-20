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
        /// 提交产品采购入库
        /// </summary>
        /// <param name="model">采购入库的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult AddProduct(PurchaseAddModel model)
        {
            return PostPurchaseAdd(model, PurchaseCategory.Product);
        }

        /// <summary>
        /// 获取AddPack的视图 
        /// </summary>
        /// <returns>返回视图</returns>
        public ActionResult AddPack()
        {
            return View(this.GetPurchaseAddModel(PurchaseCategory.Pack));
        }

        /// <summary>
        /// 提交产品采购入库
        /// </summary>
        /// <param name="model">采购入库的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult AddPack(PurchaseAddModel model)
        {
            return PostPurchaseAdd(model, PurchaseCategory.Pack);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取采购入库的视图模型
        /// </summary>
        /// <param name="category">采购分类</param>
        /// <returns>采购入库的视图模型</returns>
        private PurchaseAddModel GetPurchaseAddModel(PurchaseCategory category)
        {
            PurchaseAddModel model = new PurchaseAddModel();
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(category, true);
            model.Costs = ParameterHelper.GetCostItem(CostCategory.IntoDepot, true);
            return model;
        }

        /// <summary>
        /// 提交采购入库数量
        /// </summary>
        /// <param name="model">采购入库的视图模型</param>
        /// <param name="category">采购分类</param>
        /// <returns>返回执行结果</returns>
        private JsonResult PostPurchaseAdd(PurchaseAddModel model, PurchaseCategory category)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                List<string> errorMessages = new List<string>();
                Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
                DateTime date = new DateTime();

                #region 验证

                if (model == null)
                {
                    errorMessages.Add(WebResource.Message_ArgumentIsNull);
                }
                else
                {
                    if (!DateTime.TryParse(model.Date, out date))
                    {
                        errorMessages.Add(WebResource.Purchase_AddProduct_DateTip + WebResource.Flag_FullStop);
                    }
                    if (string.IsNullOrWhiteSpace(model.Item))
                    {
                        errorMessages.Add(category == PurchaseCategory.Product ? WebResource.Purchase_AddProduct_ItemTip : WebResource.Purchase_AddProduct_ItemTip1 + WebResource.Flag_FullStop);
                    }
                    if (model.Quantity <= 0)
                    {
                        errorMessages.Add(WebResource.Purchase_AddProduct_QuantityTip);
                    }
                    if (!string.IsNullOrWhiteSpace(model.Supplier) && model.Supplier.Trim().Length > 50)
                    {
                        errorMessages.Add(WebResource.Purchase_AddProduct_SupplierTip + WebResource.Flag_FullStop);
                    }
                    if (model.Costs == null || model.Costs.Count == 0)
                    {
                        errorMessages.Add(WebResource.Purchase_AddProduct_CostDataIsNull);
                    }
                    else
                    {
                        foreach (CostModel cost in model.Costs)
                        {
                            if (string.IsNullOrWhiteSpace(cost.ItemCode))
                            {
                                errorMessages.Add(WebResource.Purchase_AddProduct_CostItemIsNull);
                                break;
                            }
                            if (cost.Money < 0)
                            {
                                errorMessages.Add(WebResource.Purchase_AddProduct_CostMoneyTip);
                                break;
                            }
                            costs.Add(cost.ItemCode, cost.Money);
                        }
                    }
                }
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }

                #endregion

                this.purchaseService.Add(date, category, model.Item, model.Quantity, model.Unit, model.Supplier, model.Remark, costs, this.Session["Moblie"].ToString());
                result.Result = true;
                result.Data = "/Purchase/Index";
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.ToString());
                return Json(result);
            }
        }

        #endregion
    }
}