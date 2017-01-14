// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-13
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.User
{
    /// <summary>
    /// 登录视图模型类
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 获取或设置手机号码
        /// </summary>
        public string Moblie { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置要跳转的地址
        /// </summary>
        public string ReturnUrl{get;set;}
    }
}