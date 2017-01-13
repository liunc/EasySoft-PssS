// ----------------------------------------------------------
// 系统名称：EasySoft PssS
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
namespace EasySoft.PssS.Util
{
    using System;

    /// <summary>
    /// 时间转换类
    /// </summary>
    public class DateTimeUtil
    {
        #region 方法

        /// <summary>
        /// 将UTC时间转换为北京时间。
        /// </summary>
        /// <returns>返回北京时间</returns>
        public static DateTime ConvertUTCToBeijing()
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

        #endregion
    }
}
