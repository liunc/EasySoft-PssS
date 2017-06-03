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
    using EasySoft.PssS.Domain.Entity;
    using Resources;
    using System.Collections.Generic;

    /// <summary>
    /// 新增客户分组数据视图模型类
    /// </summary>
    public class CustomerGroupEditModel : CustomerGroupAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取当前页索引
        /// </summary>
        public int PageIndex { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupEditModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">客户分组领域实体对象</param>
        public CustomerGroupEditModel(CustomerGroup entity): this()
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Remark = entity.Remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public override void PostValidate(ref List<string> errorMessages)
        {
            ValidateHelper.CheckInputString(WebResource.Field_Id, this.Id, true, ValidateHelper.STRING_LENGTH_32, ref errorMessages);
            base.PostValidate(ref errorMessages);
        }

        #endregion
    }
}