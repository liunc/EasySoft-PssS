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
    using Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using System;
    using System.Data;

    /// <summary>
    /// 成本领域实体类
    /// </summary>
    [Table("dbo.Cost")]
    public class Cost : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置关联Id
        /// </summary>
        [Column(Name = "RecordId", DataType = DbType.String, Size = Constant.STRING_LENGTH_32, AllowEdit = false)]
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        [Column(Name = "Category", DataType = DbType.String, Size = Constant.STRING_LENGTH_1, AllowEdit = false)]
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        [Column(Name = "Item", DataType = DbType.String, Size = Constant.STRING_LENGTH_10, AllowEdit = false)]
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置金额
        /// </summary>
        [Column(Name = "Money", DataType = DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Money { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Cost()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="money">金额</param>
        public Cost(string id, decimal money)
            : base(id)
        {
            this.Money = money;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="recordId">记录ID</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="money">金额</param>
        public Cost(string id, string recordId, string category, string item, decimal money)
            : this(id, money)
        {
            this.RecordId = recordId;
            this.Category = category;
            this.Item = item;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 增加成本项
        /// </summary>
        /// <param name="recordId">关联Id</param>
        /// <param name="category">分类</param>
        /// <param name="item">项</param>
        /// <param name="money">金额</param>
        public void Create(string recordId, string category, string item, decimal money)
        {
            this.NewId();
            this.RecordId = recordId;
            this.Category = category;
            this.Item = item;
            this.Money = money;
        }

        /// <summary>
        /// 更新成本项
        /// </summary>
        /// <param name="money">金额</param>
        public void Update(decimal money)
        {
            this.Money = money;
        }

        #endregion
    }
}
