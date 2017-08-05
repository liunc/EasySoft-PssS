﻿// ----------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// 采购领域实体类
    /// </summary>
    [Table("dbo.Purchase")]
    public class Purchase : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        [Column("Date", DbType.DateTime)]
        public DateTime Date { get; private set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        [Column("Category", DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string Category { get; private set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        [Column("Item", DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Item { get; private set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        [Column("Quantity", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Quantity { get; private set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        [Column("Unit", DbType.String, Size = Constant.STRING_LENGTH_2)]
        public string Unit { get; private set; }

        /// <summary>
        /// 获取或设置供应方
        /// </summary>
        [Column("Supplier", DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Supplier { get; private set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        [Column("Inventory", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Inventory { get; private set; }

        /// <summary>
        /// 获取或设置成本汇总
        /// </summary>
        [Column("Cost", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Cost { get; private set; }

        /// <summary>
        /// 获取或设置益损
        /// </summary>
        [Column("ProfitLoss", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal ProfitLoss { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column("Remark", DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; private set; }

        /// <summary>
        /// 获取或设置采购单状态
        /// </summary>
        [Column("Status", DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string Status { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Purchase()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="date">日期</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>  
        /// <param name="unit">单位</param>
        /// <param name="supplier">供应方</param>
        /// <param name="inventory">余量</param>
        /// <param name="cost">成本汇总</param>
        /// <param name="profitLoss">益损</param>
        /// <param name="remark">备注</param>
        /// <param name="status">状态</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public Purchase(string id, DateTime date, string category, string item, decimal quantity, string unit, string supplier, decimal inventory, decimal cost, decimal profitLoss, string remark, string status, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.Date = date;
            this.Category = category;
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.Supplier = supplier;
            this.Inventory = inventory;
            this.Cost = cost;
            this.ProfitLoss = profitLoss;
            this.Remark = remark;
            this.Status = status;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 采购入库
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>
        /// <param name="unit">单位</param>
        /// <param name="supplier">供应方</param>
        /// <param name="remark">备注</param>
        /// <param name="costs">成本</param>
        /// <param name="creator">创建人</param>
        public void Create(DateTime date, string category, string item, decimal quantity, string unit, string supplier, string remark, string creator)
        {
            base.Create(creator);
            this.Date = date;
            this.Category = category;
            this.Item = string.IsNullOrWhiteSpace(item) ? string.Empty : item.Trim();
            this.Quantity = quantity;
            this.Unit = string.IsNullOrWhiteSpace(unit) ? string.Empty : unit.Trim();
            this.Supplier = string.IsNullOrWhiteSpace(supplier) ? string.Empty : supplier.Trim();
            this.Inventory = quantity;
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
            this.Status = PurchaseStatus.None;
            
        }

        /// <summary>
        /// 增加益损数据
        /// </summary>
        /// <param name="category">益损类型</param>
        /// <param name="quantity">数量</param>
        public void AddProfitLoss(string category, decimal quantity)
        {
            if (this.Status == PurchaseStatus.None)
            {
                this.Status = PurchaseStatus.Processing;
            }

            decimal profitLoss = Entity.ProfitLoss.Calculate(category, quantity);
            
            this.Inventory += profitLoss;
            this.ProfitLoss += profitLoss;

            this.SetFinished();
        }

        public void Delivery(decimal quantity, string mender)
        {
            this.Update(mender);
            this.Inventory -= quantity;

            this.SetFinished();
        }

        /// <summary>
        /// 当前库存为零时，设置为完成状态
        /// </summary>
        private void SetFinished()
        {
            if (this.Inventory == Constant.DECIMAL_ZERO)
            {
                this.Status = PurchaseStatus.Finished;
            }
        }

        /// <summary>
        /// 添加成本
        /// </summary>
        /// <param name="cost">成本值</param>
        public void AddCost(decimal cost)
        {
            this.Cost += cost;
        }

        #endregion
    }
}
