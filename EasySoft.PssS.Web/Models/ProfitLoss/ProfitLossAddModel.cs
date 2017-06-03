// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-02-19
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
using EasySoft.PssS.Domain.ValueObject;
using EasySoft.PssS.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasySoft.PssS.Web.Models.ProfitLoss
{
    /// <summary>
    /// 新增益损视图模型类
    /// </summary>
    public class ProfitLossAddModel : ProfitLossModel
    {

        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置父级页面标题
        /// </summary>
        public string ParentPageTitle { get; set; }

        /// <summary>
        /// 获取或设置父级页面Url
        /// </summary>
        public string ParentPageUrl { get; set; }

        /// <summary>
        /// 获取或设置余量
        /// </summary>
        public decimal Inventory { get; set; }

        /// <summary>
        /// 获取或设置单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 获取或设置返回路径
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 获取或设置分类
        /// </summary>
        public ProfitLossCategory Category { get; set; }

        /// <summary>
        /// 获取或设置目标类型
        /// </summary>
        public ProfitLossTargetType TargetType { get; set; }
        
        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref List<string> errorMessages)
        {
            this.Category = ValidateHelper.CheckProfitLossCategory(this.CategoryString, ref errorMessages);
            this.TargetType = ValidateHelper.CheckProfitLossTargetType(this.TargetTypeString, ref errorMessages);
            ValidateHelper.CheckStringArgument(WebResource.Field_RecordId, this.RecordId, true, ref errorMessages);
            ValidateHelper.CheckDecimal(WebResource.Field_Quantity, this.Quantity, ValidateHelper.DECIMAL_MIN, ValidateHelper.DECIMAL_MAX, ref errorMessages);
            ValidateHelper.CheckInputString(WebResource.Field_Remark, this.Remark, false, ValidateHelper.STRING_LENGTH_120, ref errorMessages);
        }

    }
}