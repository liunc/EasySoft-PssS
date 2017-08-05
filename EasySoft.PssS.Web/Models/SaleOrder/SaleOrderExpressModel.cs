// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using Web.Resources;
    using Core.Util;
    using PurchaseItem;

    /// <summary>
    /// 需要快递销售订单视图模型类
    /// </summary>
    public class SaleOrderExpressModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置供应商
        /// </summary>
        public string ItemName { get; private set; }

        /// <summary>
        /// 获取或设置交付数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Selected { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderExpressModel()
        {
        }
        
        #endregion

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            this.Id = validate.CheckInputString(WebResource.Field_OrderId, this.Id, true, Constant.STRING_LENGTH_32);
            PurchaseItemCacheModel item = validate.CheckDictionary<string, PurchaseItemCacheModel>(WebResource.Field_Product, this.Item, ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product, false));
            if (item != null)
            {
                this.ItemName = item.Name;
                this.Unit = item.OutUnit;
            }
            validate.CheckDecimal(WebResource.Field_Quantity, this.Quantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
        }
    }
}