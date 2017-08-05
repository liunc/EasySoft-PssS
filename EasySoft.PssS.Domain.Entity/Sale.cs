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
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// 销售领域实体类
    /// </summary>
    [Table("dbo.Sale")]
    public class Sale : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        [Column("DeliveryId", DbType.String, Size = Constant.STRING_LENGTH_32)]
        public string DeliveryId { get; private set; }

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
        /// 获取或设置余量
        /// </summary>
        [Column("Inventory", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Inventory { get; private set; }

        /// <summary>
        /// 获取或设置益损
        /// </summary>
        [Column("ProfitLoss", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal ProfitLoss { get; private set; }
        
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
        public Sale()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="deliveryId">采购单ID</param>
        /// <param name="item">项</param>
        /// <param name="quantity">数量</param>  
        /// <param name="unit">单位</param>
        /// <param name="inventory">余量</param>
        /// <param name="profitLoss">益损</param>
        /// <param name="status">状态</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public Sale(string id, string deliveryId, string item, decimal quantity, string unit, decimal inventory, decimal profitLoss, string status, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.DeliveryId = deliveryId;
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.Inventory = inventory;
            this.ProfitLoss = profitLoss;
            this.Status = status;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 采购出库到销售
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
        public void Create(string deliveryId, string item, decimal quantity, string unit, string creator)
        {
            base.Create(creator);
            this.DeliveryId = deliveryId;
            this.Item = item;
            this.Quantity = quantity;
            this.Unit = unit;
            this.Inventory = quantity;
            this.Status = SaleStatus.Sent;
        }

        /// <summary>
        /// 增加数量
        /// </summary>
        /// <param name="quantity"></param>
        public void AddQuantity(decimal quantity)
        {
            this.Quantity += quantity;
        }

        /// <summary>
        /// 增加益损数据
        /// </summary>
        /// <param name="category">益损类型</param>
        /// <param name="quantity">数量</param>
        public void AddProfitLoss(string category, decimal quantity)
        {
            if (this.Status != SaleStatus.Received)
            {
                throw new EasySoftException("当前状态不能进行益损操作。");
            }

            decimal profitLoss = Entity.ProfitLoss.Calculate(category, quantity);
            
            this.Inventory += profitLoss;
            this.ProfitLoss += profitLoss;

            if (this.Inventory == Constant.DECIMAL_ZERO)
            {
                this.Status = SaleStatus.Finished;
            }
        }

        #endregion
    }
}
