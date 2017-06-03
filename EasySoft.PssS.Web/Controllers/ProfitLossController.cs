// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-02-18
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
    using Filters;
    using Models;
    using Models.ProfitLoss;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// 益损控制器类
    /// </summary>
    public class ProfitLossController : Controller
    {
        #region 变量

        private ProfitLossService profitLossService = null;
        private PurchaseService purchaseService = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProfitLossController()
        {
            this.profitLossService = new ProfitLossService();
            this.purchaseService = new PurchaseService();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取益损数据
        /// </summary>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回益损数据</returns>
        public ActionResult GetList(string recordId)
        {
            List<ProfitLossModel> models = new List<ProfitLossModel>();
            List<ProfitLoss> entities = this.profitLossService.GetList(recordId);
            foreach (ProfitLoss entity in entities)
            {
                models.Add(new ProfitLossModel { Id = entity.Id, CategoryString = entity.Category.ToString(), Quantity = entity.Quantity, Remark = entity.Remark });
            }
            return Json(models, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取Add视图
        /// </summary>
        /// <param name="category">类型</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回视图</returns>
        [Route("ProfitLoss/Add/{category}/{targetType}/{recordId}")]
        [MyAuthorize(AuthRoles = new string[] { "Admin" })]
        public ActionResult Add(string category, string targetType, string recordId, string item, int page)
        {
            List<string> errorMessages = new List<string>();
            ProfitLossCategory enumCategory = ValidateHelper.CheckProfitLossCategory(category, ref errorMessages);
            ProfitLossTargetType enumTargetType = ValidateHelper.CheckProfitLossTargetType(targetType, ref errorMessages);
            ValidateHelper.CheckStringArgument(WebResource.Field_RecordId, recordId, true, ref errorMessages);
            if(errorMessages.Count > 0)
            {
                return RedirectToAction("Error", "Common", errorMessages);
            }

            ProfitLossAddModel model = new ProfitLossAddModel { RecordId = recordId, CategoryString = enumCategory.ToString(), TargetTypeString = enumTargetType.ToString(), Title = WebResource.Title_Loss };
            
            if (enumTargetType == ProfitLossTargetType.Purchase)
            {
                Purchase purchase = this.purchaseService.Select(recordId);
                string purchaseCategory = string.Empty;
                if (purchase.Category == PurchaseCategory.Product)
                {
                    model.ParentPageTitle = WebResource.Title_Purchase_Product;
                }
                else
                {
                    model.ParentPageTitle = WebResource.Title_Purchase_Pack;
                }
                model.ParentPageUrl = string.Format("/Purchase/Index/{0}?item={1}&page={2}", purchase.Category.ToString(), item, page);
                model.ReturnUrl = string.Format("/Purchase/Detail/{0}?item={1}&page={2}", recordId, item, page);
                model.Inventory = purchase.Inventory;
                model.Unit = purchase.Unit;
                
            }
            if (enumCategory == ProfitLossCategory.Profit)
            {
                model.Title = WebResource.Title_Profit;
                model.Inventory = ValidateHelper.DECIMAL_MAX;
            }
            return View(model);
        }

        /// <summary>
        /// 提交新增益损数据
        /// </summary>
        /// <param name="model">益损数据</param>
        /// <returns>返回执行结果</returns>
        public ActionResult Add(ProfitLossAddModel model)
        {
            JsonResultModel result = new JsonResultModel();
            try
            {
                List<string> errorMessages = new List<string>();
                if (!ValidateHelper.CheckObjectArgument<ProfitLossAddModel>("model", model, ref errorMessages))
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

                this.profitLossService.Add(model.RecordId.Trim(), model.TargetType, model.Category, model.Quantity, model.Remark, this.Session["Moblie"].ToString());
                result.Result = true;
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
