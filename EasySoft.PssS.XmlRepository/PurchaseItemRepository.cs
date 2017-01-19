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
    /// 采购项仓储实现类
    /// </summary>
    public class PurchaseItemRepository : XmlRepositoryBase, IPurchaseItemRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseItemRepository()
        {
            this.XmlFileName = "PurchaseItem";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项信息
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="onlyValid">是否仅包含有效</param>
        /// <returns>返回采购项信息</returns>
        public List<PurchaseItem> GetPurchaseItem(string category, bool onlyValid)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("Purchase category");
            }
            string xpath = string.Empty;
            XmlNodeList nodeList = this.DataSource.SelectNodes(string.Format("//PurchaseCategory[@Code='{0}']/Item{1}", category, onlyValid ? "[@Valid='1']" : string.Empty));
            if (nodeList == null)
            {
                return null;
            }
            List<PurchaseItem> items = new List<PurchaseItem>();
            PurchaseCategory enumCategory = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), category);
            foreach (XmlNode node in nodeList)
            {
                items.Add(new PurchaseItem
                {
                    Category = enumCategory,
                    Code = this.GetXmlNodeAttribute(node, "Code"),
                    InputUnit = this.GetXmlNodeAttribute(node, "InputUnit"),
                    OutputUnit = this.GetXmlNodeAttribute(node, "OutputUnit"),
                    InOutRate = this.GetXmlNodeAttribute(node, "InOutRate"),
                    Valid = this.GetXmlNodeAttribute(node, "Valid"),
                    Name = node.InnerText.Trim()
                });
            }
            return items;
        }

        #endregion
    }
}
