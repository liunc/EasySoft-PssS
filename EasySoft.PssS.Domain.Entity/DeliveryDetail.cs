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
namespace EasySoft.PssS.Domain.Entity
{
    using EasySoft.Core.Persistence;
    using EasySoft.Core.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using ValueObject;

    /// <summary>
    /// 交付明细领域实体类
    /// </summary>
    [Table("dbo.DeliveryDetail")]
    public class DeliveryDetail : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置交付Id
        /// </summary>
        [Column(Name = "DeliveryId", DataType = DbType.String, Size = Constant.STRING_LENGTH_32)]
        public string DeliveryId { get; set; }

        /// <summary>
        /// 获取或设置采购Id
        /// </summary>
        [Column(Name = "PurchaseId", DataType = DbType.String, Size = Constant.STRING_LENGTH_32)]
        public string PurchaseId { get; set; }

        /// <summary>
        /// 获取或设置采购项类型
        /// </summary>
        [Column(Name = "PurchaseCategory", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string PurchaseCategory { get; set; }

        /// <summary>
        /// 获取或设置交付数量
        /// </summary>
        [Column(Name = "DeliveryQuantity", DataType = DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal DeliveryQuantity { get; set; }

        /// <summary>
        /// 获取或设置包装数量
        /// </summary>
        [Column(Name = "PackQuantity", DataType = DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal PackQuantity { get; set; }

        /// <summary>
        /// 获取或设置交付单位
        /// </summary>
        [Column(Name = "PackUnit", DataType = DbType.String, Size = Constant.STRING_LENGTH_2)]
        public string PackUnit { get; set; }

        /// <summary>
        /// 获取或设置创建者
        /// </summary>
        [Column(Name = "Creator", DataType = DbType.String, Size = Constant.STRING_LENGTH_16, AllowEdit = false)]
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [Column(Name = "CreateTime", DataType = DbType.DateTime, AllowEdit = false)]
        public DateTime CreateTime { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryDetail()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="deliveryId">交付ID</param>
        /// <param name="purchaseId">采购ID</param>
        /// <param name="purchaseCategory">采购分类</param>
        /// <param name="deliveryQuantity">交付数量</param>
        /// <param name="packQuantity">包装数量</param>
        /// <param name="packUnit">包装单位</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        public DeliveryDetail(string id, string deliveryId, string purchaseId, string purchaseCategory, decimal deliveryQuantity, decimal packQuantity, string packUnit, string creator, DateTime createTime) : base(id)
        {
            this.DeliveryId = deliveryId;
            this.PurchaseId = purchaseId;
            this.PurchaseCategory = purchaseCategory;
            this.DeliveryQuantity = deliveryQuantity;
            this.PackQuantity = packQuantity;
            this.PackUnit = packUnit;
            this.Creator = creator;
            this.CreateTime = createTime;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="deliveryId">交付ID</param>
        /// <param name="purchaseId">采购ID</param>
        /// <param name="purchaseCategory">采购分类</param>
        /// <param name="deliveryQuantity">交付数量</param>
        /// <param name="packQuantity">包装数量</param>
        /// <param name="packUnit">包装单位</param>
        /// <param name="creator">创建人</param>
        public void Create(string deliveryId, string purchaseId, string purchaseCategory, decimal deliveryQuantity, decimal packQuantity, string packUnit, string creator)
        {
            this.NewId();
            this.DeliveryId = deliveryId;
            this.PurchaseId = purchaseId;
            this.PurchaseCategory = purchaseCategory;
            this.DeliveryQuantity = deliveryQuantity;
            if (this.PurchaseCategory == PurchaseItemCategory.Product)
            {
                this.PackQuantity = packQuantity;
                this.PackUnit = packUnit;
            }
            else
            {
                this.PackQuantity = Constant.DECIMAL_ZERO;
                this.PackUnit = string.Empty;
            }
            this.Creator = creator;
            this.CreateTime = DataConvert.ConvertUTCToBeijing(DateTime.UtcNow);
        }


        #endregion
    }
}
