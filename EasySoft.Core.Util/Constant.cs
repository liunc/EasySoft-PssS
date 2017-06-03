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
namespace EasySoft.Core.Util
{
    /// <summary>
    /// 常量定义类
    /// </summary>
    public struct Constant
    {
        /// <summary>
        /// Y
        /// </summary>
        public static readonly string COMMON_Y = "Y";

        /// <summary>
        /// N
        /// </summary>
        public static readonly string COMMON_N = "N";

        /// <summary>
        /// 时间日期格式 yyyy-MM-dd
        /// </summary>
        public static readonly string DATE_YYYY_MM_DD = "yyyy-MM-dd";

        #region 验证

        /// <summary>
        /// 整数最小值
        /// </summary>
        public static readonly int INT_ZERO = 0;

        /// <summary>
        /// 整数需要输入的最小值
        /// </summary>
        public static readonly int INT_REQUIRED_MIN = 1;

        /// <summary>
        /// 整数最大值
        /// </summary>
        public static readonly int INT_MAX = 99999999;

        /// <summary>
        /// 数字0
        /// </summary>
        public static readonly decimal DECIMAL_ZERO = 0M;

        /// <summary>
        /// 数字需要输入的最小值
        /// </summary>
        public static readonly decimal DECIMAL_REQUIRED_MIN = 0.01M;

        /// <summary>
        /// 数字最大值
        /// </summary>
        public static readonly decimal DECIMAL_MAX = 99999999.99M;

        /// <summary>
        /// 字符串长度20
        /// </summary>
        public static readonly int STRING_LENGTH_20 = 20;

        /// <summary>
        /// 字符串长度32
        /// </summary>
        public static readonly int STRING_LENGTH_32 = 32;

        /// <summary>
        /// 字符串长度50
        /// </summary>
        public static readonly int STRING_LENGTH_50 = 50;

        /// <summary>
        /// 字符串长度120
        /// </summary>
        public static readonly int STRING_LENGTH_120 = 120;

        #endregion
    }
}
