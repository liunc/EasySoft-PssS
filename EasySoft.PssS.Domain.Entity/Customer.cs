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
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// 客户领域实体类
    /// </summary>
    [Table("dbo.Customer")]
    public class Customer : EntityWithOperatorBase
    {
        #region 属性

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Column(Name = "Name", DataType = DbType.String, Size = 50)]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置呢称
        /// </summary>
        [Column(Name = "Nickname", DataType = DbType.String, Size = 50)]
        public string Nickname { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        [Column(Name = "Mobile", DataType = DbType.String, Size = 20)]
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置微信Id
        /// </summary>
        [Column(Name = "WeChatId", DataType = DbType.String, Size = 50)]
        public string WeChatId { get; set; }

        /// <summary>
        /// 获取或设置分组Id
        /// </summary>
        [Column(Name = "GroupId", DataType = DbType.String, Size = 32)]
        public string GroupId { get; set; }

        /// <summary>
        /// 获取或设置地址集合
        /// </summary>
        public List<CustomerAddress> Addresses { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Customer()
        {
            this.Addresses = new List<CustomerAddress>();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 添加新客户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="nickname">呢称</param>
        /// <param name="mobile">手机号</param>
        /// <param name="address">地址</param>
        /// <param name="weChatId">微信Id</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="creator">创建人</param>
        public void Add(string name, string nickname, string mobile, string address, string weChatId, string groupId, string creator)
        {
            this.NewId();
            this.Update(name, nickname, mobile, weChatId, groupId, creator);
            this.SetCreator(creator);
            CustomerAddress customerAddress = new CustomerAddress();
            string linkman = this.Name;
            if (string.IsNullOrWhiteSpace(linkman))
            {
                linkman = this.Nickname;
            }
            customerAddress.Add(this.Id, address, this.Mobile, linkman, Constant.COMMON_Y, creator);
            this.Addresses.Add(customerAddress);
        }

        /// <summary>
        /// 更新客户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="nickname">呢称</param>
        /// <param name="mobile">手机号</param>
        /// <param name="weChatId">微信Id</param>
        /// <param name="groupId">分组Id</param>
        /// <param name="mender">修改人</param>
        public void Update(string name, string nickname, string mobile, string weChatId, string groupId, string mender)
        {
            this.Name = name.Trim();
            this.Nickname = DataConvert.ConvertNullToEmptyString(nickname);
            this.Mobile = mobile.Trim();
            this.WeChatId = DataConvert.ConvertNullToEmptyString(weChatId);
            this.GroupId = groupId;
            this.SetMender(mender);
        }

        #endregion
    }
}
