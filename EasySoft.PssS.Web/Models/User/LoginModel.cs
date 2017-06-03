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
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 登录视图模型类
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 获取或设置手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置要跳转的地址
        /// </summary>
        public string ReturnUrl{get;set;}

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref List<string> errorMessages)
        {
            ValidateHelper.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Password, this.Password, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
        }
    }
}