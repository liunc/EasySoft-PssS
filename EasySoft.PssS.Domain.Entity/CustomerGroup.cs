// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：领域实体类库
// 创 建 人：刘年超
// 创建时间：2017-05-12
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
    /// 客户分组领域实体类
    /// </summary>
    [Table("dbo.CustomerGroup")]
    public class CustomerGroup : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Column(Name = "Name", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Name { get; private set; }

        /// <summary>
        /// 获取或设置是否为默认设置
        /// </summary>
        [Column(Name = "IsDefault", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string IsDefault { get; private set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        [Column(Name = "Remark", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Remark { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroup()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="isDefault">是否为默认设置</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public CustomerGroup(string id, string name, string isDefault, string remark, string creator, DateTime createTime, string mender, DateTime modifyTime) :
            base(id, creator, createTime, mender, modifyTime)
        {
            this.Name = name;
            this.IsDefault = isDefault;
            this.Remark = remark;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="remark">备注</param>
        /// <param name="creator">创建人</param>
        public void Create(string name, string remark, string creator)
        {
            base.Create(creator);
            this.IsDefault = Constant.COMMON_N;
            this.Name = name.Trim();
            this.Remark = DataConvert.ConvertNullToEmptyString(remark);
        }

        /// <summary>
        /// 更新分组
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="remark">备注</param>
        /// <param name="mender">修改人</param>
        public void Update(string name, string remark, string mender)
        {
            base.Update(mender);
            this.Name = name.Trim();
            this.Remark = DataConvert.ConvertNullToEmptyString(remark);
        }

        public bool CanDelete()
        {
            return this.IsDefault == Constant.COMMON_N;
        }

        #endregion
    }
}
