using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
namespace DemoApp.eq_controls
{
    public class configLoader
    {
        private static XmlElement configRoot = null ;
        public static string getConfigFile()
        {
          string path = Application.StartupPath;
          if (!path.EndsWith("\\"))
              path = path + "\\";
               
            string fname = path + "config.xml";

            return fname ;
        }
        public static void loadConfig()
        {
            configRoot  = null ;

            string fname = getConfigFile();
            if (!System.IO.File.Exists(fname))
                return;

              configRoot = xmlTool.getRootElement(fname);
           
        }
        public static string getDBString()
        {
            if (configRoot == null)
            {
                loadConfig();
            }
            if (configRoot == null)
                return "";

            XmlElement dbE = xmlTool.getFirstChildByTagName(configRoot, "database");
            if (dbE == null)
                return "";
            return dbE.InnerXml;
            
        }
        public static string getDBUserName()
        {
            if (configRoot == null)
            {
                loadConfig();
            }
            if (configRoot == null)
            {
                return "";
            }
            XmlElement dbe = xmlTool.getFirstChildById(configRoot, "username");
            if (dbe == null)
            {
                return "";
            }
            return dbe.InnerXml;
        }
        public static string getDBPassword()
        {
            if (configRoot == null)
            {
                loadConfig();
            }
            if (configRoot == null)
            {
                return "";
            }
            XmlElement dbe = xmlTool.getFirstChildById(configRoot, "password");
            if (dbe == null)
            {
                return "";
            }
            return dbe.InnerXml;
        }
    }
}
