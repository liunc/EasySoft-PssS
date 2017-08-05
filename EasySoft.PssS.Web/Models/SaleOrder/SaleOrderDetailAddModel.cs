// ----------------------------------------------------------
// 系统名称：EasySoft Core
// 项目名称：Web
// 创 建 人：刘年超
// 创建时间：2017-07-18
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
using EasySoft.Core.Util;
using EasySoft.PssS.Web.Resources;

namespace EasySoft.PssS.Web.Models.SaleOrder
{
    /// <summary>
    /// 新增销售数据传输对象类
    /// </summary>
    public class SaleOrderDetailAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置客户ID
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
        /// 获取或设置是否需要快递
        /// </summary>
        public string Selected { get; set; }

        #endregion

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            validate.CheckInputString(WebResource.Field_Id, this.Id, true, Constant.STRING_LENGTH_32);
            validate.CheckInputString(WebResource.Field_CustomerId, this.CustomerId, true, Constant.STRING_LENGTH_32);
            validate.CheckInputString(WebResource.Field_Linkman, this.Linkman, true, Constant.STRING_LENGTH_10);
            validate.CheckInputString(WebResource.Field_Mobile, this.Mobile, true, Constant.STRING_LENGTH_16);
            validate.CheckInputString(WebResource.Field_Address, this.Address, true, Constant.STRING_LENGTH_100);

        }
    }
}
