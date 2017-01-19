// ----------------------------------------------------------
// 系统名称：EasySoft PssS
// 项目名称：Xml文件仓储类库
// 创 建 人：刘年超
// 创建时间：2017-01-14
// ----------------------------------------------------------
// 修改记录：
// 
// 
// ----------------------------------------------------------
// 版权所有：易则科技工作室 
// ----------------------------------------------------------
namespace EasySoft.PssS.XmlRepository
{
    using EasySoft.PssS.Domain.Entity;
    using EasySoft.PssS.Domain.ValueObject;
    using EasySoft.PssS.Repository;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// 成本项仓储实现类
    /// </summary>
    public class CostItemRepository : XmlRepositoryBase, ICostItemRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CostItemRepository()
        {
            this.XmlFileName = "CostItem";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取成本项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回成本项信息</returns>
        public List<CostItem> GetCostItem(string category, bool onlyValid)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("Cost category");
            }
            string xpath = string.Empty;
            XmlNodeList nodeList = this.DataSource.SelectNodes(string.Format("//CostCategory[@Code='{0}']/Item{1}", category, onlyValid ? "[@Valid='1']" : string.Empty));
            if (nodeList == null)
            {
                return null;
            }
            List<CostItem> items = new List<CostItem>();
            CostCategory enumCategory = (CostCategory)Enum.Parse(typeof(CostCategory), category);
            foreach (XmlNode node in nodeList)
            {
                items.Add(new CostItem
                {
                    Category = enumCategory,
                    Code = this.GetXmlNodeAttribute(node, "Code"),
                    Name = node.InnerText.Trim()
                });
            }
            return items;
        }

        #endregion
    }
}
