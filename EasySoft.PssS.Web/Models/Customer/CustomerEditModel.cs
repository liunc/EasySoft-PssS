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
    using EasySoft.Core.Util;
    using EasySoft.Core.ViewModel;
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 客户编辑视图模型类
    /// </summary>
    public class CustomerEditModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public string QueryGroupId { get; set; }

        /// <summary>
        /// 获取或设置当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置呢称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 获取或设置微信Id
        /// </summary>
        public string WeChatId { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置分组Id
        /// </summary>
        public string GroupId { get; set; }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerEditModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">客户分组领域实体对象</param>
        public CustomerEditModel(Customer entity) : this()
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Nickname = entity.Nickname;
            this.WeChatId = entity.WeChatId;
            this.Mobile = entity.Mobile;
            this.GroupId = entity.GroupId;

        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="validate">验证对象</param>
        public void PostValidate(Validate validate)
        {
            this.Id = validate.CheckInputString(WebResource.Field_Id, this.Id, true, Constant.STRING_LENGTH_32);
            this.Name = validate.CheckInputString(WebResource.Field_Name, this.Name, true, Constant.STRING_LENGTH_10);
            this.Nickname = validate.CheckInputString(WebResource.Field_Nickname, this.Nickname, true, Constant.STRING_LENGTH_10);
            this.WeChatId = validate.CheckInputString(WebResource.Field_WeChatId, this.WeChatId, false, Constant.STRING_LENGTH_30);
            validate.CheckDictionary<string, string>(WebResource.Field_Group, this.GroupId, ParameterHelper.GetCustomerGroup());
            
            this.Mobile = validate.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, Constant.STRING_LENGTH_16);
        }

        #endregion
    }
}