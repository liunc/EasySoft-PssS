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

using System.Collections.Generic;

namespace EasySoft.PssS.Web.Models
{
    /// <summary>
    /// 带分页的视图模型基类
    /// </summary>
    public class PagingModel<T>
    {
        #region 属性

        /// <summary>
        /// 获取或设置数据页数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 获取或设置当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置分页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 获取或设置分页数据
        /// </summary>
        public List<T> PageData { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PagingModel()
        {
            this.PageSize = ParameterHelper.GetPageSize();
        }
        
        #endregion
    }
}