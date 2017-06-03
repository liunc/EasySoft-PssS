// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-02-07
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
    using Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using System;
    using System.Data;

    /// <summary>
    /// 益损领域实体类
    /// </summary>
    [Table("dbo.ProfitLoss")]
    public class ProfitLoss : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置关联Id
        /// </summary>
        [Column(Name = "RecordId", DataType = DbType.String, Size = 36, AllowEdit = false)]
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置目标类型
        /// </summary>
        [Column(Name = "TargetType", DataType = DbType.String, Size = 10, AllowEdit =false)]
        public ProfitLossTargetType TargetType { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        [Column(Name = "Category", DataType = DbType.String, Size = 10, AllowEdit =false)]
        public ProfitLossCategory Category { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        [Column(Name = "Quantity", DataType = DbType.Decimal, Size = 18)]
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = 120)]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置创建者
        /// </summary>
        [Column(Name = "Creator", DataType = DbType.String, Size = 20, AllowEdit = false)]
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [Column(Name = "CreateTime", DataType = DbType.DateTime, AllowEdit = false)]
        public DateTime CreateTime { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 增加益损信息
        /// </summary>
        /// <param name="recordId">关联Id</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="category">分类</param>
        /// <param name="quantity">数量</param>
        /// <param name="remark">备注</param>
        /// <param name="money">金额</param>
        /// <param name="creator">创建人</param>
        public void Add(string recordId, ProfitLossTargetType targetType, ProfitLossCategory category, decimal quantity, string remark, string creator)
        {
            this.NewId();
            this.RecordId = recordId;
            this.TargetType = targetType;
            this.Category = category;
            this.Quantity = quantity;
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
            this.Creator = creator;
            this.CreateTime = DataConvert.ConvertUTCToBeijing(DateTime.UtcNow);
        }

        #endregion
    }
}
