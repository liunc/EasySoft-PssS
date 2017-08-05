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
namespace EasySoft.PssS.Web.Models.CustomerGroup
{
    using EasySoft.Core.ViewModel;

    /// <summary>
    /// 客户分组列表页面视图模型类
    /// </summary>
    public class CustomerGroupIndexModel : PagingModel<CustomerGroupPageModel>
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupIndexModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">当前页索引</param>
        public CustomerGroupIndexModel(int pageIndex) : base(pageIndex)
        {

        }

        #endregion
    }
}