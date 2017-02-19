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
        /// <param name="targetType">目标类型</param>
        /// <param name="recordId">记录Id</param>
        /// <returns>返回视图</returns>
        [Route("ProfitLoss/Add/{targetType}/{recordId}")]
        public ActionResult Add(string targetType, string recordId)
        {
            if (string.IsNullOrWhiteSpace(targetType))
            {
                throw new Exception(string.Format("{0}{1}", WebResource.ProfitLoss_Add_TargetTypeTip + WebResource.Common_FullStop));
            }
            if (string.IsNullOrWhiteSpace(recordId))
            {
                throw new Exception(string.Format("{0}{1} {2}{3}", WebResource.Message_ArgumentIsNull, WebResource.Message_Colon, WebResource.ProfitLoss_Add_RecordId, WebResource.Common_FullStop));
            }
            string parentPageTitle = string.Empty;
            string parentPageUrl = string.Empty;
            decimal allowance = 0;
            string unit = string.Empty;
            if (targetType.Equals(ProfitLossTargetType.Purchase.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                Purchase purchase = this.purchaseService.Selete(recordId);
                if (purchase.Category == PurchaseCategory.Product)
                {
                    parentPageTitle = WebResource.Purchase_Index_ProductTitle;
                    parentPageUrl = "/Purchase/Index/Product";
                }
                else
                {
                    parentPageTitle = WebResource.Purchase_Index_PackTitle;
                    parentPageUrl = "/Purchase/Index/Pack";
                }
                allowance = purchase.Allowance;
                unit = purchase.Unit;
            }
            ProfitLossAddModel model = new ProfitLossAddModel { RecordId = recordId, TargetType = targetType, ParentPageTitle = parentPageTitle, ParentPageUrl = parentPageUrl, Allowance = allowance, Unit = unit };
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

                #region 验证

                if (model == null)
                {
                    errorMessages.Add(WebResource.Message_ArgumentIsNull);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(model.RecordId))
                    {
                        errorMessages.Add(string.Format("{0}{1} {2}{3}", WebResource.Message_ArgumentIsNull, WebResource.Message_Colon, WebResource.ProfitLoss_Add_RecordId, WebResource.Common_FullStop));
                    }
                    if (string.IsNullOrWhiteSpace(model.TargetType))
                    {
                        errorMessages.Add(string.Format("{0}{1}", WebResource.ProfitLoss_Add_TargetTypeTip + WebResource.Common_FullStop));
                    }
                    if (string.IsNullOrWhiteSpace(model.Category))
                    {
                        errorMessages.Add(string.Format("{0}{1}", WebResource.ProfitLoss_Add_CategoryTip + WebResource.Common_FullStop));
                    }
                    if (model.Quantity <= 0)
                    {
                        errorMessages.Add(string.Format("{0}{1}", WebResource.Purchase_Add_QuantityTip + WebResource.Common_FullStop));
                    }
                    if (!string.IsNullOrWhiteSpace(model.Remark) && model.Remark.Trim().Length > 120)
                    {
                        errorMessages.Add(string.Format("{0}{1}", WebResource.Purchase_Add_RemarkTip + WebResource.Common_FullStop));
                    }
                }
                if (errorMessages.Count > 0)
                {
                    result.BuilderErrorMessage(errorMessages);
                    return Json(result);
                }

                #endregion
                ProfitLossTargetType targetType = (ProfitLossTargetType)Enum.Parse(typeof(ProfitLossTargetType), model.TargetType.Trim());
                ProfitLossCategory category = (ProfitLossCategory)Enum.Parse(typeof(ProfitLossCategory), model.Category.Trim());

                this.profitLossService.Add(model.RecordId.Trim(), targetType, category, model.Quantity, model.Remark, this.Session["Moblie"].ToString());
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
