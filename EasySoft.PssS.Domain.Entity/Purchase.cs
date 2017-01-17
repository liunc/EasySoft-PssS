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
    using EasySoft.PssS.Domain.ValueObject;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 采购领域实体类
    /// </summary>
    public class Purchase : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        public PurchaseCategory Category { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置供应方
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置成本
        /// </summary>
        public List<Cost> Costs { get; set; }

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
        /// 新增采购记录
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
        public void Add(DateTime date, string category, string item, decimal quantity, string unit, string supplier, string remark, Dictionary<string, decimal> costs, string creator)
        {
            this.NewId();
            this.Date = date;
            this.Category = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), category);
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.Supplier = supplier;
            this.Allowance = quantity;
            this.Remark = remark;
            this.Creator = new Operator(creator);
            this.Mender = this.Creator;
            foreach(KeyValuePair<string, decimal> cost in costs)
            {
                Cost cost1 = new Cost();
                cost1.Add(this.Id, CostCategory.PurchaseInput, cost.Key, cost.Value);
                this.Costs.Add(cost1);
            }
        }

        #endregion
    }
}
