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
    /// 采购项仓储接口
    /// </summary>
    public interface IPurchaseItemRepository
    {
        /// <summary>
        /// 获取采购项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="valid">是否有效</param>
        /// <returns>返回采购项信息</returns>
        List<PurchaseItem> GetPurchaseItem(string category, bool valid);
    }
}
