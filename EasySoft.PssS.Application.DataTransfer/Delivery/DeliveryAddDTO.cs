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
namespace EasySoft.PssS.Application.DataTransfer.Delivery
{
    using Core.Util;
    using EasySoft.PssS.Application.DataTransfer.Cost;
    using EasySoft.PssS.Application.DataTransfer.Sale;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 新增交付数据传输对象类
    /// </summary>
    public class DeliveryAddDTO
    {
        /// <summary>
        /// 获取或设置日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 获取或设置快递公司
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        /// 获取或设置快递单号
        /// </summary>
        public string ExpressBill { get; set; }

        /// <summary>
        /// 获取或设置是否包含订单
        /// </summary>
        public string IncludeOrder { get; set; }

        /// <summary>
        /// 获取或设置摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置新增销售明细数据
        /// </summary>
        public List<DeliveryDetailAddDTO> DeliveryDetails { get; set; }

        /// <summary>
        /// 获取或设置新增销售数据
        /// </summary>
        public List<SaleAddDTO> Sales { get; set; }

        /// <summary>
        /// 获取或设置需要快递的订单数据
        /// </summary>
        public List<SaleOrderExpressDTO> SaleOrders { get; set; }

        /// <summary>
        /// 获取或设置新增成本数据
        /// </summary>
        public List<CostAddDTO> Costs { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryAddDTO()
        {
            this.IncludeOrder = Constant.COMMON_N;
            this.DeliveryDetails = new List<DeliveryDetailAddDTO>();
            this.Sales = new List<SaleAddDTO>();
            this.SaleOrders = new List<SaleOrderExpressDTO>();
        }
        
    }
}
