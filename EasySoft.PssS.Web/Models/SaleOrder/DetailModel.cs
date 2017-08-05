
// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-08-03
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.PurchaseItem;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Entity = EasySoft.PssS.Domain.Entity;
    /// <summary>
    /// 订单明细视图模型类
    /// </summary>
    public class DetailModel
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置选中项
        /// </summary>
        public string SelectedItem { get; set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        public string QueryStatus { get; set; }

        /// <summary>
        /// 获取或设置当前索引
        /// </summary>
        public int PageIndex { get; set; }
        
        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string ItemName { get; set; }

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
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置已付款金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置记录ID
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置折扣
        /// </summary>
        public decimal Discount { get; private set; }

        /// <summary>
        /// 获取或设置是否需要快递
        /// </summary>
        public bool NeedExpress { get; set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DetailModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="item">选中项</param>
        /// <param name="page">当前索引</param>
        public DetailModel(Entity.SaleOrder entity, string item, string status, int page)
        {
            this.Id = entity.Id;
            this.Item = entity.Item;
            this.Address = entity.Address;
            this.Mobile = entity.Mobile;
            this.Linkman = entity.Linkman;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.Unit = entity.Unit;
            this.Price = entity.Price;
            this.Quantity = entity.Quantity;
            this.Remark = entity.Remark;
            this.RecordId = entity.RecordId;
            this.Discount = entity.Discount;
            this.ActualAmount = entity.ActualAmount;
            this.Status = entity.Status;
            this.QueryStatus = status;
            if (string.IsNullOrWhiteSpace(this.Remark))
            {
                this.Remark = WebResource.Common_None;
            }
            
            this.SelectedItem = item;
            this.PageIndex = page;
        }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="validate">返回验证对象</param>
        public void PostValidate(ref Validate validate)
        {
            this.Id = validate.CheckInputString(WebResource.Field_Id, this.Id, true, Constant.STRING_LENGTH_32);
            validate.CheckDictionary<string, string>(WebResource.Field_Status, this.Status, ParameterHelper.GetSaleOrderStatus());
            validate.CheckDecimal(WebResource.Field_ActualAmount, this.ActualAmount, Constant.DECIMAL_ZERO, Constant.DECIMAL_MAX);
        }

    }
}