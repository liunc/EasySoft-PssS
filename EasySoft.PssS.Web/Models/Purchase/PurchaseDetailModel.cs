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
namespace EasySoft.PssS.Web.Models.Purchase
{
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using Web.Resources;
    using System.Linq;
    using Core.Util;

    /// <summary>
    /// 采购视图模型类
    /// </summary>
    public class PurchaseDetailModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置页面标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public PurchaseCategory Category { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public string CategoryString { get; set; }

        /// <summary>
        /// 获取或设置父级页面标题
        /// </summary>
        public string ParentPageTitle { get; set; }

        /// <summary>
        /// 获取或设置项
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
        /// 获取或设置余量
        /// </summary>
        public decimal Inventory { get; set; }

        /// <summary>
        /// 获取或设置成本汇总
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 获取或设置益损
        /// </summary>
        public decimal ProfitLoss { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置采购单状态
        /// </summary>
        public PurchaseStatus Status { get; set; }

        /// <summary>
        /// 获取或设置父页面选中项
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// 获取或设置父页面当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseDetailModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public PurchaseDetailModel(Purchase entity, string item, int page)
        {
            this.Id = entity.Id;
            this.Category = entity.Category;
            this.CategoryString = entity.Category.ToString();
            this.ParentPageTitle = WebResource.Title_Purchase_Product;
            this.SelectedItem = item;
            this.PageIndex = page;
            if (entity.Category == PurchaseCategory.Pack)
            {
                this.ParentPageTitle = WebResource.Title_Purchase_Pack;
            }
            this.Item = entity.Item;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.Unit = entity.Unit;
            this.Quantity = entity.Quantity;
            this.ProfitLoss = entity.ProfitLoss;
            this.Supplier = entity.Supplier;
            this.Remark = entity.Remark;
            this.Status = entity.Status;
            if (string.IsNullOrWhiteSpace(this.Remark))
            {
                this.Remark = WebResource.Common_None;
            }
            this.Inventory = entity.Inventory;
            this.Cost = entity.Cost;

        }

        #endregion

    }
}
