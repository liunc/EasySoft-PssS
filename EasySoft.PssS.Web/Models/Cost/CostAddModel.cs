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
namespace EasySoft.PssS.Web.Models.Cost
{
    using EasySoft.Core.Util;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Web.Models.CostItem;
    using EasySoft.PssS.Web.Resources;

    /// <summary>
    /// 成本项视图模型类
    /// </summary>
    public class CostAddModel
    {

        /// <summary>
        /// 获取或设置成本项代码
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// 获取或设置金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 获取或设置成本分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 提交验证
        /// </summary>
        /// <param name="errorMessages">返回的错误信息</param>
        public void PostValidate(ref Validate validate)
        {
            validate.CheckStringArgument(WebResource.Field_CostCategory, this.Category, true);
            if (string.IsNullOrWhiteSpace(this.Category))
            {
                validate.CheckDictionary<string, CostItemCacheModel>(WebResource.Field_CostItem, this.Item, ParameterHelper.GetCostItem(this.Category));
                validate.CheckDecimal(WebResource.Field_CostItemMoney, this.Money, Constant.DECIMAL_ZERO, Constant.DECIMAL_MAX);
            }
        }
    }
}