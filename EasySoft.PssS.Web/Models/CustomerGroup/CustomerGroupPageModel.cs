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
namespace EasySoft.PssS.Web.Models.CustomerGroup
{
    using EasySoft.PssS.Domain.Entity;

    /// <summary>
    /// 客户分组数据视图模型类
    /// </summary>
    public class CustomerGroupPageModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置是否为默认设置
        /// </summary>
        public string IsDefault { get; set; }

        /// <summary>
        /// 获取或设置备注
        /// </summary>
        public string Remark { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerGroupPageModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">客户分组领域实体对象</param>
        public CustomerGroupPageModel(CustomerGroup entity)
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.IsDefault = entity.IsDefault;
            this.Remark = entity.Remark;
        }

        #endregion
    }
}