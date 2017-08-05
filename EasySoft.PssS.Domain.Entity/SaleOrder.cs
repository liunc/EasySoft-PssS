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
namespace EasySoft.PssS.Domain.Entity
{
    using EasySoft.Core.Persistence;
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using System;
    using System.Data;

    /// <summary>
    /// 采购领域实体类
    /// </summary>
    [Table("dbo.SaleOrder")]
    public class SaleOrder : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        [Column("Date", DbType.DateTime, AllowEdit = false)]
        public DateTime Date { get; private set; }

        /// <summary>
        /// 获取或设置客户ID
        /// </summary>
        [Column("CustomerId", DbType.String, AllowEdit = false, Size = Constant.STRING_LENGTH_32)]
        public string CustomerId { get; private set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        [Column(Name = "Address", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Address { get; private set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        [Column(Name = "Mobile", DataType = DbType.String, Size = Constant.STRING_LENGTH_16)]
        public string Mobile { get; private set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [Column(Name = "Linkman", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Linkman { get; private set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        [Column("Item", DbType.String, AllowEdit = false, Size = Constant.STRING_LENGTH_10)]
        public string Item { get; private set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        [Column("Quantity", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Quantity { get; private set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        [Column("Unit", DbType.String, AllowEdit = false, Size = Constant.STRING_LENGTH_2)]
        public string Unit { get; private set; }

        /// <summary>
        /// 获取或设置是否需要快递
        /// </summary>
        [Column("NeedExpress", DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string NeedExpress { get; private set; }

        /// <summary>
        /// 获取或设置记录ID
        /// </summary>
        [Column("RecordId", DbType.String, Size = Constant.STRING_LENGTH_32)]
        public string RecordId { get; private set; }

        /// <summary>
        /// 获取或设置单价
        /// </summary>
        [Column("Price", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Price { get; private set; }

        /// <summary>
        /// 获取或设置实际总价
        /// </summary>
        [Column("ActualAmount", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal ActualAmount { get; private set; }

        /// <summary>
        /// 获取或设置折扣
        /// </summary>
        [Column("Discount", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Discount { get; private set; }

        /// <summary>
        /// 获取或设置状态
        /// </summary>
        [Column("Status", DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string Status { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrder()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="date">日期</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="address">地址</param>
        ///  <param name="mobile">手机号</param>
        ///  <param name="linkman">联系人</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>  
        /// <param name="unit">单位</param>
        /// <param name="needExpress">是否需要快递</param>
        /// <param name="recordId">记录ID</param>
        /// <param name="price">单价</param>
        /// <param name="actualAmount">总价</param>
        /// <param name="discount">折扣</param>
        /// <param name="status">状态</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public SaleOrder(string id, DateTime date, string customerId, string address, string mobile, string linkman, string item, decimal quantity, string unit, string needExpress, string recordId, decimal price, decimal actualAmount, decimal discount, string status, string remark, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.Date = date;
            this.CustomerId = customerId;
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.NeedExpress = needExpress;
            this.RecordId = recordId;
            this.Price = price;
            this.ActualAmount = actualAmount;
            this.Discount = discount;
            this.Status = status;
            this.Remark = remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="address">地址</param>
        ///  <param name="mobile">手机号</param>
        ///  <param name="linkman">联系人</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>  
        /// <param name="unit">单位</param>
        /// <param name="needExpress">是否需要快递</param>
        /// <param name="recordId">记录ID</param>
        /// <param name="price">单价</param>
        /// <param name="actualAmount">总价</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Create(DateTime date, string customerId, string address, string mobile, string linkman, string item, decimal quantity, string unit, string needExpress, decimal price, decimal actualAmount, string remark, string creator)
        {
            base.Create(creator);
            this.Date = date;
            this.CustomerId = customerId;
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.NeedExpress = needExpress;
            this.RecordId = string.Empty;
            this.Price = price;
            this.ActualAmount = actualAmount;
            this.Discount = 0;
            this.Status = SaleOrderStatus.Ordered;
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
        }

        /// <summary>
        /// 发送快递
        /// </summary>
        /// <param name="recordId">出库单号</param>
        /// <param name="mender">出库人</param>
        public void SentExpress(string recordId, string mender)
        {
            this.Update(mender);
            this.RecordId = recordId;
            this.Status = SaleOrderStatus.Sent;
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="actualAmount">已收款</param>
        /// <param name="mender">修改人</param>
        public void Update(string status, decimal actualAmount, string mender)
        {
            this.Update(mender);
            this.ActualAmount = actualAmount;
            this.Status = status;
            decimal amount = this.Price * this.Quantity;
            if(amount > this.ActualAmount)
            {
                this.Discount = amount - this.ActualAmount;
            }
        }

        #endregion
    }
}
