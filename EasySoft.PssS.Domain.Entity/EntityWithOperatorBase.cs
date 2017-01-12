// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-12
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
    /// 领域实体基类，带操作者信息
    /// </summary>
    public class EntityWithOperatorBase : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置创建者
        /// </summary>
        public Operator Creator { get; set; }

        /// <summary>
        /// 获取或设置修改者
        /// </summary>
        public Operator Mender { get; set; }

        #endregion
    }
}
