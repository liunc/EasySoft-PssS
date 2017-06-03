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
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 采购首页视图模型类
    /// </summary>
    public class PurchaseIndexModel
    {
        /// <summary>
        /// 获取或设置页面标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置采购项
        /// </summary>
        public List<PurchaseItemModel> PurchaseItems { get; set; }
        
        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// 获取或设置显示的数据
        /// </summary>
        public List<PurchasePageModel> Data { get; set; }

        /// <summary>
        /// 获取或设置数据页数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 获取或设置当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseIndexModel()
        {
            this.Data = new List<PurchasePageModel>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category">采购分类</param>
        public PurchaseIndexModel(PurchaseCategory category) : this()
        {
            this.Init(category);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="category">采购分类</param>
        public void Init(PurchaseCategory category)
        {
            this.Category = category.ToString();
            if (category == PurchaseCategory.Product)
            {
                this.Title = WebResource.Title_Purchase_Product;
            }
            else if (category == PurchaseCategory.Pack)
            {
                this.Title = WebResource.Title_Purchase_Pack;
            }
            this.PurchaseItems = ParameterHelper.GetPurchaseItem(category, false).Values.ToList();

        }
    }

}