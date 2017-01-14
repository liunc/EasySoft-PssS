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

    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 根据手机号与密码获取用户信息
        /// </summary>
        /// <param name="moblie">手机号</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户信息</returns>
        User GetUserByMoblieAndPassword(string moblie, string password);
    }
}
