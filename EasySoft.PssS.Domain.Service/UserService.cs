﻿// ----------------------------------------------------------
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
        private IUserRepository userRepository = null;

        public UserService()
        {
            this.userRepository = new UserRepository();
        }

        public User Login(string moblie, string password)
        {
            return this.userRepository.GetUserByMoblieAndPassword(moblie, password);
        }
    }
}
