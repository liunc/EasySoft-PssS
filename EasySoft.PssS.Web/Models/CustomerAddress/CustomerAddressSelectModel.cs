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
    public class CustomerAddressSelectModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置客户Id
        /// </summary>
        public string CustomerId { get; set; }

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
        /// 获取或设置是否需要快递
        /// </summary>
        public string NeedExpress { get; set; }

        /// <summary>
        /// 获取或设置是否选中
        /// </summary>
        public string Selected { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressSelectModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="address">客户地址领域实体对象</param>
        public CustomerAddressSelectModel(Entity.CustomerAddress address)
        {
            this.Id = address.Id;
            this.CustomerId = address.CustomerId;
            this.Address = address.Address;
            this.Linkman = address.Linkman;
            this.Mobile = address.Mobile;
        }

        #endregion

    }
}