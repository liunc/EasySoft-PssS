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
namespace EasySoft.PssS.Web.Models.CustomerAddress
{
    using Core.Util;
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 添加客户地址视图模型类
    /// </summary>
    public class CustomerAddressAddModel
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
        public string CustomerId { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        /// 获取或设置是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(Validate validate)
        {
            this.CustomerId = validate.CheckInputString(WebResource.Field_CustomerId, this.CustomerId, true, Constant.STRING_LENGTH_32);
            this.Linkman = validate.CheckInputString(WebResource.Field_Linkman, this.Linkman, true, Constant.STRING_LENGTH_50);
            this.Mobile = validate.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, Constant.STRING_LENGTH_20);
            this.Address = validate.CheckInputString(WebResource.Field_Address, this.Address, true, Constant.STRING_LENGTH_120);
        }

        #endregion
    }
}