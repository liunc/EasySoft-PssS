// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域值对象类库
// 创 建 人：刘年超
// 创建时间：2017-01-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.ValueObject
{
    using System;
    using Util;

    /// <summary>
    /// 操作者领域值对象类
    /// </summary>
    public class Operator
    {
        #region 属性

        /// <summary>
        /// 获取或设置用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 获取或设置时间
        /// </summary>
        public DateTime Time { get; set; }

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public Operator() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userId">用户Id</param>
        public Operator(string userId)
        {
            this.UserId = userId;
            this.Time = DateTimeUtil.ConvertUTCToBeijing();
        }

        #endregion
    }
}
