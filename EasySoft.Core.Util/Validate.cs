using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySoft.Core.Util
{
    public class Validate
    {
        #region 属性

        /// <summary>
        /// 获取或设置验证结果
        /// </summary>
        public ValidateResult Result { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Validate()
        {
            this.Result = new ValidateResult();
        }
        
        #endregion

        #region 获取字符串

        /// <summary>
        /// 获取参数无效字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetArgumentInValidString(string name)
        {
            return string.Format(UtilResource.Validate_ArgumentInValid, name);
        }

        /// <summary>
        /// 获取请输入字段字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetPleaseInputFieldString(string name)
        {
            return string.Format(UtilResource.Validate_PleaseInputField, name);
        }

        /// <summary>
        /// 获取请选择字段字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetPleaseSelectFieldString(string name)
        {
            return string.Format(UtilResource.Validate_PleaseSelectField, name);
        }

        /// <summary>
        /// 获取请输入字段，最少多少个字字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="minlength">最小长度</param>
        /// <returns>返回字符串</returns>
        public static string GetFieldMinLengthString(string name, int minlength)
        {
            return string.Format(UtilResource.Validate_FieldMinLength, name, minlength);
        }

        /// <summary>
        /// 获取请输入字段，最多多少个字字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <param name="maxlength">最大长度</param>
        /// <returns>返回字符串</returns>
        public static string GetFieldMaxLengthString(string name, int maxlength)
        {
            return string.Format(UtilResource.Validate_FieldMaxLength, name, maxlength);
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
            return string.Format(UtilResource.Validate_FieldLengthOverflow, name, minlength, maxlength);
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
            return string.Format(UtilResource.Validate_IntFieldRange, name, minValue, maxValue);
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
            return string.Format(UtilResource.Validate_DecimalFieldRange, name, minValue.ToString("0.##"), maxValue.ToString("0.##"));
        }

        #endregion

        #region 验证方法

        /// <summary>
        /// 检查输入的字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否为非空项</param>
        /// <param name="minLength">最短长度</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>返回验证后的字符串</returns>
        public string CheckInputString(string name, string value, bool isRequired, int minLength, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    this.Result.AddErrorMessage(GetPleaseInputFieldString(name));
                    return string.Empty;
                }
                return string.Empty;
            }
            value = value.Trim();
            value = Sanitizer.GetSafeHtmlFragment(value);

            if (value.Length < minLength)
            {
                this.Result.AddErrorMessage(GetFieldMinLengthString(name, minLength));
                return string.Empty;
            }

            if (value.Length > maxLength)
            {
                this.Result.AddErrorMessage(GetFieldMaxLengthString(name, maxLength));
                return string.Empty;
            }
            return value;
        }

        /// <summary>
        /// 检查输入的字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否为非空项</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回验证后的字符串</returns>
        public string CheckInputString(string name, string value, bool isRequired, int maxLength)
        {
            return this.CheckInputString(name, value, isRequired, 0, maxLength);
        }

        /// <summary>
        /// 检查数字或小数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回检查结果</returns>
        public decimal CheckDecimal(string name, decimal value, decimal minValue, decimal maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                this.Result.AddErrorMessage(GetDecimalFieldRangeString(name, minValue, maxValue));
                return 0;
            }
            return value;
        }

        /// <summary>
        /// 检查整数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public  int CheckInt(string name, int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                this.Result.AddErrorMessage(GetIntFieldRangeString(name, minValue, maxValue));
                return 0;
            }
            return value;
        }

        /// <summary>
        /// 检查选项字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否必选项</param>
        /// <param name="options">选项集合</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回执行结果</returns>
        public string CheckSelectString(string name, string value, bool isRequired, List<string> options)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    this.Result.AddErrorMessage(GetPleaseSelectFieldString(name));
                    return string.Empty;
                }
                return string.Empty;
            }
            value = value.Trim();
            if (!options.Contains(value))
            {
                this.Result.AddErrorMessage(GetArgumentInValidString(name));
                return string.Empty;
            }
            return value;
        }

        /// <summary>
        /// 检查选项字符串
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否必选项</param>
        /// <param name="errorMessages">返回的错误信息集合</param>
        /// <returns>返回转换成的日期数据</returns>
        public DateTime CheckDateString(string name, string value, bool isRequired)
        {
            DateTime date = new DateTime();
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    this.Result.AddErrorMessage(GetPleaseInputFieldString(name));
                    return date;
                }
            }
            if (!DateTime.TryParse(value, out date))
            {
                this.Result.AddErrorMessage(UtilResource.Validate_PleaseInputDateString);
            }
            return date;
        }

        /// <summary>
        /// 检查字符串参数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否必填</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public string CheckStringArgument(string name, string value, bool isRequired)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    this.Result.AddErrorMessage(GetArgumentInValidString(name));
                    return string.Empty;
                }
            }
            return value.Trim();
        }

        /// <summary>
        /// 检查字符串参数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <returns>返回字符串对应的枚举值</returns>
        public void CheckObjectArgument<T>(string name, T value)
        {
            if (value == null)
            {
                this.Result.AddErrorMessage(GetArgumentInValidString(name));
            }
        }

        #endregion
    }
}
