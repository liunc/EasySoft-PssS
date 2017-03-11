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
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models;
    using EasySoft.PssS.Web.Models.Purchase;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    /// 采购控制器类
    /// </summary>
    public class PurchaseController : Controller
    {
        #region 变量

        private PurchaseService purchaseService = null;
        private CostService costService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseController()
        {
            this.purchaseService = new PurchaseService();
            this.costService = new CostService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取Index视图
        /// </summary>
        /// <param name="category">分类</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/Index/{category=Product}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string category)
        {
            List<string> errorMessages = new List<string>();
            PurchaseCategory enumCategory = ValidateHelper.CheckPurchaseCategory(category, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                return RedirectToAction("Index", "Error", errorMessages);
            }

            PurchaseIndexModel model = new PurchaseIndexModel { Category = enumCategory.ToString(), Title = WebResource.Title_Purchase_Product };
            if (enumCategory == PurchaseCategory.Pack)
            {
                model.Title = WebResource.Title_Purchase_Pack;
            }
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(model.Category, false).Values.ToList();
            return View(model);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="pageIndex">当前索引</param>
        /// <returns>返回分页数据</returns>
        [HttpPost]
        public ActionResult GetDataByPaing(string category, string item, int pageIndex)
        {
            int totalCount = 0;
            PagingModel<PurchaseModel> model = new PagingModel<PurchaseModel>();
            model.Data = new List<PurchaseModel>();
            List<Purchase> entities = this.purchaseService.Search(category, item, pageIndex, model.PageSize, ref totalCount);
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(category, false);

            foreach (Purchase entity in entities)
            {
                PurchaseItemModel query = items[entity.Item];
                if (query != null)
                {
                    entity.Item = query.Name;
                }
                model.Data.Add(new PurchaseModel(entity));
            }
            model.TotalCount = totalCount;
            return Json(model);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="category">分类</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/IntoDepot/{category=Product}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult IntoDepot(string category)
        {
            List<string> errorMessages = new List<string>();
            PurchaseCategory enumCategory = ValidateHelper.CheckPurchaseCategory(category, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                return RedirectToAction("Index", "Error", errorMessages);
            }

            PurchaseAddModel model = new PurchaseAddModel { Category = enumCategory.ToString(), Title = WebResource.Title_Purchase_AddProduct, ParentPageTitle = WebResource.Title_Purchase_Product };
            if (enumCategory == PurchaseCategory.Pack)
            {
                model.Title = WebResource.Title_Purchase_AddPack;
                model.ParentPageTitle = WebResource.Title_Purchase_Pack;
            }
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(category, true).Values.ToList();
            model.Costs = new List<CostModel>();
            foreach (CostItemModel cost in ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), true).Values)
            {
                model.Costs.Add(new CostModel { ItemCode = cost.ItemCode, ItemName = cost.ItemName });
            }
            return View(model);
        }

        /// <summary>
        /// 提交产品采购入库
        /// </summary>
        /// <param name="model">采购入库的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [Route("Purchase/IntoDepot/{category=Product}")]
        public ActionResult IntoDepot(PurchaseAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                List<string> errorMessages = new List<string>();
                if (!ValidateHelper.CheckObjectArgument<PurchaseAddModel>("model", model, ref errorMessages))
                {
                    result.BuilderErrorMessage(errorMessages[0]);
                    return Json(result);
                }

                PurchaseCategory enumCategory = ValidateHelper.CheckPurchaseCategory(model.Category, ref errorMessages);
                DateTime date = ValidateHelper.CheckDateString(WebResource.Field_Date, model.Date, true, ref errorMessages);
                ValidateHelper.CheckInputString(WebResource.Field_Supplier, model.Supplier, false, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
                ValidateHelper.CheckSelectString(WebResource.Field_Item, model.Item, true, ParameterHelper.GetPurchaseItem(enumCategory.ToString(), true).Keys.ToList(), ref errorMessages);
                ValidateHelper.CheckDecimal(WebResource.Field_Quantity, model.Quantity, ValidateHelper.DECIMAL_MIN, ValidateHelper.DECIMAL_MAX, ref errorMessages);
                
                Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
                if (model.Costs != null && model.Costs.Count > 0)
                {
                    List<string> costOptions = ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), true).Keys.ToList();
                    foreach (CostModel cost in model.Costs)
                    {
                        if (!ValidateHelper.CheckSelectString(WebResource.Field_CostItem, cost.ItemCode, true, costOptions, ref errorMessages))
                        {
                            break;
                        }
                        if (!ValidateHelper.CheckDecimal(WebResource.Field_CostItemMoney, cost.Money, ValidateHelper.DECIMAL_ZERO, ValidateHelper.DECIMAL_MAX, ref errorMessages))
                        {
                            break;
                        }
                        costs.Add(cost.ItemCode, cost.Money);
                    }
                }
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }
                
                this.purchaseService.IntoDepot(date, enumCategory, model.Item.Trim(), model.Quantity, model.Unit.Trim(), model.Supplier, model.Remark, costs, this.Session["Moblie"].ToString());
                result.Result = true;
                result.Data = "/Purchase/Index/" + model.Category;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 获取Detail视图
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回视图</returns>
        public ActionResult Detail(string id)
        {
            List<string> errorMessages = new List<string>();
            ValidateHelper.CheckStringArgument("Id", id, true, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                return RedirectToAction("Index", "Error", errorMessages);
            }

            PurchaseDetailModel model = new PurchaseDetailModel(this.purchaseService.Selete(id));
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(model.Category, false);
            var query = ParameterHelper.GetPurchaseItem(model.Category, false)[model.Item];
            if (query != null)
            {
                model.Item = query.Name;
            }
            return View(model);
        }

        /// <summary>
        /// 获取Edit视图
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>返回视图</returns>
        public ActionResult Edit(string id)
        {
            Purchase entity = this.purchaseService.Selete(id);
            entity.Costs = this.costService.GetList(id);
            PurchaseEditModel model = new PurchaseEditModel(entity);
            PurchaseItemModel query = ParameterHelper.GetPurchaseItem(model.Category, false)[entity.Item];
            if (query != null)
            {
                model.ItemName = query.Name;
            }
            return View(model);
        }

        /// <summary>
        /// 提交修改采购记录
        /// </summary>
        /// <param name="model">修改采购的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult Edit(PurchaseEditModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                List<string> errorMessages = new List<string>();
                
                #region 验证

                if(!ValidateHelper.CheckObjectArgument<PurchaseEditModel>("model", model, ref errorMessages))
                {
                    result.BuilderErrorMessage(errorMessages[0]);
                    return Json(result);
                }
                ValidateHelper.CheckStringArgument("model.Id", model.Id, true, ref errorMessages);
                DateTime date = ValidateHelper.CheckDateString(WebResource.Field_Date, model.Date, true, ref errorMessages);
                ValidateHelper.CheckInputString(WebResource.Field_Supplier, model.Supplier, false, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
                ValidateHelper.CheckDecimal(WebResource.Field_Quantity, model.Quantity, ValidateHelper.DECIMAL_MIN, ValidateHelper.DECIMAL_MAX, ref errorMessages);
                ValidateHelper.CheckInputString(WebResource.Field_Remark, model.Remark, false,  ValidateHelper.STRING_LENGTH_120, ref errorMessages);

                Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
                if (model.Costs != null && model.Costs.Count > 0)
                {
                    List<string> costOptions = ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), true).Keys.ToList();
                    foreach (CostModel cost in model.Costs)
                    {
                        if (!ValidateHelper.CheckInputString(WebResource.Field_CostItem, cost.Id, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages))
                        {
                            break;
                        }
                        if (!ValidateHelper.CheckDecimal(WebResource.Field_CostItemMoney, cost.Money, ValidateHelper.DECIMAL_ZERO, ValidateHelper.DECIMAL_MAX, ref errorMessages))
                        {
                            break;
                        }
                        costs.Add(cost.Id, cost.Money);
                    }
                }
                
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }

                #endregion

                this.purchaseService.Update(model.Id, date, model.Quantity, model.Supplier, model.Remark, costs, this.Session["Moblie"].ToString());
                result.Result = true;
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

        /// <summary>
        /// 提交修改采购记录
        /// </summary>
        /// <param name="model">修改采购的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    result.BuilderErrorMessage(string.Format("{0}{1} Id{2}", WebResource.Message_ArgumentIsNull, WebResource.Message_Colon, WebResource.Common_FullStop));
                }
                else
                {
                    this.purchaseService.Delete(id);
                    result.Result = true;
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
                return Json(result);
            }
        }

#endregion
    }
}