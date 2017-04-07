// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域服务类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Service
{
    using EasySoft.PssS.Repository;
    using EasySoft.PssS.XmlRepository;
    using EasySoft.PssS.Domain.Entity;
    using System.Collections.Generic;

    /// <summary>
    /// 配置参数领域服务类
    /// </summary>
    public class ParameterService
    {
        #region 变量

        private IPurchaseConfigRepository purchaseConfigRepository;
        private IDeliveryConfigRepository deliveryConfigRepository;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ParameterService()
        {
            this.purchaseConfigRepository = new PurchaseConfigRepository();
            this.deliveryConfigRepository = new DeliveryConfigRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购配置信息
        /// </summary>
        /// <returns>返回采购配置信息</returns>
        public PurchaseConfig GetPurchaseConfig()
        {
            return this.purchaseConfigRepository.GetPurchaseConfig();
        }

        /// <summary>
        /// 获取成本配置信息
        /// </summary>
        /// <returns>返回成本配置信息</returns>
        public DeliveryConfig GetDeliveryConfig()
        {
            return this.deliveryConfigRepository.GetDeliveryConfig();
        }

        #endregion
    }
}
