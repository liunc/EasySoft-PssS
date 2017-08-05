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
namespace EasySoft.PssS.Web.Models.Customer
{
    using EasySoft.Core.ViewModel;

    /// <summary>
    /// 客户分组列表页面视图模型类
    /// </summary>
    public class CustomerIndexModel : PagingModel<CustomerPageModel>
    {
        #region 属性

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public string QueryGroupId { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerIndexModel() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="groupId">分组ID</param>
        /// <param name="pageIndex">当前页索引</param>
        public CustomerIndexModel(string name, string groupId, int pageIndex) : base(pageIndex)
        {
            this.QueryName = name;
            this.QueryGroupId = groupId;
        }

        #endregion
    }
}