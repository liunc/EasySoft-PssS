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
    using Core.Persistence;
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
        [Column(Size =32, DataType = DbType.String, PrimaryKey =true )]
        public string Id { get; set; }

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
