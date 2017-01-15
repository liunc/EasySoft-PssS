// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Xml文件仓储类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.XmlRepository
{
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Repository;
    using System.Xml;

    /// <summary>
    /// 用户仓储实现类
    /// </summary>
    public class UserRepository : XmlRepositoryBase, IUserRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserRepository()
        {
            this.XmlFileName = "User";
        }
        
        #endregion

        #region 方法

        /// <summary>
        /// 根据手机号与密码获取用户信息
        /// </summary>
        /// <param name="moblie">手机号</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户信息</returns>
        public User GetUserByMoblieAndPassword(string moblie, string password)
        {
            XmlNode node = this.DataSource.SelectSingleNode(string.Format("//User[@Moblie='{0}' and @Password='{1}']", moblie, password));
            if (node == null)
            {
                return null;
            }
            return new User
            {
                Moblie = moblie,
                Roles = this.GetXmlNodeAttribute(node, "Roles"),
                Name = node.InnerText.Trim()
            };
        }

        #endregion


    }
}
