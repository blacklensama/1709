using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wxwinter.BPM.WFDesigner.model
{
    class Configuration
    {
        public static void setDBIp(String ip){
            EditConfig.ConfigSetValue("databaseIp",ip);
        }
        public static String getDBIp(){
            EditConfig ec = new EditConfig();
            return ec.ConfigGetValue("databaseIp");
        }
        public static void setDBUsername(String username){
            EditConfig.ConfigSetValue("databaseUsername",username);
        }
        public static String getDBUsername(){
            EditConfig ec = new EditConfig();
            return ec.ConfigGetValue("databaseUsername");
        }
        public static void setDBPassword(String password)
        {
            EditConfig.ConfigSetValue("databasePassword", password);
        }
        public static String getDBPassword()
        {
            EditConfig ec = new EditConfig();
            return ec.ConfigGetValue("databasePassword");
        }

    }
}
