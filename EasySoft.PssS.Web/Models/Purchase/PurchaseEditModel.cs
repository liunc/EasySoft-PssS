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
    using Resources;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using Util;
    using Domain.ValueObject;
    using System.Linq;

    /// <summary>
    /// 修改采购记录视图模型类
    /// </summary>
    public class PurchaseEditModel
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置页面标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置父级页面标题
        /// </summary>
        public string ParentPageTitle { get; set; }

        /// <summary>
        /// 获取或设置采购项名称
        /// </summary>
        public string ItemName { get; set; }

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
        public List<CostModel> Costs { get; set; }

        /// <summary>
        /// 获取或设置采购项
        /// </summary>
        public List<PurchaseItemModel> PurchaseItems { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseEditModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public PurchaseEditModel(Purchase entity)
        {
            this.Id = entity.Id;
            this.Category = entity.Category.ToString();
            this.ParentPageTitle = WebResource.Purchase_Index_ProductTitle;
            if (entity.Category == PurchaseCategory.Pack)
            {
                this.ParentPageTitle = WebResource.Purchase_Index_PackTitle;
            }
            this.Title = string.Format("{0}{1}", WebResource.Button_Edit, this.ParentPageTitle);

            this.ItemName = entity.Item;
            Dictionary<string, PurchaseItemModel> items = ParameterHelper.GetPurchaseItem(this.Category, false);
            PurchaseItemModel query = items[entity.Item];
            if (query != null)
            {
                this.ItemName =query.Name;
            }

            this.Date = DateTimeUtil.ConvertDateToString(entity.Date);
            this.Unit = entity.Unit;
            this.Quantity = entity.Quantity;
            this.Supplier = entity.Supplier;
            this.Remark = entity.Remark;
            if (string.IsNullOrWhiteSpace(this.Remark))
            {
                this.Remark = WebResource.Common_None;
            }
            this.Costs = new List<CostModel>();
            Dictionary<string, CostItemModel> costItems = ParameterHelper.GetCostItem(CostCategory.IntoDepot.ToString(), false);
            foreach (Cost cost in entity.Costs)
            {
                string itemName = entity.Item;
                CostItemModel query1 = costItems[cost.Item];
                if (query1 != null)
                {
                    itemName = query1.ItemName;
                }
                this.Costs.Add(new CostModel { Id = cost.Id, ItemCode = cost.Item, ItemName = itemName, Money = cost.Money });

            }

        }
    }
}