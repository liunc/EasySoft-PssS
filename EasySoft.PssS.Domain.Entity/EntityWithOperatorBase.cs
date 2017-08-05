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
    /// 领域实体基类，带操作者信息
    /// </summary>
    public class EntityWithOperatorBase : EntityBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置创建者
        /// </summary>
        [Column(Name = "Creator", DataType = DbType.String, Size = Constant.STRING_LENGTH_16, AllowEdit = false)]
        public string Creator { get; private set; }

        /// <summary>
        /// 获取或设置创建时间
        /// </summary>
        [Column(Name = "CreateTime", DataType = DbType.DateTime, AllowEdit = false)]
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 获取或设置修改者
        /// </summary>
        [Column(Name = "Mender", DataType = DbType.String, Size = Constant.STRING_LENGTH_16)]
        public string Mender { get; private set; }

        /// <summary>
        /// 获取或设置修改时间
        /// </summary>
        [Column(Name = "ModifyTime", DataType = DbType.DateTime)]
        public DateTime ModifyTime { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityWithOperatorBase()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public EntityWithOperatorBase(string id, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id)
        {
            this.SetCreator(creator, createTime);
            this.SetMender(mender, modifyTime);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="creator">创建人</param>
        protected void Create(string creator)
        {
            this.NewId();
            this.SetCreator(creator);
            this.SetMender(creator);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="mender"></param>
        protected void Update(string mender)
        {
            this.SetMender(mender);
        }

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
            this.Creator = userId.Trim();
            this.CreateTime = DataConvert.ConvertUTCToBeijing(time);
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
            this.Mender = userId.Trim();
            this.ModifyTime = DataConvert.ConvertUTCToBeijing(time);
        }

        #endregion
    }
}
