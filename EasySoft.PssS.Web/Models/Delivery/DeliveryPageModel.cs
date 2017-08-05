// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-05-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Delivery
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using Web.Resources;

    /// <summary>
    /// 交付数据视图模型类
    /// </summary>
    public class DeliveryPageModel 
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 获取或设置快递公司
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        /// 获取或设置快递公司
        /// </summary>
        public string ExpressCompanyName { get; set; }

        /// <summary>
        /// 获取或设置快递单号
        /// </summary>
        public string ExpressBill { get; set; }

        /// <summary>
        /// 获取或设置是否包含订单
        /// </summary>
        public string IncludeOrder { get; set; }

        /// <summary>
        /// 获取或设置摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 构造函数

        public DeliveryPageModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public DeliveryPageModel(Delivery entity)
        {
            this.Id = entity.Id;
            this.ExpressCompany = entity.ExpressCompany;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.ExpressBill = entity.ExpressBill;
            this.IncludeOrder = entity.IncludeOrder;
            this.Summary = entity.Summary;
            this.Remark = entity.Remark;
            if (string.IsNullOrWhiteSpace(this.Remark))
            {
                this.Remark = WebResource.Common_None;
            }
        }

        #endregion


    }
}
