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
namespace EasySoft.PssS.Web.Models.Delivery
{
    using Core.Util;
    using EasySoft.Core.ViewModel;
    using System;

    /// <summary>
    /// 交付记录列表页面视图模型类
    /// </summary>
    public class DeliveryIndexModel : PagingModel<DeliveryPageModel>
    {
        #region 属性

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public string QueryStartDate { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public string QueryEndDate { get; set; }

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryIndexModel() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="pageIndex">当前页索引</param>
        public DeliveryIndexModel(string startDate, string endDate, int pageIndex) : base(pageIndex)
        {
            this.QueryStartDate = startDate;
            this.QueryEndDate = endDate;
            this.StartDate = DataConvert.ConvertStringToDate(startDate);
            this.EndDate = DataConvert.ConvertStringToDate(startDate);
            if(this.EndDate != DateTime.MinValue)
            {
                this.EndDate = this.EndDate.AddDays(1);
            }
        }

        #endregion
    }
}