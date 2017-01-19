// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：仓储接口库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Repository
{
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// 成本项仓储接口
    /// </summary>
    public interface ICostItemRepository
    {
        /// <summary>
        /// 获取成本项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        List<CostItem> GetCostItem(string category, bool onlyValid);
    }
}
