// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据传输对象类库
// 创 建 人：刘年超
// 创建时间：2017-07-18
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Application.DataTransfer.Sale
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 新增销售订单数据传输对象类
    /// </summary>
    public class SaleOrderAddDTO
    {
        #region 属性

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public DateTime Date { get; set; }

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
        /// 获取或设置单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置销售订单明细
        /// </summary>
        public List<SaleOrderDetailAddDTO> SaleOrderDetails { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderAddDTO()
        {
            this.SaleOrderDetails = new List<SaleOrderDetailAddDTO>();
        }

        #endregion
    }
}
