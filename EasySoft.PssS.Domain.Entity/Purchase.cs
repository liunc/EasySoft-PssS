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
    using Core.Persistence;
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
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        [Column("Category", DbType.String, Size =20)]
        public PurchaseCategory Category { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        [Column("Item", DbType.String, Size = 20)]
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        [Column("Quantity", DbType.Decimal, Size = 18)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        [Column("Unit", DbType.String, Size = 5)]
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置供应方
        /// </summary>
        [Column("Supplier", DbType.String, Size = 50)]
        public string Supplier { get; set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        [Column("Allowance", DbType.Decimal, Size = 18)]
        public decimal Allowance { get; set; }

        /// <summary>
        /// 获取或设置成本汇总
        /// </summary>
        [Column("Cost", DbType.Decimal, Size = 18)]
        public decimal Cost { get; set; }

        /// <summary>
        /// 获取或设置益损
        /// </summary>
        [Column("ProfitLoss", DbType.Decimal, Size = 18)]
        public decimal ProfitLoss { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column("Remark", DbType.String, Size = 120)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置采购单状态
        /// </summary>
        [Column("Status", DbType.Int16)]
        public PurchaseStatus Status { get; set; }

        /// <summary>
        /// 获取或设置成本集合
        /// </summary>
        public List<Cost> Costs { get; set; }

        /// <summary>
        /// 获取或设置益损集合
        /// </summary>
        public List<ProfitLoss> ProfitLosss { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Purchase()
        {
            this.Costs = new List<Cost>();
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
        public void IntoDepot(DateTime date, PurchaseCategory category, string item, decimal quantity, string unit, string supplier, string remark, Dictionary<string, decimal> costs, string creator)
        {
            this.NewId();
            this.Date = date;
            this.Category = category;
            this.Item = string.IsNullOrWhiteSpace(item) ? string.Empty : item.Trim();
            this.Quantity = quantity;
            this.Unit = string.IsNullOrWhiteSpace(unit) ? string.Empty : unit.Trim();
            this.Supplier = string.IsNullOrWhiteSpace(supplier) ? string.Empty : supplier.Trim();
            this.Allowance = quantity;
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
            this.Status = PurchaseStatus.None;
            this.SetCreator(creator);
            this.SetMender(creator);
            foreach (KeyValuePair<string, decimal> cost in costs)
            {
                Cost cost1 = new Cost();
                cost1.Add(this.Id, CostCategory.Purchase, cost.Key, cost.Value);
                this.Cost += cost.Value;
                this.Costs.Add(cost1);
            }
        }

        /// <summary>
        /// 更新采购记录
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="quantity">数量</param>
        /// <param name="supplier">供应方</param>
        /// <param name="remark">备注</param>
        /// <param name="cost">成本</param>
        /// <param name="mender">修改人</param>
        public void Update(DateTime date, decimal quantity, string supplier, string remark, decimal cost, string mender)
        {
            this.Date = date;
            this.Quantity = quantity;
            this.Allowance = quantity;
            this.Supplier = string.IsNullOrWhiteSpace(supplier) ? string.Empty : supplier.Trim();
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
            this.Cost = cost;
            this.SetMender(mender);
        }

        #endregion
    }
}
