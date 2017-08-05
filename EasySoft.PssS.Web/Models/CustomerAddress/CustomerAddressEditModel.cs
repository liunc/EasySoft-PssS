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
    using Core.Util;
    using Newtonsoft.Json;
    using Resources;
    using Entity = EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 编辑客户地址视图模型类
    /// </summary>
    public class CustomerAddressEditModel
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

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerAddressEditModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="address">客户地址领域实体对象</param>
        public CustomerAddressEditModel(Entity.CustomerAddress address)
        {
            this.Id = address.Id;
            this.Address = address.Address;
            this.Linkman = address.Linkman;
            this.Mobile = address.Mobile;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(Validate validate)
        {
            this.Id = validate.CheckInputString(WebResource.Field_Id, this.Id, true, Constant.STRING_LENGTH_32);
            this.Linkman = validate.CheckInputString(WebResource.Field_Linkman, this.Linkman, true, Constant.STRING_LENGTH_10);
            this.Mobile = validate.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, Constant.STRING_LENGTH_16);
            this.Address = validate.CheckInputString(WebResource.Field_Address, this.Address, true, Constant.STRING_LENGTH_100);
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns>返回字符串</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion

    }
}