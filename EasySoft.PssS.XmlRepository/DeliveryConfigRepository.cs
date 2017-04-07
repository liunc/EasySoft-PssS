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
    /// 交付配置仓储实现类
    /// </summary>
    public class DeliveryConfigRepository : XmlRepositoryBase, IDeliveryConfigRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeliveryConfigRepository()
        {
            this.XmlFileName = "DeliveryConfig";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取采购项信息
        /// </summary>
        /// <returns>返回采购项信息</returns>
        public DeliveryConfig GetDeliveryConfig()
        {
            XmlNodeList nodeList = this.DataSource.SelectNodes("//Produce");
            if (nodeList == null)
            {
                return null;
            }
            DeliveryConfig config = new DeliveryConfig();
            config.Products = new List<DeliveryItem>();
            config.CostItems = new List<CostItem>();
            foreach (XmlNode node in nodeList)
            {
                bool needPack = false;
                if (this.GetXmlNodeAttribute(node, "NeedPack") == "true")
                {
                    needPack = true;
                }
                config.Products.Add(new DeliveryItem
                {
                    Code = this.GetXmlNodeAttribute(node, "Code"),
                    Unit = this.GetXmlNodeAttribute(node, "Unit"),
                    Valid = this.GetXmlNodeAttribute(node, "Valid"),
                    NeedPack = needPack,
                    PackRate = this.GetXmlNodeAttribute(node, "PackRate"),
                    Name = node.InnerText.Trim()
                });
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
