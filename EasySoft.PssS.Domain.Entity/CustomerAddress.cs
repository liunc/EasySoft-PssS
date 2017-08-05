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
    /// 客户地址领域实体类
    /// </summary>
    [Table("dbo.CustomerAddress")]
    public class CustomerAddress : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置客户Id
        /// </summary>
        [Column(Name = "CustomerId", DataType = DbType.String, Size = Constant.STRING_LENGTH_32)]
        public string CustomerId { get; private set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        [Column(Name = "Address", DataType = DbType.String, Size = Constant.STRING_LENGTH_100)]
        public string Address { get; private set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        [Column(Name = "Mobile", DataType = DbType.String, Size = Constant.STRING_LENGTH_16)]
        public string Mobile { get; private set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [Column(Name = "Linkman", DataType = DbType.String, Size = Constant.STRING_LENGTH_10)]
        public string Linkman { get; private set; }

        /// <summary>
        /// 获取或设置是否为默认地址
        /// </summary>
        [Column(Name = "IsDefault", DataType = DbType.String, Size = Constant.STRING_LENGTH_1)]
        public string IsDefault { get; private set; }

        #endregion

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddress()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="customerId">客户ID</param>
        /// <param name="address">地址</param>
        /// <param name="mobile">手机号</param>
        /// <param name="linkman">联系人</param>
        /// <param name="isDefault">是否默认设置</param>
        /// <param name="creator">创建人</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="mender">修改人</param>
        /// <param name="modifyTime">修改时间</param>
        public CustomerAddress(string id, string customerId, string address, string mobile, string linkman, string isDefault, string creator, DateTime createTime, string mender, DateTime modifyTime)
            : base(id, creator, createTime, mender, modifyTime)
        {
            this.CustomerId = customerId;
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
            this.IsDefault = isDefault;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加客户地址
        /// </summary>
        /// <param name="customerId">客户Id</param>
        /// <param name="address">地址</param>
        /// <param name="mobile">手机号</param>
        /// <param name="linkman">联系人</param>
        /// <param name="isDefault">是否默认地址</param>
        /// <param name="creator">创建人</param>
        public void Create(string customerId, string address, string mobile, string linkman, string isDefault, string creator)
        {
            base.Create(creator);
            this.CustomerId = customerId;
            this.IsDefault = isDefault;
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
        }

        /// <summary>
        /// 更新客户地址
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="mobile">手机号</param>
        /// <param name="linkman">联系人</param>
        /// <param name="mender">修改人</param>
        public void Update(string address, string mobile, string linkman, string mender)
        {
            base.Update(mender);
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
        }

        /// <summary>
        /// 设置为默认地址
        /// </summary>
        /// <param name="mender">修改人</param>
        public void SetDefault(string mender)
        {
            this.IsDefault = Constant.COMMON_Y;
            this.Update(mender);
        }

        /// <summary>
        /// 取消默认地址设置
        /// </summary>
        /// <param name="mender">修改人</param>
        public void CancelDefault(string mender)
        {
            this.IsDefault = Constant.COMMON_N;
            this.Update(mender);
        }

        /// <summary>
        /// 判断是否默认地址
        /// </summary>
        /// <returns>返回布尔值</returns>
        public bool IsDefaultAddress()
        {
            return this.IsDefault == Constant.COMMON_Y;
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns>返回布尔值</returns>
        public bool CanDelete()
        {
            return this.IsDefault == Constant.COMMON_N;
        }

        #endregion
    }
}
