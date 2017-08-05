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
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 采购首页视图模型类
    /// </summary>
    public class PurchaseIndexModel : PagingModel<PurchasePageModel>
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
        /// 获取或设置选中项
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// 获取或设置添加按钮显示文本
        /// </summary>
        public string AddButtonText { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseIndexModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="category">采购分类</param>
        public PurchaseIndexModel(string category, int pageIndex) : base(pageIndex)
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
                this.Title = WebResource.Title_Purchase_Product;
                this.AddButtonText = WebResource.Title_Purchase_AddProduct;
            }
            else if (category == PurchaseItemCategory.Pack)
            {
                this.Title = WebResource.Title_Purchase_Pack;
                this.AddButtonText = WebResource.Title_Purchase_AddPack;
            }

        }
    }

}