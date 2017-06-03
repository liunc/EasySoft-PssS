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
    using Core.Util;
    using EasySoft.Core.Persistence;
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
        [Column(Name = "CustomerId", DataType = DbType.String, Size = 32)]
        public string CustomerId { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        [Column(Name = "Address", DataType = DbType.String, Size = 120)]
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        [Column(Name = "Mobile", DataType = DbType.String, Size = 20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        [Column(Name = "Linkman", DataType = DbType.String, Size = 50)]
        public string Linkman { get; set; }

        /// <summary>
        /// 获取或设置是否为默认地址
        /// </summary>
        [Column(Name = "IsDefault", DataType = DbType.String, Size = 1)]
        public string IsDefault { get; set; }

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
        public void Add(string customerId, string address, string mobile, string linkman, string isDefault, string creator)
        {
            this.NewId();
            this.CustomerId = customerId;
            this.IsDefault = isDefault;
            this.SetCreator(creator);
            this.Update(address, mobile, linkman, creator);
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
            this.Address = address;
            this.Mobile = mobile;
            this.Linkman = linkman;
            this.SetMender(mender);
        }

        /// <summary>
        /// 设置为默认地址
        /// </summary>
        /// <param name="mender">修改人</param>
        public void SetDefault(string mender)
        {
            this.IsDefault = Constant.COMMON_Y;
            this.SetMender(mender);
        }

        /// <summary>
        /// 取消默认地址设置
        /// </summary>
        /// <param name="mender">修改人</param>
        public void CancelDefault(string mender)
        {
            this.IsDefault = Constant.COMMON_N;
            this.SetMender(mender);
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
