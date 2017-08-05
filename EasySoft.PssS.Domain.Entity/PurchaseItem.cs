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
    using System;
    using System.Data;
    using ValueObject;

    /// <summary>
    /// 采购项领域实体类
    /// </summary>
    [Table("dbo.PurchaseItem")]
    public class PurchaseItem : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Column(Name = "Name", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Name { get; private set; }

        /// <summary>
        /// 获取或设置编码
        /// </summary>
        [Column(Name = "Code", DataType = DbType.String, Size = Constant.STRING_LENGTH_10, AllowEdit = false)]
        public string Code { get; private set; }

        /// <summary>
        /// 获取或设置采购分类
        /// </summary>
        [Column(Name = "Category", DataType = DbType.String, Size = Constant.STRING_LENGTH_1, AllowEdit = false)]
        public string Category { get; private set; }

        /// <summary>
        /// 获取或设置入库单位
        /// </summary>
        [Column(Name = "InUnit", DataType = DbType.String, Size = Constant.STRING_LENGTH_2)]
        public string InUnit { get; private set; }

        /// <summary>
        /// 获取或设置出库单位
        /// </summary>
        [Column(Name = "OutUnit", DataType = DbType.String, Size = Constant.STRING_LENGTH_2)]
        public string OutUnit { get; private set; }

        /// <summary>
        /// 获取或设置入库出库单位换算比例
        /// </summary>
        [Column(Name = "InOutRate", DataType = DbType.Decimal, Size = Constant.STRING_LENGTH_4)]
        public decimal InOutRate { get; private set; }

        /// <summary>
        /// 获取或设置销售单价
        /// </summary>
        [Column(Name = "Price", DataType = DbType.Decimal, Size = Constant.STRING_LENGTH_6)]
        public decimal Price { get; private set; }

        /// <summary>
        /// 获取或设置是否有效
        /// </summary>
        [Column(Name = "IsValid", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string IsValid { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; set; }

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseItem()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="category">分类</param>
        /// <param name="inUnit">入库单位</param>
        /// <param name="outUnit">出库单位</param>
        /// <param name="inOutRate">入库出库单位换算比例</param>
        /// <param name="price">销售单价</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public PurchaseItem(string id, string name, string code, string category, string inUnit, string outUnit, decimal inOutRate, decimal price, string isValid, string remark, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.Name = name;
            this.Code = code;
            this.Category = category;
            this.InUnit = inUnit;
            this.OutUnit = outUnit;
            this.InOutRate = inOutRate;
            this.Price = price;
            this.IsValid = IsValid;
            this.Remark = remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建采购项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="category">分类</param>
        /// <param name="inUnit">入库单位</param>
        /// <param name="outUnit">出库单位</param>
        /// <param name="inOutRate">入库出库单位换算比例</param>
        /// <param name="price">销售单价</param>
        /// <param name="creator">创建人</param>
        public void Create(string name, string code, string category, string inUnit, string outUnit, decimal inOutRate, decimal price, string creator)
        {
            base.Create(creator);
            this.Name = name;
            this.Code = code;
            this.Category = category;
            this.InUnit = inUnit;
            this.IsValid = Constant.COMMON_Y;
            if (category == PurchaseItemCategory.Product)
            {
                this.OutUnit = outUnit;
                this.InOutRate = inOutRate;
                this.Price = price;
            }
            else
            {
                this.OutUnit = string.Empty;
                this.InOutRate = 0;
                this.Price = 0;
            }
        }

        /// <summary>
        /// 修改采购项
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="inUnit">入库单位</param>
        /// <param name="outUnit">出库单位</param>
        /// <param name="inOutRate">入库出库单位换算比例</param>
        /// <param name="price">销售单价</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="mender">修改人</param>
        public void Update(string name, string inUnit, string outUnit, decimal inOutRate, decimal price, string isValid, string mender)
        {
            base.Update(mender);
            this.Name = name;
            this.InUnit = inUnit;
            this.IsValid = isValid;
            if (this.Category == PurchaseItemCategory.Product)
            {
                this.OutUnit = outUnit;
                this.InOutRate = inOutRate;
                this.Price = price;
            }
        }

        #endregion
    }
}
