// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Entity
{
    /// <summary>
    /// 用户领域实体类
    /// </summary>
    public class User: EntityBase
    {
        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Moblie { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置角色
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 获取或设置姓名
        /// </summary>
        public string Name { get; set; }
    }
}
