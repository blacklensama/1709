using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections;
namespace DemoApp
{
    public class DataBase2
    {
        public static string connectionStr = ("Driver={mySQL ODBC 3.51 Driver};Server=192.168.61.100;Port=3306;Option=4;Database=template;Uid=root;Pwd=123456;");

        public static Boolean deleteMapType(string maptype)
        {
            OdbcConnection con = getCon();
            string sql = "delete from MAPLINK where maptype = '" + maptype + "'";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            mycm.ExecuteNonQuery();
            return true;
        }

        public static Boolean addMapType(string maptype, string picsrc, string html, string description)
        {
            OdbcConnection con = getCon();
            //先测试有没有重名
            string sqlTest = "select * from MAPLINK where maptype = '" + maptype + "'";
            OdbcCommand mycmTest = new OdbcCommand(sqlTest, con);
            OdbcDataReader msdr = (OdbcDataReader)mycmTest.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    return false;
                }
            }

            string sql = "insert into MAPLINK(maptype,picsrc,html,description) values ('" + maptype + "','" + picsrc + "','" + html + "','" + description + "') ";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            mycm.ExecuteNonQuery();
            return true;
        }

        public static ArrayList getTemplateList()
        {
            Hashtable result = new Hashtable();
            OdbcConnection con = getCon();
            string sql = "select * from template ";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            OdbcDataReader msdr = (OdbcDataReader)mycm.ExecuteReader();
            ArrayList templateNameList = new ArrayList();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    string templateName = msdr.GetString(0);
                    templateNameList.Add(templateName);
                }
            }

            msdr.Close();
            con.Close();
            return templateNameList;
        }

        public static string getTemplate(string templateName)
        {
            string result = null;
            OdbcConnection con = getCon();
            OdbcCommand command = new OdbcCommand("set names gb2312", con);
            command.ExecuteNonQuery();

            //先测试有没有重名
            string sqlTest = "select * from TEMPLATE where NAME = '" + templateName + "'";
            OdbcCommand mycmTest = new OdbcCommand(sqlTest, con);
            OdbcDataReader msdr = (OdbcDataReader)mycmTest.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    result = msdr.GetString(1);
                }
            }
            return result;
        }

        public static Hashtable getMapTypeList()
        {
            Hashtable result = new Hashtable();
            OdbcConnection con = getCon();
            string sql = "select * from MAPLINK ";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            OdbcDataReader msdr = (OdbcDataReader)mycm.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    ArrayList propList = new ArrayList();
                    string mapType = msdr.GetString(0);
                    string picSrc = msdr.GetString(1);
                    string mapTypeHtml = msdr.GetString(2);
                    propList.Add(picSrc);
                    propList.Add(mapTypeHtml);
                    result.Add(mapType, propList);
                }
            }
            foreach (DictionaryEntry de in result)
            {
                Console.WriteLine(de.Key);// 取得键
                ArrayList propList = (ArrayList)de.Value;
                Console.WriteLine(propList[0] + "-------" + propList[1]);// 取得值
            }

            msdr.Close();
            con.Close();
            return result;
        }

        public static Boolean updateHtml(string name, string htmlSource, string description)
        {

            OdbcConnection con = getCon();
            OdbcCommand command = new OdbcCommand("set names gb2312", con);
            command.ExecuteNonQuery();

            //先测试有没有重名
            string sqlTest = "update TEMPLATE set htmlsource = '" + htmlSource + "' where NAME = '" + name + "'";
            OdbcCommand commandInsert = new OdbcCommand(sqlTest, con);
            commandInsert.ExecuteNonQuery();

            con.Close();
            return true;
        }

        public static Boolean insertHtml(string name, string htmlSource, string description)
        {

            OdbcConnection con = getCon();
            OdbcCommand command = new OdbcCommand("set names gb2312", con);
            command.ExecuteNonQuery();

            //先测试有没有重名
            string sqlTest = "select * from TEMPLATE where NAME = '" + name + "'";
            OdbcCommand mycmTest = new OdbcCommand(sqlTest, con);
            OdbcDataReader msdr = (OdbcDataReader)mycmTest.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    return false;
                }
            }

            string sql = "insert into TEMPLATE(NAME,HTMLSOURCE,DESCRIPTION) values( '" + name + "','" + htmlSource + "','" + description + "') ";
            OdbcCommand commandInsert = new OdbcCommand(sql, con);
            commandInsert.ExecuteNonQuery();

            con.Close();
            return true;
        }

        private static OdbcConnection getCon()
        {
           /* OdbcConnection con = null;
            con = new OdbcConnection(DataBase.connectionStr);
            con.Open();
            return con;*/
            return null;
        }


        public static string getValue(string targerKeyname, string targerValue, string valueKeyname)
        {
            OdbcConnection con = getCon();
            string sql = "select " + valueKeyname + " from DATALINK where " + targerKeyname + "=" + targerValue;
            OdbcCommand mycm = new OdbcCommand("select * from DATALINK", con);
            OdbcDataReader msdr = (OdbcDataReader)mycm.ExecuteReader();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    Console.WriteLine(msdr.GetString(0));
                }
            }
            msdr.Close();
            con.Close();
            return null;

        }
    }
}