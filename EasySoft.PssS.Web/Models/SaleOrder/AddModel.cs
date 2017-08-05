
// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-08-03
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.PurchaseItem;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// 新增订单_明细视图模型类
    /// </summary>
    public class AddModel
    {
        /// <summary>
        /// 获取或设置客户地址Id
        /// </summary>
        public string AddressId { get; set; }

        /// <summary>
        /// 获取或设置查询条件客户分组Id
        /// </summary>
        public string QueryGroupId { get; set; }

        /// <summary>
        /// 获取或设置查询条件客户名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置已付款金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置是否需要快递
        /// </summary>
        public bool NeedExpress { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="addressId">客户地址Id</param>
        /// <param name="groupId">查询条件客户分组Id</param>
        /// <param name="name">查询条件客户名称</param>
        public AddModel(string addressId, string groupId, string name)
        {
            this.AddressId = addressId;
            this.QueryGroupId = groupId;
            this.QueryName = name;
            this.DateString = DataConvert.ConvertDateToString(DataConvert.ConvertNowUTCToBeijing());
        }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="validate">返回验证对象</param>
        public void PostValidate(ref Validate validate)
        {
            this.AddressId = validate.CheckInputString(WebResource.Field_Address, this.AddressId, true, Constant.STRING_LENGTH_32);
            this.Date = validate.CheckDateString(WebResource.Field_Date, this.DateString, true);
            PurchaseItemCacheModel item = validate.CheckDictionary<string, PurchaseItemCacheModel>(WebResource.Field_Product, this.Item, ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product));
            if (item != null)
            {
                this.Unit = item.OutUnit;
                this.Price = item.Price;
            }
            validate.CheckDecimal(WebResource.Field_Quantity, this.Quantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
            validate.CheckDecimal(WebResource.Field_ActualAmount, this.ActualAmount, Constant.DECIMAL_ZERO, Constant.DECIMAL_MAX);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);
        }
    }
}