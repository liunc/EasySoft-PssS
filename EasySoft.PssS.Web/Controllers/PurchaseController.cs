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
        /// <param name="category">分类</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/Index/{category=Product}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string category)
        {
            string title = WebResource.Purchase_Index_ProductTitle;
            PurchaseCategory enumCategory = this.GetPurchaseCategory(category);
            if (enumCategory == PurchaseCategory.Pack)
            {
                title = WebResource.Purchase_Index_PackTitle;
            }
            PurchaseIndexModel model = new PurchaseIndexModel();
            model.Category = enumCategory.ToString();
            model.Title = title;
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(model.Category, false);
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
            List<PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(category, false);

            foreach (Purchase entity in entities)
            {
                var query = items.Where(i => i.Code == entity.Item).FirstOrDefault();
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
        public ActionResult IntoDepot(string category)
        {
            string title = WebResource.Purchase_Add_ProductTitle;
            PurchaseCategory enumCategory = this.GetPurchaseCategory(category);
            if (enumCategory == PurchaseCategory.Pack)
            {
                title = WebResource.Purchase_Add_PackTitle;
            }

            PurchaseAddModel model = new PurchaseAddModel();
            model.Title = title;
            model.PurchaseItems = ParameterHelper.GetPurchaseItem(category, true);
            model.Category = enumCategory.ToString();
            model.Costs = new List<CostModel>();
            foreach(CostItemModel cost in ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), true))
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
                        errorMessages.Add(WebResource.Purchase_Add_DateTip + WebResource.Common_FullStop);
                    }
                    if (string.IsNullOrWhiteSpace(model.Item))
                    {
                        errorMessages.Add(WebResource.Purchase_Add_ItemTip + WebResource.Common_FullStop);
                    }
                    if (model.Quantity <= 0)
                    {
                        errorMessages.Add(WebResource.Purchase_Add_QuantityTip);
                    }
                    if (!string.IsNullOrWhiteSpace(model.Supplier) && model.Supplier.Trim().Length > 50)
                    {
                        errorMessages.Add(WebResource.Purchase_Add_SupplierTip + WebResource.Common_FullStop);
                    }
                    if (model.Costs == null || model.Costs.Count == 0)
                    {
                        errorMessages.Add(WebResource.Purchase_Add_CostDataIsNull);
                    }
                    else
                    {
                        foreach (CostModel cost in model.Costs)
                        {
                            if (string.IsNullOrWhiteSpace(cost.ItemCode))
                            {
                                errorMessages.Add(WebResource.Purchase_Add_CostItemIsNull);
                                break;
                            }
                            if (cost.Money < 0)
                            {
                                errorMessages.Add(WebResource.Purchase_Add_CostMoneyTip);
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

                this.purchaseService.IntoDepot(date, this.GetPurchaseCategory(model.Category), model.Item.Trim(), model.Quantity, model.Unit.Trim(), model.Supplier, model.Remark, costs, this.Session["Moblie"].ToString());
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
            PurchaseDetailModel model = new PurchaseDetailModel(this.purchaseService.Find(id));
            List<PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(model.Category, false);
            var query = items.Where(i => i.Code == model.Item).FirstOrDefault();
            if (query != null)
            {
                model.Item = query.Name;
            }
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
        public ActionResult GetCostList(string id)
        {
            List<CostModel> models = new List<CostModel>();
            List<Cost> entities = this.purchaseService.GetCostList(id);
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
            return Json(models);
        }

        public ActionResult Edit(string id)
        {
            return View();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 将类型字符串转换为枚举值
        /// </summary>
        /// <param name="category">类型字符串</param>
        /// <returns>返回采购枚举值</returns>
        private PurchaseCategory GetPurchaseCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("Category");
            }
            category = category.ToLower();
            if (category == "product")
            {
                return PurchaseCategory.Product;
            }
            if (category == "pack")
            {
                return PurchaseCategory.Pack;
            }
            throw new ArgumentException("Category");
        }

        #endregion
    }
}