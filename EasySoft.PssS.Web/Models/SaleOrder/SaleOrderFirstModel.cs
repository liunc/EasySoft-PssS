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
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using Core.Util;
    using Domain.ValueObject;
    using EasySoft.Core.ViewModel;
    using PurchaseItem;
    using Resources;
    using System;

    /// <summary>
    /// 销售订单首页视图模型类
    /// </summary>
    public class SaleOrderFirstModel : SaleOrderAddModel
    {
        private string currentDate;

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public string CurrentDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.currentDate))
                {
                    this.currentDate = DataConvert.ConvertDateToString(DataConvert.ConvertNowUTCToBeijing());
                }
                return this.currentDate;
            }
        }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderFirstModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        public SaleOrderFirstModel(string id, string linkMan) : base(id, linkMan)
        {

        }

        #region 方法

        /// <summary>
        /// 生成新Id
        /// </summary>
        public void NewId()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            this.Date = validate.CheckDateString(WebResource.Field_Date, this.DateString, true);
            PurchaseItemCacheModel item = validate.CheckDictionary<string, PurchaseItemCacheModel>(WebResource.Field_Product, this.Item, ParameterHelper.GetPurchaseItem(PurchaseItemCategory.Product));
            if(item != null)
            {
                this.Unit = item.OutUnit;
                this.Price = item.Price;
            }
            validate.CheckDecimal(WebResource.Field_Quantity, this.Quantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);
           
        }
        #endregion
    }

}