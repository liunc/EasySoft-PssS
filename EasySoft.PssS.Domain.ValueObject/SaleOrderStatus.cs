// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域值对象类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.ValueObject
{
    /// <summary>
    /// 销售订单状态
    /// </summary>
    public struct SaleOrderStatus
    {
        /// <summary>
        /// 已下单
        /// </summary>
        public const string Ordered = "O";

        /// <summary>
        /// 已发货
        /// </summary>
        public const string Sent = "S";

        /// <summary>
        /// 已收货未付款
        /// </summary>
        public const string Received = "R";

        /// <summary>
        /// 已完成
        /// </summary>
        public const string Finished = "F";

    }
}
