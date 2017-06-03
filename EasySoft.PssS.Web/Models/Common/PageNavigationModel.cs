// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-03-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Common
{
    /// <summary>
    /// 分页导航条视图模型类
    /// </summary>
    public class PageNavigationModel
    {
        private int pageIndex;

        /// <summary>
        /// 获取或设置当前页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (this.pageIndex > this.PageCount)
                {
                    this.pageIndex = this.PageCount;
                }
                return this.pageIndex;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        /// <summary>
        /// 获取分页数量
        /// </summary>
        public int PageCount
        {
            get
            {
                if(this.TotalCount == 0)
                {
                    return 0;
                }
                int pageCount = this.TotalCount / ParameterHelper.GetPageSize();
                if (this.TotalCount % ParameterHelper.GetPageSize() > 0)
                {
                    pageCount++;
                }
                return pageCount;
            }
        }

        /// <summary>
        /// 获取或设置查询记录总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 获取或设置页面Url
        /// </summary>
        public string Url { get; set; }

    }
}