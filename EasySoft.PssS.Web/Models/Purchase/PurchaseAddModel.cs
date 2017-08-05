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
namespace EasySoft.PssS.Web.Models.Purchase
{
    using Core.Util;
    using Cost;
    using CostItem;
    using Domain.ValueObject;
    using PurchaseItem;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 新增采购记录视图模型类
    /// </summary>
    public class PurchaseAddModel
    {
        /// <summary>
        /// 获取或设置页面标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public string CurrentDate
        {
            get
            {
                return DataConvert.ConvertDateToString(DataConvert.ConvertNowUTCToBeijing());
            }
        }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置父级页面标题
        /// </summary>
        public string ParentPageTitle { get; set; }

        /// <summary>
        /// 获取或设置采购项
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
        /// 获取或设置供应方
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置成本明细
        /// </summary>
        public List<CostAddModel> Costs { get; set; }

        /// <summary>
        /// 获取或设置成本明细
        /// </summary>
        public Dictionary<string, decimal> CostData { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseAddModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category">采购分类</param>
        public PurchaseAddModel(string category)
        {
            this.Init(category);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="category">采购分类</param>
        public void Init(string category)
        {
            this.Category = category;
            if (category == PurchaseItemCategory.Product)
            {
                this.Title = WebResource.Title_Purchase_AddProduct;
                this.ParentPageTitle = WebResource.Title_Purchase_Product;
            }
            else if (category == PurchaseItemCategory.Pack)
            {
                this.Title = WebResource.Title_Purchase_AddPack;
                this.ParentPageTitle = WebResource.Title_Purchase_Pack;
            }

        }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            validate.CheckDictionary<string, string>(WebResource.Field_Category, this.Category, ParameterHelper.GetPurchaseItemCatetory());
            this.Date = validate.CheckDateString(WebResource.Field_Date, this.DateString, true);
            this.Supplier = validate.CheckInputString(WebResource.Field_Supplier, this.Supplier, false, Constant.STRING_LENGTH_10);
            validate.CheckDictionary<string, PurchaseItemCacheModel>(WebResource.Field_Item, this.Item, ParameterHelper.GetPurchaseItem(this.Category));
            validate.CheckDecimal(WebResource.Field_Quantity, this.Quantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
            this.Unit = validate.CheckInputString(WebResource.Field_Unit, this.Unit, false, Constant.STRING_LENGTH_2);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);
            this.CostData = new Dictionary<string, decimal>();
            if (this.Costs != null && this.Costs.Count > 0)
            {
                foreach (CostAddModel cost in this.Costs)
                {
                    cost.Category = CostItemCategory.Purchase;
                    cost.PostValidate(ref validate);
                    this.CostData.Add(cost.Item, cost.Money);
                }
            }
        }
    }
}