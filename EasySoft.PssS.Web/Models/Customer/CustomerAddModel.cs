// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Customer
{
    using Core.Util;
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 客户分组新增页面视图模型类
    /// </summary>
    public class CustomerAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置呢称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置分组Id
        /// </summary>
        public string GroupId { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddModel(string source)
        {
            this.Source = source;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="validate">验证类对象</param>
        public virtual void PostValidate(ref Validate validate)
        {
            this.Name = validate.CheckInputString(WebResource.Field_Name, this.Name, true, Constant.STRING_LENGTH_10);
            this.Nickname = validate.CheckInputString(WebResource.Field_Nickname, this.Nickname, true, Constant.STRING_LENGTH_10);
            validate.CheckDictionary<string, string>(WebResource.Field_Group, this.GroupId, ParameterHelper.GetCustomerGroup());
            this.Mobile = validate.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, Constant.STRING_LENGTH_16);
            this.Address = validate.CheckInputString(WebResource.Field_Address, this.Address, true, Constant.STRING_LENGTH_100);
        }

        #endregion
    }
}