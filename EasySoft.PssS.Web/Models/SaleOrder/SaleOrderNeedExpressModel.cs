// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using Web.Resources;
    using Core.Util;

    /// <summary>
    /// 需要快递销售订单视图模型类
    /// </summary>
    public class SaleOrderNeedExpressModel
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
        /// 获取或设置地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置供应商
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置是否选中
        /// </summary>
        public string Selected { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderNeedExpressModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public SaleOrderNeedExpressModel(SaleOrder entity)
        {
            this.Id = entity.Id;
            this.Item = entity.Item;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.Address = entity.Address;
            this.Mobile = entity.Mobile;
            this.Linkman = entity.Linkman;
            this.Quantity = entity.Quantity;
            this.Unit = entity.Unit;
        }

        #endregion
    }
}