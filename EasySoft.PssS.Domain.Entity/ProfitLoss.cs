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
    using EasySoft.PssS.Domain.ValueObject;

    /// <summary>
    /// 益损领域实体类
    /// </summary>
    public class ProfitLoss : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置关联Id
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        public ProfitLossCategory Category { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 增加益损项
        /// </summary>
        /// <param name="recordId">关联Id</param>
        /// <param name="category">分类</param>
        /// <param name="quantity">数量</param>
        /// <param name="remark">备注</param>
        /// <param name="money">金额</param>
        /// <param name="creator">创建人</param>
        public void Add(string recordId, ProfitLossCategory category, decimal quantity, string remark, string creator)
        {
            this.NewId();
            this.RecordId = recordId;
            this.Category = category;
            this.Quantity = quantity;
            this.Remark = string.IsNullOrWhiteSpace(remark) ? string.Empty : remark.Trim();
            this.Creator = new Operator(creator);
            this.Mender = this.Creator;
        }

        #endregion
    }
}
