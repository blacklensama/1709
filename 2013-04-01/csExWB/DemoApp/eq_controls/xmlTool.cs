using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace DemoApp.eq_controls
{
    public class xmlTool
    {
        public static XmlElement getRootElement(String fileName)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(fileName);
                if (!xdoc.HasChildNodes)
                    return null;

                int iNumChildNodes = xdoc.ChildNodes.Count;
                for (int i = 0; i < iNumChildNodes; i++)
                {
                    XmlNode childXmlNode = xdoc.ChildNodes[i];
                    switch (childXmlNode.NodeType)
                    {
                        case XmlNodeType.Document: break;
                        case XmlNodeType.XmlDeclaration: break;
                        case XmlNodeType.Element: return (XmlElement)(childXmlNode);


                    }
                }

            }
            catch (Exception e)
            {
                //   System.Windows.Forms.MessageBox.Show("fail to load " + fileName);
                return null;
            }
            return null;

        }
        public static void update(XmlElement e)
        {
            if (e == null)
                return;
            string s = "file:///";
            string path = e.OwnerDocument.BaseURI;
            if (e.OwnerDocument.BaseURI.StartsWith(s))
            {
                path = path.Substring(s.Length);
            }

            saveElement(path, e);
        }
        public static XmlElement selectSingleInformationByXmlPath(XmlElement root, string xmlpath)
        {



            try
            {
                XmlNode list = root.SelectSingleNode(xmlpath);

                if (list is XmlElement)
                {
                    XmlElement e = (XmlElement)list;
                    return e;
                }

            }
            catch (Exception exp)
            {

            }
            return null;
        }
        public static List<XmlElement> selectInformationByXmlPath2(XmlElement root, string xmlpath)
        {


            List<XmlElement> result = new List<XmlElement>();

            try
            {
                XmlNodeList list = root.SelectNodes(xmlpath);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is XmlElement)
                    {
                        XmlElement e = (XmlElement)list[i];
                        result.Add(e);
                    }
                }
            }
            catch (Exception exp)
            {

            }
            return result;
        }
        public static List<XmlElement> selectInformationByXmlPath(XmlElement root, string xmlpath)
        {


            List<XmlElement> result = new List<XmlElement>();
            XmlDocument doc = root.OwnerDocument;
            try
            {
                XmlNodeList list = doc.SelectNodes(xmlpath);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] is XmlElement)
                    {
                        XmlElement e = (XmlElement)list[i];
                        result.Add(e);
                    }
                }
            }
            catch (Exception exp)
            {

            }
            return result;
        }
        public static List<XmlElement> getAllChildsByID(XmlElement parent, string id)
        {
            return getAllChildsByAttribute(parent, "id", id);
        }
        public static List<XmlElement> getAllChildsByAttribute(XmlElement parent, string aname, string avalue)
        {
            List<XmlElement> res = new List<XmlElement>();
            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                XmlNode childNode = parent.ChildNodes[i];
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;
                XmlElement e = (XmlElement)childNode;
                if (e.GetAttribute(aname).Equals(avalue))
                    res.Add(e);


            }
            return res;
        }

        public static XmlElement getFirstChildById(XmlElement parent, string id)
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                XmlNode childNode = parent.ChildNodes[i];
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;
                string childName = childNode.LocalName;
                if (childName == null)
                    continue;
                string iid = getAttributeValue(childNode, "id");
                if (iid.Equals(id))
                    return (XmlElement)(childNode);


            }
            return null;
        }
        public static XmlElement getFirstChildByName(XmlElement parent, string name)
        {
            if (parent == null)
                return null;

            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                XmlNode childNode = parent.ChildNodes[i];
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;
                string childName = childNode.LocalName;
                if (childName == null)
                    continue;
                string iname = getAttributeValue(childNode, "name");
                if (iname.Equals(name))
                    return (XmlElement)(childNode);


            }
            return null;
        }
        public static List<XmlElement> getAllChildByTagName(XmlElement parent, string tname)
        {
            List<XmlElement> res = new List<XmlElement>();
            if (parent == null)
                return res;
            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                XmlNode childNode = parent.ChildNodes[i];
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;
                string childName = childNode.LocalName;
                if (childName == null)
                    continue;
                if (childName.Equals(tname))
                    res.Add((XmlElement)(childNode));


            }
            return res;
        }
        public static XmlElement getFirstChildByTagName(XmlElement parent, string tname)
        {

            if (parent == null)
                return null;
            for (int i = 0; i < parent.ChildNodes.Count; i++)
            {
                XmlNode childNode = parent.ChildNodes[i];
                if (childNode.NodeType != XmlNodeType.Element)
                    continue;
                string childName = childNode.LocalName;
                if (childName == null)
                    continue;
                if (childName.Equals(tname))
                    return (XmlElement)(childNode);


            }
            return null;
        }
        public static string getAttributeValue(XmlNode node, string name)
        {
            if (node == null)
                return "";
            if (node.Attributes.Count <= 0)
                return "";
            for (int i = 0; i < node.Attributes.Count; i++)
            {
                XmlAttribute attribute = node.Attributes[i];
                if (attribute == null)
                    return "";
                if (attribute.LocalName.Equals(name))
                    return getAttributeValue(attribute);

            }
            return "";

        }
        public static string getAttributeValue(XmlAttribute attribute)
        {
            if (attribute.Equals(""))
                return "";
            if (attribute.ChildNodes.Count != 1)
                return "";
            XmlNode valueNode = attribute.ChildNodes[0];
            string valueName = valueNode.Value;
            if (valueName == null || valueName.Equals(""))
                return "";
            return valueName;
        }
        public static string getValue(XmlNode node)
        {
            if (node == null)
                return "";
            if (node.ChildNodes.Count != 1)
                return "";
            XmlNode valueNode = node.ChildNodes[0];
            string valueName = valueNode.Value;
            if (valueName == null || valueName.Equals(""))
                return "";
            return valueName;
        }
        public static void saveElement(string fn, XmlElement e)
        {
            XmlDocument xdoc = e.OwnerDocument;
            string codding = "GB18030";
            if (xdoc.InnerXml.IndexOf("encoding=\"GB18030\"") <= 0)
                codding = "UTF-8";
            XmlTextWriter writer = new XmlTextWriter(fn, Encoding.GetEncoding(codding));
            writer.Formatting = Formatting.Indented;
            xdoc.WriteTo(writer);
            writer.Flush();
            writer.Close();

        }
        public static XmlElement ConvertStringToXml(string xmlString)
        {
            XmlDocument xdoc = new XmlDocument();

            string head = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
            if (xmlString.StartsWith("<?xml"))
                head = "";
            try
            {
                xdoc.LoadXml(head + xmlString);

                int iNumChildNodes = xdoc.ChildNodes.Count;
                for (int i = 0; i < iNumChildNodes; i++)
                {
                    XmlNode childXmlNode = xdoc.ChildNodes[i];
                    switch (childXmlNode.NodeType)
                    {
                        case XmlNodeType.Document: break;
                        case XmlNodeType.XmlDeclaration: break;
                        case XmlNodeType.Element: return (XmlElement)(childXmlNode);


                    }
                }


            }
            catch (Exception excp)
            {
                return null;
            }
            return null;
        }
    }
}
