// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-01-12
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Domain.Entity
{
    using EasySoft.Core.Persistence;
    using EasySoft.Core.Util;
    using System;
    using System.Data;

    /// <summary>
    /// 领域实体基类
    /// </summary>
    public class EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        [Column(Size = Constant.STRING_LENGTH_32, DataType = DbType.String, PrimaryKey = true)]
        public string Id { get; protected set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityBase() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Id</param>
        public EntityBase(string id)
        {
            this.Id = id;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 生成新Id
        /// </summary>
        public void NewId()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }

        #endregion
    }
}
