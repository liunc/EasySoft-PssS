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
namespace EasySoft.PssS.Application.DataTransfer.Cost
{
    /// <summary>
    /// 新增成本数据传输对象类
    /// </summary>
    public class CostAddDTO
    {
        #region 属性

        /// <summary>
        /// 获取或设置成本分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置项
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置金额
        /// </summary>
        public decimal Money { get; set; }

        #endregion
    }
}
