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
    using System;

    /// <summary>
    /// 时间转换类
    /// </summary>
    public class DataConvert
    {
        #region 日期与字符串相互转换

        /// <summary>
        /// 将UTC时间转换为北京时间。
        /// </summary>
        /// <returns>返回北京时间</returns>
        public static DateTime ConvertNowUTCToBeijing()
        {
            return ConvertUTCToBeijing(DateTime.UtcNow);
        }

        /// <summary>
        /// 将UTC时间转换为北京时间。
        /// </summary>
        /// <param name="time">要转换的UTC时间</param>
        /// <returns>返回北京时间</returns>
        public static DateTime ConvertUTCToBeijing(DateTime time)
        {
            return time.AddHours(8);
        }

        /// <summary>
        /// 将日期转换为日期字符串
        /// </summary>
        /// <param name="time">日期</param>
        /// <param name="format">字符串格式</param>
        /// <returns>返回日期字符串</returns>
        public static string ConvertDateToString(DateTime time, string format)
        {
            if (time == DateTime.MinValue)
            {
                return string.Empty;
            }
            return time.ToString(format);
        }

        /// <summary>
        /// 将日期转换为日期字符串
        /// </summary>
        /// <param name="time">日期</param>
        /// <returns>返回日期字符串</returns>
        public static string ConvertDateToString(DateTime time)
        {
            return ConvertDateToString(time, Constant.DATE_YYYY_MM_DD);
        }

        /// <summary>
        /// 将日期字符串转换为日期
        /// </summary>
        /// <param name="dateString">日期字符串</param>
        /// <returns>返回日期</returns>
        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTime time = new DateTime();
            dateString = string.Format("{0} 00:00:00.000", dateString);
            DateTime.TryParse(dateString, out time);
            return time;
        }

        #endregion

        /// <summary>
        /// 将Null转换成空字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>返回转换后的字符串</returns>
        public static string ConvertNullToEmptyString(string source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return source.Trim();
        }

    }
}