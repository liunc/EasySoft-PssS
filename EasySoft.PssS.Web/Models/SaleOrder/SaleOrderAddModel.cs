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
namespace EasySoft.PssS.Web.Models.SaleOrder
{
    /// <summary>
    /// 出库第一步视图模型类
    /// </summary>
    public class SaleOrderAddModel
    {
        #region 属性

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 获取或设置联系人
        /// </summary>
        public string LinkMan { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SaleOrderAddModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">ID</param>
        public SaleOrderAddModel(string id, string linkMan)
        {
            this.Id = id;
            this.LinkMan = linkMan;
        }
        
        #endregion
    }
}