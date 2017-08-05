// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：工具类库
// 创 建 人：刘年超
// 创建时间：2017-01-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Util
{
    using Microsoft.Security.Application;
    using EasySoft.Core.Util.Resources;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 验证类
    /// </summary>
    public class Validate
    {
        #region 属性

        /// <summary>
        /// 获取或设置是否验证失败
        /// </summary>
        public bool IsFailed { get; private set; }

        /// <summary>
        /// 获取或设置错误消息 
        /// </summary>
        public List<string> ErrorMessages { get; private set; }


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Validate()
        {
            this.IsFailed = false;
            this.ErrorMessages = new List<string>();
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

        /// <summary>
        /// 获取数据为空字符串
        /// </summary>
        /// <param name="name">字段名</param>
        /// <returns>返回字符串</returns>
        public static string GetListDataIsEmpty(string name)
        {
            return string.Format(UtilResource.Validate_ListDataIsEmpty, name);
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
                    this.AddErrorMessage(GetPleaseInputFieldString(name));
                    return string.Empty;
                }
                return string.Empty;
            }
            value = value.Trim();
            value = Sanitizer.GetSafeHtmlFragment(value);

            if (value.Length < minLength)
            {
                this.AddErrorMessage(GetFieldMinLengthString(name, minLength));
                return string.Empty;
            }

            if (value.Length > maxLength)
            {
                this.AddErrorMessage(GetFieldMaxLengthString(name, maxLength));
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
                this.AddErrorMessage(GetDecimalFieldRangeString(name, minValue, maxValue));
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
        public int CheckInt(string name, int value, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                this.AddErrorMessage(GetIntFieldRangeString(name, minValue, maxValue));
                return 0;
            }
            return value;
        }

        /// <summary>
        /// 检查日期字符串
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
                    this.AddErrorMessage(GetPleaseInputFieldString(name));
                    return date;
                }
            }
            if (!DateTime.TryParse(value, out date))
            {
                this.AddErrorMessage(UtilResource.Validate_PleaseInputDateString);
            }
            return date;
        }

        /// <summary>
        /// 检查字符串参数
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="isRequired">是否必填</param>
        /// <returns>返回字符串对应的值</returns>
        public string CheckStringArgument(string name, string value, bool isRequired)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                if (isRequired)
                {
                    this.AddErrorMessage(GetArgumentInValidString(name));
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
        public void CheckObjectArgument<T>(string name, T value)
        {
            if (value == null)
            {
                this.AddErrorMessage(GetArgumentInValidString(name));
            }
        }
        
        /// <summary>
        /// 检查字典
        /// </summary>
        /// <typeparam name="T">字典Key类型</typeparam>
        /// <typeparam name="K">字典Value类型</typeparam>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        /// <param name="dicts">字典</param>
        /// <returns>返回字典Value</returns>
        public K CheckDictionary<T, K>(string name, T value, Dictionary<T, K> dicts)
        {
            if(value == null || dicts == null || !dicts.ContainsKey(value))
            {
                this.AddErrorMessage(GetArgumentInValidString(name));
                return default(K);
            }
            return dicts[value];
        }

        /// <summary>
        /// 检查List集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="values">List集合数据</param>
        /// <returns>返回是否有数据</returns>
        public bool CheckList<T>(string name, List<T> values)
        {
            if (values == null || values.Count == 0)
            {
                this.AddErrorMessage(GetListDataIsEmpty(name));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        public void AddErrorMessage(string errorMessage)
        {
            if (!this.IsFailed)
            {
                this.IsFailed = true;
            }
            this.ErrorMessages.Add(errorMessage);
        }

        #endregion
    }
}
