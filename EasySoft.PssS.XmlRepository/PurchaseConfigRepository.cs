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
    /// 采购配置仓储实现类
    /// </summary>
    public class PurchaseConfigRepository : XmlRepositoryBase, IPurchaseConfigRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PurchaseConfigRepository()
        {
            this.XmlFileName = "PurchaseConfig";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项信息
        /// </summary>
        /// <returns>返回采购项信息</returns>
        public PurchaseConfig GetPurchaseConfig()
        {
            XmlNodeList nodeList = this.DataSource.SelectNodes("//Category");
            if (nodeList == null)
            {
                return null;
            }
            PurchaseConfig config = new PurchaseConfig();
            config.PurchaseItems = new List<PurchaseItem>();
            config.CostItems = new List<CostItem>();
            foreach (XmlNode node in nodeList)
            {
                PurchaseCategory category = (PurchaseCategory)Enum.Parse(typeof(PurchaseCategory), this.GetXmlNodeAttribute(node, "Code"), true);
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    config.PurchaseItems.Add(new PurchaseItem
                    {
                        Category = category,
                        Code = this.GetXmlNodeAttribute(childNode, "Code"),
                        Unit = this.GetXmlNodeAttribute(childNode, "Unit"),
                        Valid = this.GetXmlNodeAttribute(childNode, "Valid"),
                        Name = childNode.InnerText.Trim()
                    });
                }
            }
            nodeList = this.DataSource.SelectNodes("//Cost/Item");
            foreach (XmlNode node in nodeList)
            {
                config.CostItems.Add(new CostItem
                {
                    Code = this.GetXmlNodeAttribute(node, "Code"),
                    Valid = this.GetXmlNodeAttribute(node, "Valid"),
                    Name = node.InnerText.Trim()
                });
            }
            return config;
        }

        #endregion
    }
}
