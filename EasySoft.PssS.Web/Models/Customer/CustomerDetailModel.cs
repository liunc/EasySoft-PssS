// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-01-15
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Web.Models.Customer
{
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Web.Models.CustomerAddress;
    using System.Collections.Generic;

    /// <summary>
    /// 客户明细视图模型类
    /// </summary>
    public class CustomerDetailModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 获取或设置查询分组
        /// </summary>
        public string QueryGroupId { get; set; }

        /// <summary>
        /// 获取或设置当前页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置呢称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 获取或设置微信Id
        /// </summary>
        public string WeChatId { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置分组
        /// </summary>
        public string Group { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerDetailModel()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">客户分组领域实体对象</param>
        public CustomerDetailModel(Customer entity) : this()
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Nickname = entity.Nickname;
            this.WeChatId = entity.WeChatId;
            this.Mobile = entity.Mobile;
            this.Group = entity.GroupId;

        }

        #endregion
    }
}