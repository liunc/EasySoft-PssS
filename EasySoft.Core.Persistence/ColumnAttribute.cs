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
    using System.Data;

    /// <summary>
    /// 列特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        #region 属性

        /// <summary>
        /// 获取或设置列名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        public DbType DataType
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置长度
        /// </summary>
        public int Size
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置是否为主键列
        /// </summary>
        public bool PrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置是否为自增列
        /// </summary>
        public bool Identity
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置是否可编辑
        /// </summary>
        public virtual bool AllowEdit
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ColumnAttribute()
        {
            this.Size = 0;
            this.Identity = false;
            this.AllowEdit = true;
            this.PrimaryKey = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">列名称</param>
        /// <param name="dataType">数据类型</param>
        public ColumnAttribute(string name, DbType dataType)
            : this()
        {
            this.Name = name;
            this.DataType = dataType;
        }

        #endregion
    }
}
