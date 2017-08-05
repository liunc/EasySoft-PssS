// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.CostItem
{
    using EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 成本项缓存视图模型类
    /// </summary>
    public class CostItemCacheModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置排序号
        /// </summary>
        public short OrderNumber { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CostItemCacheModel()
        {
           
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">成本项领域实体对象</param>
        public CostItemCacheModel(CostItem entity)
        {
            this.Code = entity.Code;
            this.Name = entity.Name;
            this.OrderNumber = entity.OrderNumber;
        }

        #endregion
    }
}