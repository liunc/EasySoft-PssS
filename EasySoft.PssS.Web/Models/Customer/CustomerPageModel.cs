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

    /// <summary>
    /// 客户分组数据视图模型类
    /// </summary>
    public class CustomerPageModel
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
        public string Mobile { get; set; }

        /// <summary>
        /// 获取或设置分组ID
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 获取或设置分组名称 
        /// </summary>
        public string GroupName { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomerPageModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity">客户分组领域实体对象</param>
        public CustomerPageModel(Customer entity) : this()
        {
            this.Id = entity.Id;
            this.Name = entity.Name;
            this.Mobile = entity.Mobile;
            this.GroupId = entity.GroupId;
        }

        #endregion
    }
}