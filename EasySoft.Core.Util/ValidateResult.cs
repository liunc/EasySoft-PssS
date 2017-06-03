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
    using System.Collections.Generic;

    /// <summary>
    /// 验证结果类
    /// </summary>
    public class ValidateResult
    {
        /// <summary>
        /// 获取或设置是否成功
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// 获取或设置错误消息 
        /// </summary>
        public List<string> ErrorMessages { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ValidateResult()
        {
            this.Success = true;
            this.ErrorMessages = new List<string>();
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="errorMessage">错误消息</param>
        public void AddErrorMessage(string errorMessage)
        {
            if (this.Success)
            {
                this.Success = false;
            }
            this.ErrorMessages.Add(errorMessage);
        }

    }
}
