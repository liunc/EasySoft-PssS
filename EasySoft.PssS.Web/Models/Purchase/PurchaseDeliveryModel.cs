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
namespace EasySoft.PssS.Web.Models.Purchase
{
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using Web.Resources;
    using Core.Util;

    /// <summary>
    /// 采购视图模型类
    /// </summary>
    public class PurchaseDeliveryModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置项名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 获取或设置供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置包装单位
        /// </summary>
        public string PackUnit { get; set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        public decimal Inventory { get; set; }

        /// <summary>
        /// 获取或设置出库数量
        /// </summary>
        public string DeliveryQuantity { get; set; }

        /// <summary>
        /// 获取或设置包装数量
        /// </summary>
        public string PackQuantity { get; set; }

        /// <summary>
        /// 获取或设置出入库比例
        /// </summary>
        public decimal InOutRate { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseDeliveryModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">实体模型对象</param>
        public PurchaseDeliveryModel(Purchase entity)
        {
            this.Id = entity.Id;
            this.Item = entity.Item;
            this.Date = DataConvert.ConvertDateToString(entity.Date);
            this.Unit = entity.Unit;
            this.Supplier = entity.Supplier;
            this.Inventory = entity.Inventory;
        }

        #endregion

    }
}
