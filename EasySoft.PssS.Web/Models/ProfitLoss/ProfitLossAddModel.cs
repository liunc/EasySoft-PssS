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
using EasySoft.Core.Util;
using EasySoft.PssS.Domain.ValueObject;
using EasySoft.PssS.Web.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasySoft.PssS.Web.Models.ProfitLoss
{
    /// <summary>
    /// 新增益损视图模型类
    /// </summary>
    public class ProfitLossAddModel
    {
        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title { get; set; }
        
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
        public string Category { get; set; }

        /// <summary>
        /// 获取或设置目标类型
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 获取或设置关联Id
        /// </summary>
        public string RecordId { get; set; }

        /// <summary>
        /// 获取或设置数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            validate.CheckDictionary<string, string>(WebResource.Field_ProfitLossCategory, this.Category, ParameterHelper.GetProfitLossCatetory());
            validate.CheckDictionary<string, string>(WebResource.Field_ProfitLossTargetType, this.TargetType, ParameterHelper.GetProfitLossTargetType());
            this.RecordId = validate.CheckStringArgument(WebResource.Field_RecordId, this.RecordId, true);
            this.Quantity = validate.CheckDecimal(WebResource.Field_Quantity, this.Quantity, Constant.DECIMAL_REQUIRED_MIN, Constant.DECIMAL_MAX);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);
        }

    }
}