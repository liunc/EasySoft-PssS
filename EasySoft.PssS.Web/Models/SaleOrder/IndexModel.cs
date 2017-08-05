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
    using EasySoft.Core.ViewModel;

    /// <summary>
    /// 销售订单首页视图模型类
    /// </summary>
    public class IndexModel : PagingModel<SaleOrderPageModel>
    {
        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public string QueryStatus { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        public IndexModel(int pageIndex) : base(pageIndex)
        {
        }
    }

}