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
    using EasySoft.PssS.Web.Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 客户分组新增页面视图模型类
    /// </summary>
    public class CustomerAddModel 
    {
        #region 属性

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

        /// <summary>
        /// 获取或设置分组数据
        /// </summary>
        public List<ValueTextModel> CustomerGroups { get; set; }

        /// <summary>
        /// 获取或设置分组Id
        /// </summary>
        private List<string> CustomerGroupIds { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddModel() 
        {
            this.CustomerGroups = ParameterHelper.GetCustomerGroup();
            this.CustomerGroupIds = new List<string>();
            if (this.CustomerGroups != null)
            {
                foreach (ValueTextModel valueText in this.CustomerGroups)
                {
                    this.CustomerGroupIds.Add(valueText.Value);
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public virtual void PostValidate(ref List<string> errorMessages)
        {
            ValidateHelper.CheckInputString(WebResource.Field_Name, this.Name, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Nickname, this.Nickname, true, ValidateHelper.STRING_LENGTH_50, ref errorMessages);
            if (this.CustomerGroupIds != null)
            {
                ValidateHelper.CheckSelectString(WebResource.Field_Group, this.GroupId, true, this.CustomerGroupIds, ref errorMessages);
            }
            ValidateHelper.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, ValidateHelper.STRING_LENGTH_20, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Address, this.Address, true, ValidateHelper.STRING_LENGTH_120, ref errorMessages);
        }

        #endregion
    }
}