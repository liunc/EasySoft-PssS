// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Delivery
{
    using Domain.ValueObject;
    using EasySoft.Core.Util;
    using EasySoft.PssS.Web.Resources;
    using PurchaseItem;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 交付明细视图模型类
    /// </summary>
    public class DeliveryDetailAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置采购Id
        /// </summary>
        public string PurchaseId { get; set; }

        /// <summary>
        /// 获取或设置采购项分类
        /// </summary>
        public string ItemCategory { get; set; }

        /// <summary>
        /// 获取或设置采购项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置采购项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 获取或设置交付数量
        /// </summary>
        public decimal DeliveryQuantity { get; set; }

        /// <summary>
        /// 获取或设置包装数量
        /// </summary>
        public decimal PackQuantity { get; set; }

        /// <summary>
        /// 获取或设置交付单位
        /// </summary>
        public string PackUnit { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            this.PurchaseId = validate.CheckInputString(WebResource.Field_PurchaseId, this.PurchaseId, false, Constant.STRING_LENGTH_32);
            validate.CheckDictionary<string, string>(WebResource.Field_PurchaseCategory, this.ItemCategory, ParameterHelper.GetPurchaseItemCatetory());
            validate.CheckDecimal(WebResource.Field_DeliveryQuantity, this.DeliveryQuantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
            Dictionary<string, PurchaseItemCacheModel> purchaseItems = ParameterHelper.GetPurchaseItem(this.ItemCategory, false);
            PurchaseItemCacheModel item = purchaseItems[this.Item];
            if (item == null)
            {
                validate.AddErrorMessage(string.Format(WebResource.Message_NoPurchaseItem, this.Item));
            }
            else
            {
                this.ItemName = item.Name;
                if (this.ItemCategory == PurchaseItemCategory.Product)
                {
                    validate.CheckDecimal(WebResource.Field_PackQuantity, this.PackQuantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
                    this.PackUnit = item.OutUnit;
                }
            }
        }

        #endregion
    }
}