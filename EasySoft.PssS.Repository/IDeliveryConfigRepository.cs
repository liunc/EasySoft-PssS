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
    /// 交付配置仓储接口
    /// </summary>
    public interface IDeliveryConfigRepository
    {
        /// <summary>
        /// 获取采购配置信息
        /// </summary>
        /// <returns>返回采购配置信息</returns>
        DeliveryConfig GetDeliveryConfig();
    }
}
