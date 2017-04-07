// ----------------------------------------------------------
// 系统名称：核心框架库
// 项目名称：ORM组件
// 创 建 人：刘年超
// 创建时间：2010-07-27
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.Core.Persistence
{
    using System;

    /// <summary>
    /// 表特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        #region 属性

        /// <summary>
        /// 获取或设置表名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">表名称</param>
        public TableAttribute(string name)
        {
            this.Name = name;
        }

        #endregion
    }
}
