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
    using EasySoft.Core.Persistence.Repository;
    using EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 交付明细仓储接口
    /// </summary>
    public interface IDeliveryDetailRepository : IBaseRepository<DeliveryDetail, string>
    {

    }
}
