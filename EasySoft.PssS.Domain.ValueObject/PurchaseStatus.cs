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
    /// 采购状态
    /// </summary>
    public struct PurchaseStatus
    {
        /// <summary>
        /// 未指定
        /// </summary>
        public const string None = "N";

        /// <summary>
        /// 处理中
        /// </summary>
        public const string Processing = "P";

        /// <summary>
        /// 完成
        /// </summary>
        public const string Finished = "F";

    }
}
