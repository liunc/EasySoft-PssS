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
    using EasySoft.Core.ViewModel;
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
    using Core.Util;

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
            Validate validate = new Validate();
            validate.CheckDictionary<string, string>(WebResource.Field_ProfitLossCategory, category, ParameterHelper.GetProfitLossCatetory());
            validate.CheckDictionary<string, string>(WebResource.Field_ProfitLossTargetType, targetType, ParameterHelper.GetProfitLossTargetType());
            validate.CheckStringArgument(WebResource.Field_RecordId, recordId, true);
            if (validate.IsFailed)
            {
                return RedirectToAction("Error", "Common", validate.ErrorMessages);
            }

            ProfitLossAddModel model = new ProfitLossAddModel { RecordId = recordId, Category = category, TargetType = targetType, Title = WebResource.Title_Loss };
            if (targetType == ProfitLossTargetType.Purchase)
            {
                Purchase purchase = this.purchaseService.Select(recordId);
                model.ReturnUrl = string.Format("/Purchase/Detail/{0}?&category={1}&item={2}&page={3}", recordId, purchase.Category, item, page);
                model.Inventory = purchase.Inventory;
                model.Unit = purchase.Unit;

                if (category == ProfitLossCategory.Profit)
                {
                    model.Title = WebResource.Title_Profit;
                    model.Inventory = Constant.DECIMAL_MAX;
                }
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
                Validate validate = new Validate();
                validate.CheckObjectArgument<ProfitLossAddModel>("model", model);
                if(validate.IsFailed)
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

                this.profitLossService.Add(model.RecordId.Trim(), model.TargetType, model.Category, model.Quantity, model.Remark, this.Session["Mobile"].ToString());
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
