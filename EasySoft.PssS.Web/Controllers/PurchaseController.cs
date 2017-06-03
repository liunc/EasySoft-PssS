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
        /// <param name="page">当前页索引</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/Index/{category=Product}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string category, string item, int page = 1)
        {
            List<string> errorMessages = new List<string>();
            PurchaseCategory enumCategory = ValidateHelper.CheckPurchaseCategory(category, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                return RedirectToAction("Error", "Common", errorMessages);
            }

            PurchaseIndexModel model = new PurchaseIndexModel(enumCategory);
            model.SelectedItem = item;
            int totalCount = 0;
            List<Purchase> entities = this.purchaseService.Search(category, item, page, ParameterHelper.GetPageSize(), ref totalCount);
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(category, false);
            foreach (Purchase entity in entities)
            {
                PurchaseItemModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    entity.Item = purchaseItem.Name;
                }
                model.Data.Add(new PurchasePageModel(entity));
            }
            model.TotalCount = totalCount;
            model.PageIndex = page;
            return View(model);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="category">分类</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/Add/{category=Product}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string category)
        {
            List<string> errorMessages = new List<string>();
            PurchaseCategory enumCategory = ValidateHelper.CheckPurchaseCategory(category, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                throw new ArgumentException(errorMessages[0], "category");
            }

            return View(new PurchaseAddModel(enumCategory));
        }

        /// <summary>
        /// 提交产品采购入库
        /// </summary>
        /// <param name="model">采购入库的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult PostAdd(PurchaseAddModel model)
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
                model.PostValidate(ref errorMessages);
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }

                this.purchaseService.Add(model.Date, model.Category, model.Item.Trim(), model.Quantity, model.Unit.Trim(), model.Supplier, model.Remark, model.Costs, this.Session["Moblie"].ToString());
                result.Result = true;
                result.Data = "/Purchase/Index/" + model.CategoryString;
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
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Detail(string id, string item, int page = 1)
        {
            List<string> errorMessages = new List<string>();
            ValidateHelper.CheckStringArgument("Id", id, true, ref errorMessages);
            if (errorMessages.Count > 0)
            {
                return RedirectToAction("Error", "Common", errorMessages);
            }

            PurchaseDetailModel model = new PurchaseDetailModel(this.purchaseService.Select(id), item, page);
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(model.Category, false);
            PurchaseItemModel query = items[model.Item];
            if (query != null)
            {
                model.Item = query.Name;
            }
            return View(model);
        }

        /// <summary>
        /// 提交修改采购记录
        /// </summary>
        /// <param name="model">修改采购的数据</param>
        /// <returns>返回执行结果</returns>
        [HttpPost]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Delete(string id)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    result.BuilderErrorMessage(string.Format("{0}{1} Id{2}", WebResource.Message_ArgumentIsNull, WebResource.Field_Colon, WebResource.Common_FullStop));
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