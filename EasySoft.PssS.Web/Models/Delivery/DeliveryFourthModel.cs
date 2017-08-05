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
namespace EasySoft.PssS.Web.Models.Delivery
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.Cost;
    using EasySoft.PssS.Web.Resources;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 出库第四步视图模型类
    /// </summary>
    public class DeliveryFourthModel
    {
        private string currentDate;

        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public string CurrentDate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.currentDate))
                {
                    this.currentDate = DataConvert.ConvertDateToString(DataConvert.ConvertNowUTCToBeijing());
                }
                return this.currentDate;
            }
            set
            {
                this.currentDate = value;
            }
        }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public string DateString { get; set; }

        /// <summary>
        /// 获取或设置日期字符串
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 获取或设置快递公司
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        /// 获取或设置快递单号
        /// </summary>
        public string ExpressBill { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置成本明细
        /// </summary>
        public List<CostAddModel> Costs { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryFourthModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">id</param>
        public DeliveryFourthModel(string id) : this()
        {
            this.Id = id;
        }

        #endregion

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            this.Date = validate.CheckDateString(WebResource.Field_Date, this.DateString, true);
            validate.CheckDictionary<string, string>(WebResource.Field_ExpressCompany, this.ExpressCompany, ParameterHelper.GetExpressCompany());
            this.ExpressBill = validate.CheckInputString(WebResource.Field_ExpressBill, this.ExpressBill, true, Constant.STRING_LENGTH_100);
            this.Remark = validate.CheckInputString(WebResource.Field_Remark, this.Remark, false, Constant.STRING_LENGTH_100);

            foreach (CostAddModel cost in this.Costs)
            {
                cost.Category = CostItemCategory.Delivery;
                cost.PostValidate(ref validate);
            }
        }
    }
}