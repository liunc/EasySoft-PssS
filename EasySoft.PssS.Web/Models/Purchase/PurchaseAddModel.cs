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
    using Domain.ValueObject;
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
        public string CategoryString { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public PurchaseCategory Category { get; set; }

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
        public List<CostItemModel> CostItems { get; set; }

        /// <summary>
        /// 获取或设置采购项
        /// </summary>
        public List<PurchaseItemModel> PurchaseItems { get; set; }

        /// <summary>
        /// 获取或设置成本明细
        /// </summary>
        public Dictionary<string,decimal> Costs { get; set; }

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
        public PurchaseAddModel(PurchaseCategory category)
        {
            this.Init(category);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="category">采购分类</param>
        public void Init(PurchaseCategory category)
        {
            this.CategoryString = category.ToString();
            if (category == PurchaseCategory.Product)
            {
                this.Title = WebResource.Title_Purchase_AddProduct;
                this.ParentPageTitle = WebResource.Title_Purchase_Product;
            }
            else if (category == PurchaseCategory.Pack)
            {
                this.Title = WebResource.Title_Purchase_AddPack;
                this.ParentPageTitle = WebResource.Title_Purchase_Pack;
            }

            Dictionary<string, PurchaseItemModel> purchaseItems = ParameterHelper.GetPurchaseItem(category, true);
            if (purchaseItems != null && purchaseItems.Count > 0)
            {
                this.PurchaseItems = purchaseItems.Values.ToList();
            }

            Dictionary<string, CostItemModel> costItems = ParameterHelper.GetCostItem(CostCategory.Purchase, true);
            if (costItems != null && costItems.Count > 0)
            {
                this.CostItems = costItems.Values.ToList();
            }

        }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref List<string> errorMessages)
        {
            this.Category = ValidateHelper.CheckPurchaseCategory(this.CategoryString, ref errorMessages);
            this.Date = ValidateHelper.CheckDateString(WebResource.Field_Date, this.DateString, true, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Supplier, this.Supplier, false, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            ValidateHelper.CheckSelectString(WebResource.Field_Item, this.Item, true, ParameterHelper.GetPurchaseItem(this.Category, true).Keys.ToList(), ref errorMessages);
            ValidateHelper.CheckDecimal(WebResource.Field_Quantity, this.Quantity, ValidateHelper.DECIMAL_MIN, ValidateHelper.DECIMAL_MAX, ref errorMessages);
            Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
            if (this.CostItems != null && this.CostItems.Count > 0)
            {
                List<string> costOptions = ParameterHelper.GetCostItem(CostCategory.Purchase.ToString(), true).Keys.ToList();
                foreach (CostItemModel cost in this.CostItems)
                {
                    if (!ValidateHelper.CheckSelectString(WebResource.Field_CostItem, cost.ItemCode, true, costOptions, ref errorMessages))
                    {
                        break;
                    }
                    if (!ValidateHelper.CheckDecimal(WebResource.Field_CostItemMoney, cost.Money, ValidateHelper.DECIMAL_ZERO, ValidateHelper.DECIMAL_MAX, ref errorMessages))
                    {
                        break;
                    }
                    costs.Add(cost.ItemCode, cost.Money);
                }
            }
            this.Costs = costs;
        }
    }
}