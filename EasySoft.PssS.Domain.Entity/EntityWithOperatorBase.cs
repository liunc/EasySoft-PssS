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
    using EasySoft.PssS.Domain.ValueObject;
    using System;
    using System.Data;
    using Core.Util;

    /// <summary>
    /// 领域实体基类，带操作者信息
    /// </summary>
    public class EntityWithOperatorBase : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置创建者
        /// </summary>
        [Column(Name = "Creator", DataType = DbType.String, Size =20, AllowEdit =false )]
        public string Creator { get; set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [Column(Name = "CreateTime", DataType = DbType.DateTime, AllowEdit = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 获取或设置修改者
        /// </summary>
        [Column(Name = "Mender", DataType = DbType.String, Size = 20)]
        public string Mender { get; set; }

        /// <summary>
        /// 获取或设置修改时间
        /// </summary>
        [Column(Name = "ModifyTime", DataType = DbType.DateTime)]
        public DateTime ModifyTime { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 设置创建人和创建时间
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void SetCreator(string userId)
        {
            this.SetCreator(userId, DateTime.UtcNow);
        }

        /// <summary>
        /// 设置创建人和创建时间
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="time">时间</param>
        public void SetCreator(string userId, DateTime time)
        {
            this.Creator = userId;
            this.CreateTime = DateTimeUtil.ConvertUTCToBeijing(time);
        }

        /// <summary>
        /// 设置修改人和修改时间
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void SetMender(string userId)
        {
            this.SetMender(userId, DateTime.UtcNow);
        }

        /// <summary>
        /// 设置修改人和修改时间
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="time">时间</param>
        public void SetMender(string userId, DateTime time)
        {
            this.Mender = userId;
            this.ModifyTime = DateTimeUtil.ConvertUTCToBeijing(time);
        }

        #endregion
    }
}
