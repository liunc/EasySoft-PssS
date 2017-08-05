
// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据传输对象类库
// 创 建 人：刘年超
// 创建时间：2017-07-18
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Application.DataTransfer.Customer
{
    /// <summary>
    /// 客户地址数据传输对象类
    /// </summary>
    public class CustomerAddressDTO
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置是否为默认地址的标记
        /// </summary>
        public string IsDefault { get; set; }
    }
}
