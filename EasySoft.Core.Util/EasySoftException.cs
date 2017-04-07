// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：工具类库
// 创 建 人：刘年超
// 创建时间：2017-02-19
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
    /// EasySoft 异常类
    /// </summary>
    public class EasySoftException : Exception
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EasySoftException()
            : base()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误描述信息</param>
        public EasySoftException(string message)
            : base(message)
        {
        }

        #endregion
    }
}