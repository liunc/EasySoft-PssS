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
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using EasySoft.PssS.XmlRepository;

    /// <summary>
    /// 用户领域服务类
    /// </summary>
    public class UserService
    {
        #region 变量

        private IUserRepository userRepository = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserService()
        {
            this.userRepository = new UserRepository();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 用户登录 
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户信息</returns>
        public User Login(string mobile, string password)
        {
            return this.userRepository.GetUserByMobileAndPassword(mobile, password);
        }

        #endregion
    }
}
