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
    using Models;
    using Models.ProfitLoss;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                models.Add(new ProfitLossModel { Id = entity.Id, Category = entity.Category.ToString(), Quantity = entity.Quantity, Remark = entity.Remark });
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
        public ActionResult Add(string category, string targetType, string recordId)
        {
            List<string> errorMessages = new List<string>();
            ProfitLossCategory enumCategory = ValidateHelper.CheckProfitLossCategory(category, ref errorMessages);
            ProfitLossTargetType enumTargetType = ValidateHelper.CheckProfitLossTargetType(targetType, ref errorMessages);
            ValidateHelper.CheckStringArgument(WebResource.Field_RecordId, recordId, true, ref errorMessages);
            if(errorMessages.Count > 0)
            {
                return RedirectToAction("Index", "Error", errorMessages);
            }

            ProfitLossAddModel model = new ProfitLossAddModel { RecordId = recordId, Category = enumCategory.ToString(), TargetType = enumTargetType.ToString(), Title = WebResource.Title_Loss };
            if (enumTargetType == ProfitLossTargetType.Purchase)
            {
                Purchase purchase = this.purchaseService.Selete(recordId);
                if (purchase.Category == PurchaseCategory.Product)
                {
                    model.ParentPageTitle = WebResource.Title_Purchase_Product;
                    model.ParentPageUrl = "/Purchase/Index/Product";
                }
                else
                {
                    model.ParentPageTitle = WebResource.Title_Purchase_Pack;
                    model.ParentPageUrl = "/Purchase/Index/Pack";
                }
                model.Allowance = purchase.Allowance;
                model.Unit = purchase.Unit;
            }
            if (enumCategory == ProfitLossCategory.Profit)
            {
                model.Title = WebResource.Title_Profit;
                model.Allowance = ValidateHelper.DECIMAL_MAX;
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

                ProfitLossCategory enumCategory = ValidateHelper.CheckProfitLossCategory(model.Category, ref errorMessages);
                ProfitLossTargetType enumTargetType = ValidateHelper.CheckProfitLossTargetType(model.TargetType, ref errorMessages);
                ValidateHelper.CheckStringArgument(WebResource.Field_RecordId, model.RecordId, true, ref errorMessages);
                ValidateHelper.CheckDecimal(WebResource.Field_Quantity, model.Quantity, ValidateHelper.DECIMAL_MIN, ValidateHelper.DECIMAL_MAX, ref errorMessages);
                ValidateHelper.CheckInputString(WebResource.Field_Remark, model.Remark, false, ValidateHelper.STRING_LENGTH_120, ref errorMessages);
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }

                this.profitLossService.Add(model.RecordId.Trim(), enumTargetType, enumCategory, model.Quantity, model.Remark, this.Session["Moblie"].ToString());
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
