using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Collections;
using System.Collections.Generic;

namespace DemoApp.eq_controls.dbTools
{
    public class dbTool
    {
       // public static string connectionStr = ("Driver={mySQL ODBC 3.51 Driver};Server=192.168.61.100;Port=3306;Option=4;Database=template;Uid=root;Pwd=123456;");
        private static string getConnecStr()
        {
            string s = "Driver={mySQL ODBC 3.51 Driver};Server=";
            s += configLoader.getDBString();
            s += ";Port=3306;Option=4;Database=template;Uid=";
            s += configLoader.getDBUserName();
            s += ";Pwd=";
            s += configLoader.getDBPassword();
            s += ";";
            return s;
        }
        private static OdbcConnection getCon()
        {
            OdbcConnection con = null;
            try
            {
                con = new OdbcConnection(getConnecStr());
                con.Open();
                return con;
            }
            catch (Exception exp)
            {
            }
            return null;
        }
        public static bool saveTemplate(string name , string html , string description , string uid)
        {
            OdbcConnection con = getCon();
            try
            {
                
                if (con == null)
                {
                    System.Windows.Forms.MessageBox.Show("无法建立连接，请检查数据库配置");
                    return false;
                }
                OdbcCommand encodcommand = new OdbcCommand("set names gb2312", con);
                encodcommand.ExecuteNonQuery();
                encodcommand = null;
                //先测试有没有重名
                string html2 = "<label id=\"a\">test</label>";
                string sqlTest = "select * from template where name = '" + name + "'";
                OdbcCommand mycmTest = new OdbcCommand(sqlTest, con);
                OdbcDataReader msdr = (OdbcDataReader)mycmTest.ExecuteReader();
                string sql = "insert into template (name,htmlsource,description,userid) values ('" + name + "','" + html + "','" + description + "','" + uid + "') ";
                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {
                        if (System.Windows.Forms.MessageBox.Show(name + "模版已存在，是否覆盖已有记录内容？", "确认覆盖", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            sql = "update template set htmlsource = '" + html + "' where name = '" + name + "'";
                        else
                        {
                            msdr.Close();
                            con.Close();
                            return false;
                        }
                    }
                }

                msdr.Close();
                OdbcCommand mycm = new OdbcCommand(sql, con);
                mycm.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception exp)
            {

            }
            return false;
        }
        public static bool deleteTemplete(string name, string html, string description, string uid)
        {
            OdbcConnection con = getCon();
            try
            {
                if (con == null)
                {
                    System.Windows.Forms.MessageBox.Show("无法建立连接，请检查数据库配置");
                    return false;
                }
                OdbcCommand encodcommand = new OdbcCommand("set names gb2312", con);
                encodcommand.ExecuteNonQuery();
                encodcommand = null;
                string sqlTest = "select * from template where name = '" + name + "'";
                OdbcCommand mycmTest = new OdbcCommand(sqlTest, con);
                OdbcDataReader msdr = (OdbcDataReader)mycmTest.ExecuteReader();
                string sql = "insert into template (name,htmlsource,description,userid) values ('" + name + "','" + html + "','" + description + "','" + uid + "') ";
                while (msdr.Read())
                {
                    if (msdr.HasRows)
                    {
                        if (System.Windows.Forms.MessageBox.Show(name + "模版已存在，是否覆盖已有记录内容？", "确认覆盖", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                            sql = "delete template set htmlsource = '" + html + "' where name = '" + name + "'";
                        else
                        {
                            msdr.Close();
                            con.Close();
                            return false;
                        }
                        msdr.Close();
                        OdbcCommand mycm = new OdbcCommand(sql, con);
                        mycm.ExecuteNonQuery();

                        con.Close();
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            return false;
        }
        public static List<string> getTemplateNameList()
        {
           
            OdbcConnection con = getCon();
            if(con == null )
                return null ;
            OdbcCommand encodcommand = new OdbcCommand("set names gb2312", con);
            encodcommand.ExecuteNonQuery();
            encodcommand = null;
            string sql = "select name from template ";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            OdbcDataReader msdr = (OdbcDataReader)mycm.ExecuteReader();
            List<string> result = new List<string>();
            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    //string[] linkdef = new  string[] { "", "", "","" };
                    
                    string name = msdr.GetString(0);
                    //string html = msdr.GetString(1);
                    //string desp = msdr.GetString(2);
                    //string userID = msdr.GetString(3);
                    //linkdef[0] = name;
                    //linkdef[1] = html;
                    //linkdef[2] = desp;
                    //linkdef[3] = userID;
                    
                    result.Add(name);
                    
                }
            }
          
            msdr.Close();
            con.Close();
            return result;
        }

        public static string[]  getTemplate(string name)
        {

            OdbcConnection con = getCon();
            if (con == null)
                return null;
            OdbcCommand encodcommand = new OdbcCommand("set names gb2312", con);
            encodcommand.ExecuteNonQuery();
            encodcommand = null;
            string sql = "select * from template where name='"+name+"' ";
            OdbcCommand mycm = new OdbcCommand(sql, con);
            OdbcDataReader msdr = (OdbcDataReader)mycm.ExecuteReader();
            string[] result = null;

            while (msdr.Read())
            {
                if (msdr.HasRows)
                {
                    //string[] linkdef = new  string[] { "", "", "","" };

                    string tname = msdr.GetString(0);
                    string html = msdr.GetString(1);
                    string desp = msdr.GetString(2);
                    //string userID = msdr.GetString(3);
                    //linkdef[0] = name;
                    //linkdef[1] = html;
                    //linkdef[2] = desp;
                    //linkdef[3] = userID;
                    result = new string[] {tname , html , desp };
                    break;

                }
            }

            msdr.Close();
            con.Close();
            return result;
        }
    }
}
