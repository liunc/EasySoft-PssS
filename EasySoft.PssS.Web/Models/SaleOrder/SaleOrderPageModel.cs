// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-07-29
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using Resources;

    /// <summary>
    /// 销售订单分页视图模型类
    /// </summary>
    public class SaleOrderPageModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public string Date { get;  set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public string Address { get;  set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get;  set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string Linkman { get;  set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get;  set; }

        /// <summary>
        /// 获取或设置项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get;  set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get;  set; }

        /// <summary>
        /// 获取或设置是否需要快递
        /// </summary>
        public string NeedExpress { get;  set; }

        /// <summary>
        /// 获取或设置单价
        /// </summary>
        public decimal Price { get;  set; }

        /// <summary>
        /// 获取或设置实际总价
        /// </summary>
        public decimal ActualAmount { get;  set; }

        /// <summary>
        /// 获取或设置折扣
        /// </summary>
        public decimal Discount { get;  set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        public string Status { get;  set; }

        /// <summary>
        /// 获取或设置状态名称
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get;  set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderPageModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public SaleOrderPageModel(SaleOrder entity)
        {
            this.Id = entity.Id;
            this.Item = entity.Item;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.Address = entity.Address;
            this.Mobile = entity.Mobile;
            this.Linkman = entity.Linkman;
            this.Unit = entity.Unit;
            this.Quantity = entity.Quantity;
            this.NeedExpress = entity.NeedExpress;
            this.Price = entity.Price;
            this.ActualAmount = entity.ActualAmount;
            this.Discount = entity.Discount;
            this.Status = entity.Status;
            this.Remark = entity.Remark;
        }

        #endregion
    }
}