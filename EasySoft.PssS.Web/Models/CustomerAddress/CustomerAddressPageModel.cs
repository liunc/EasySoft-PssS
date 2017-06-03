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
namespace EasySoft.PssS.Web.Models.CustomerAddress
{
    using Entity = EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 客户地址视图模型类
    /// </summary>
    public class CustomerAddressPageModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        /// 获取或设置是否为默认地址
        /// </summary>
        public string IsDefault { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressPageModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="address">客户地址领域实体对象</param>
        public CustomerAddressPageModel(Entity.CustomerAddress address)
        {
            this.Id = address.Id;
            this.Address = address.Address;
            this.IsDefault = address.IsDefault;
            this.Linkman = address.Linkman;
            this.Mobile = address.Mobile;
        }

        #endregion

    }
}