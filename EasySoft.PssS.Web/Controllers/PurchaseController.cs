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
    using EasySoft.Core.Util;
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.Service;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Filters;
    using EasySoft.PssS.Web.Models.Purchase;
    using EasySoft.PssS.Web.Models.PurchaseItem;
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
        [Route("Purchase/Index/{category=" + PurchaseItemCategory.Product + "}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Index(string category, string item, int page = 1)
        {
            Validate validate = new Validate();
            validate.CheckDictionary<string, string>(WebResource.Field_PurchaseCategory, category, ParameterHelper.GetPurchaseItemCatetory());
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }

            PurchaseIndexModel model = new PurchaseIndexModel(category, page);
            model.SelectedItem = item;
            int totalCount = 0;
            List<Purchase> entities = this.purchaseService.Search(category, item, model.PageIndex, model.PageSize, ref totalCount);
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(category, false);
            foreach (Purchase entity in entities)
            {
                PurchasePageModel pageModel = new PurchasePageModel(entity);
                PurchaseItemCacheModel purchaseItem = items[entity.Item];
                if (purchaseItem != null)
                {
                    pageModel.Item = purchaseItem.Name;
                }
                model.PageData.Add(pageModel);
            }
            model.TotalCount = totalCount;
            return View(model);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="category">分类</param>
        /// <returns>返回视图</returns>
        [Route("Purchase/Add/{category}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string category)
        {
            Validate validate = new Validate();
            validate.CheckDictionary<string, string>(WebResource.Field_PurchaseCategory, category, ParameterHelper.GetPurchaseItemCatetory());
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }

            return View(new PurchaseAddModel(category));
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
                Validate validate = new Validate();
                validate.CheckObjectArgument<PurchaseAddModel>("model", model);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }
                model.PostValidate(ref validate);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                    return Json(result);
                }

                this.purchaseService.Add(model.Date, model.Category, model.Item.Trim(), model.Quantity, model.Unit.Trim(), model.Supplier, model.Remark, model.CostData, this.Session["Mobile"].ToString());
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
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Detail(string id, string item, int page = 1)
        {
            Validate validate = new Validate();
            validate.CheckStringArgument("Id", id, true);
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }

            PurchaseDetailModel model = new PurchaseDetailModel(this.purchaseService.Select(id), item, page);
            Dictionary<string, PurchaseItemCacheModel> items = ParameterHelper.GetPurchaseItem(model.Category, false);
            PurchaseItemCacheModel query = items[model.Item];
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
                Validate validate = new Validate();
                validate.CheckStringArgument(WebResource.Field_Id, id, true);
                if (validate.IsFailed)
                {
                    result.BuilderErrorMessage(validate.ErrorMessages);
                }
                else
                {
                    this.purchaseService.Delete(id);
                    result.Result = true;
                }
            }
            catch (Exception ex)
            {
                result.BuilderErrorMessage(ex.Message);
            }
            return Json(result);
        }

        #endregion
    }
}