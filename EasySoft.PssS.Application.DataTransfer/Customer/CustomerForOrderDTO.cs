
// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：数据传输对象类库
// 创 建 人：刘年超
// 创建时间：2017-08-03
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.Application.DataTransfer.Customer
{
    using System.Collections.Generic;

    /// <summary>
    /// 用于下单的客户数据传输对象类
    /// </summary>
    public class CustomerForOrderDTO
    {
        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 获取或设置手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置所属分组Id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 获取或设置地址列表
        /// </summary>
        public List<CustomerAddressDTO> CustomerAddressList { get; set; }
    }
}
