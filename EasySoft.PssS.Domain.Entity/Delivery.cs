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
    using Application.DataTransfer.Cost;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using ValueObject;

    /// <summary>
    /// 交付领域实体类
    /// </summary>
    [Table("dbo.Delivery")]
    public class Delivery : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置日期
        /// </summary>
        [Column(Name = "Date", DataType = DbType.Date)]
        public DateTime Date { get; private set; }

        /// <summary>
        /// 获取或设置快递公司
        /// </summary>
        [Column(Name = "ExpressCompany", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string ExpressCompany { get; private set; }

        /// <summary>
        /// 获取或设置快递单号
        /// </summary>
        [Column(Name = "ExpressBill", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string ExpressBill { get; private set; }

        /// <summary>
        /// 获取或设置是否包含订单
        /// </summary>
        [Column(Name = "IncludeOrder", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string IncludeOrder { get; private set; }

        /// <summary>
        /// 获取或设置成本汇总
        /// </summary>
        [Column("Cost", DbType.Decimal, Size = Constant.STRING_LENGTH_18)]
        public decimal Cost { get; private set; }

        /// <summary>
        /// 获取或设置摘要
        /// </summary>
        [Column(Name = "Summary", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Summary { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; private set; }

        /// <summary>
        /// 获取或设置成本明细
        /// </summary>
        public List<Cost> Costs { get; set; }

        #endregion

        #region 构造函数

        public Delivery()
        {
            this.Costs = new List<Entity.Cost>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="date">日期</param>
        /// <param name="expressCompany">快递公司</param>
        /// <param name="expressBill">快递单号</param>
        /// <param name="includeOrder">是否包含订单</param>
        /// <param name="cost">成本</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public Delivery(string id, DateTime date, string expressCompany, string expressBill, string includeOrder, decimal cost, string summary, string remark, string creator, DateTime createTime, string mender, DateTime modifyTime) :
            base(id, creator, createTime, mender, modifyTime)
        {
            this.Date = date;
            this.ExpressCompany = expressCompany;
            this.ExpressBill = expressBill;
            this.IncludeOrder = includeOrder;
            this.Cost = cost;
            this.Summary = summary;
            this.Remark = remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="expressCompany">快递公司</param>
        /// <param name="expressBill">快递单号</param>
        /// <param name="includeOrder">包含订单</param>
        /// <param name="summary">摘要</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Create(DateTime date, string expressCompany, string expressBill, string includeOrder,string summary, string remark, List<CostAddDTO> costs, string creator)
        {
            base.Create(creator);
            this.Date = date;
            this.ExpressCompany = expressCompany;
            this.ExpressBill = expressBill;
            this.IncludeOrder = includeOrder;
            this.Summary = summary;
            this.Remark = remark;
            if (costs != null)
            {
                foreach (CostAddDTO cost in costs)
                {
                    Cost costEntity = new Cost();
                    costEntity.Create(this.Id, cost.Category, cost.Item, cost.Money);
                    this.Costs.Add(costEntity);
                    this.Cost += cost.Money;
                }
            }
        }

        /// <summary>
        /// 添加成本
        /// </summary>
        /// <param name="cost">成本值</param>
        public void AddCost(decimal cost)
        {
            this.Cost += cost;
        }

        #endregion
    }
}
