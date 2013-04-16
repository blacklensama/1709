using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Wxwinter.BPM.WFDesigner.model
{
    class EditConfig
    {
        public EditConfig()
        {
            ///
            /// TODO: 在此处添加构造函数逻辑
            ///
        }
        ///
        /// 写操作
        ///
        ///
        ///
        ///
        public static void ConfigSetValue(string AppKey, string AppValue)
        {
            XmlDocument xDoc = new XmlDocument();
            //获取可执行文件的路径和名称
            xDoc.Load(System.Windows.Forms.Application.StartupPath + "\\userconfig.config");
        
            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//configuration");
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null)
                xElem1.SetAttribute("value", AppValue);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(System.Windows.Forms.Application.StartupPath + "\\userconfig.config");

        }
        ///
        /// 读操作
        ///
        ///
        ///
        ///
        public string ConfigGetValue(string appKey)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(System.Windows.Forms.Application.StartupPath + "\\userconfig.config");

                XmlNode xNode;
                XmlElement xElem;
                xNode = xDoc.SelectSingleNode("//configuration");
                xElem = (XmlElement)xNode.SelectSingleNode("//add[@key='" + appKey + "']");
                if (xElem != null)
                    return xElem.GetAttribute("value");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
   
}
