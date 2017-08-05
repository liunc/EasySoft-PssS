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
namespace EasySoft.PssS.Web.Models.CustomerGroup
{
    using Core.Util;
    using EasySoft.PssS.Domain.Entity;
    using Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 新增客户分组数据视图模型类
    /// </summary>
    public class CustomerGroupAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupAddModel()
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public virtual void PostValidate(ref Validate validate)
        {
            this.Name = validate.CheckInputString(WebResource.Field_Name, this.Name, true, Constant.STRING_LENGTH_10);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);
        }

        #endregion
    }
}