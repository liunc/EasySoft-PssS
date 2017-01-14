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
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Xml文件仓储基类
    /// </summary>
    public abstract class XmlRepositoryBase
    {
        #region 变量

        private XmlDocument dataSource = null;

        #endregion

        #region 属性

        /// <summary>
        /// 获取Xml文件路径
        /// </summary>
        protected abstract string XmlFilePath { get; }

        /// <summary>
        /// 获取Xml数据源
        /// </summary>
        protected XmlDocument DataSource
        {
            get
            {
                if (this.dataSource == null)
                {
                    if (File.Exists(this.XmlFilePath))
                    {
                        this.dataSource = new XmlDocument();
                        this.dataSource.Load(this.XmlFilePath);
                    }
                    else
                    {
                        this.dataSource = this.CreateXml();
                    }
                }
                return this.dataSource;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取Xml文件路径
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回Xml文件路径字符串</returns>
        protected string GetXmlFilePath(string fileName)
        {
            return string.Format("{0}InitialData\\XML\\{1}.xml", System.AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        /// <summary>
        /// 初始化Xml文件
        /// </summary>
        /// <returns>返回XDocument对象</returns>
        private XmlDocument CreateXml()
        {
            StringBuilder xmlBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\" ?>");
            xmlBuilder.AppendLine("<root>");
            xmlBuilder.AppendLine("</root>");

            XmlDocument doc = new XmlDocument(); 
            doc.LoadXml(xmlBuilder.ToString());   
            doc.Save(this.XmlFilePath);
            return doc;
        }

        /// <summary>
        /// 获取XmlNode的Attribute值
        /// </summary>
        /// <param name="node">XmlNode</param>
        /// <param name="attributeName">Attribute名称</param>
        /// <returns>返回Attribute值</returns>
        protected string GetXmlNodeAttribute(XmlNode node, string attributeName)
        {
            return node.Attributes[attributeName] == null ? string.Empty : node.Attributes[attributeName].Value.Trim();
        }

        #endregion;
    }
}
