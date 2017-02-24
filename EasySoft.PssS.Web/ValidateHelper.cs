// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-02-24
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web
{
    using Domain.ValueObject;
    using EasySoft.PssS.Web.Resources;
    using Microsoft.Security.Application;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// 数据验证类
    /// </summary>
    public class ValidateHelper
    {
        #region 获取字符串

        /// <summary>
        /// 获取参数无效字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetArgumentInValidString(string name)
        {
            return string.Format(WebResource.Validate_ArgumentInValid, name);
        }

        /// <summary>
        /// 获取请输入字段字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetPleaseInputFieldString(string name)
        {
            return string.Format(WebResource.Validate_PleaseInputField, name);
        }

        /// <summary>
        /// 获取请选择字段字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetPleaseSelectFieldString(string name)
        {
            return string.Format(WebResource.Validate_PleaseSelectField, name);
        }

        /// <summary>
        /// 获取请输入字段，字数在min-max个之间字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="minlength">最小长度</param>
        /// <param name="maxlength">最大长度</param>
        /// <returns>返回字符串</returns>
        public static string GetFieldLengthOverflowString(string name, int minlength, int maxlength)
        {
            return string.Format(WebResource.Validate_FieldLengthOverflow, name, minlength, maxlength);
        }

        /// <summary>
        /// 获取请输入字段，值为最小值-最大值之间的整数字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>返回字符串</returns>
        public static string GetIntFieldRangeString(string name, int minValue, int maxValue)
        {
            return string.Format(WebResource.Validate_IntFieldRange, name, minValue, maxValue);
        }

        /// <summary>
        /// 获取请输入字段，值为最小值-最大值之间的整数或小数字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>返回字符串</returns>
        public static string GetDecimalFieldRangeString(string name, decimal minValue, decimal maxValue)
        {
            return string.Format(WebResource.Validate_DecimalFieldRange, name, minValue.ToString("0.##"), maxValue.ToString("0.##"));
        }

        #endregion

        #region 检查方法

        /// <summary>
        /// 检查输入的字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否为非空项</param>
        /// <param name="minLength">最短长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        public static void CheckInputString(string name, string value, bool isRequired, int minLength, int maxLength, ref List<string> errorMessages)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    errorMessages.Add(GetPleaseInputFieldString(name));
                    return;
                }
                return;
            }
            value = value.Trim();
            value = Sanitizer.GetSafeHtmlFragment(value);
            int length = value.Length;
            if (isRequired && minLength == 0)
            {
                minLength = 1;
            }
            if ((minLength > 0 && length < minLength) || (maxLength > 0 && length > maxLength))
            {
                errorMessages.Add(GetFieldLengthOverflowString(name, minLength, maxLength));
            }
        }

        /// <summary>
        /// 检查输入的字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否为非空项</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        public static void CheckInputString(string name, string value, bool isRequired, int maxLength, ref List<string> errorMessages)
        {
            CheckInputString(name, value, isRequired, 0, maxLength, ref errorMessages);
        }

        /// <summary>
        /// 检查数字或小数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        public static void CheckDecimal(string name, decimal value, decimal minValue, decimal maxValue, ref List<string> errorMessages)
        {
            if (value < minValue || value > maxValue)
            {
                errorMessages.Add(GetDecimalFieldRangeString(name, minValue, maxValue));
            }
        }

        /// <summary>
        /// 检查整数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        public static void CheckInt(string name, int value, int minValue, int maxValue, ref List<string> errorMessages)
        {
            if (value < minValue || value > maxValue)
            {
                errorMessages.Add(GetIntFieldRangeString(name, minValue, maxValue));
            }
        }

        /// <summary>
        /// 检查选项字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否必选项</param>
        /// <param name="options">选项集合</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        public static void CheckSelectString(string name, string value, bool isRequired, List<string> options, ref List<string> errorMessages)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    errorMessages.Add(GetPleaseSelectFieldString(name));
                    return;
                }
                return;
            }
            if (!options.Contains(value))
            {
                errorMessages.Add(GetArgumentInValidString(name));
            }
        }

        /// <summary>
        /// 检查字符串参数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public static bool CheckStringArgument(string name, string value, bool isRequired, ref List<string> errorMessages)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    errorMessages.Add(GetArgumentInValidString(name));
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查字符串参数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public static bool CheckObjectArgument<T>(string name, T value, ref List<string> errorMessages)
        {
            if (value == null)
            {
                errorMessages.Add(GetArgumentInValidString(name));
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 检查益损类型
        /// </summary>
        /// <param name="value">传入的字符串</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public static ProfitLossCategory CheckProfitLossCategory(string value, ref List<string> errorMessages)
        {
            if (!CheckStringArgument(WebResource.Field_ProfitLossCategory, value, true, ref errorMessages))
            {
                return ProfitLossCategory.None;
            }
            if (value.Equals(ProfitLossCategory.Profit.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ProfitLossCategory.Profit;
            }
            if (value.Equals(ProfitLossCategory.Loss.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ProfitLossCategory.Loss;
            }
            errorMessages.Add(GetArgumentInValidString(WebResource.Field_ProfitLossCategory));
            return ProfitLossCategory.None;
        }

        /// <summary>
        /// 检查益损目标类型
        /// </summary>
        /// <param name="value">传入的字符串</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public static ProfitLossTargetType CheckProfitLossTargetType(string value, ref List<string> errorMessages)
        {
            if (!CheckStringArgument(WebResource.Field_ProfitLossTargetType, value, true, ref errorMessages))
            {
                return ProfitLossTargetType.None;
            }
            if (value.Equals(ProfitLossTargetType.Purchase.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ProfitLossTargetType.Purchase;
            }
            if (value.Equals(ProfitLossTargetType.Sale.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return ProfitLossTargetType.Sale;
            }
            errorMessages.Add(GetArgumentInValidString(WebResource.Field_ProfitLossCategory));
            return ProfitLossTargetType.None;
        }
    }
}