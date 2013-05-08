using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DemoApp
{
    static class frmConfig
    {
        public static string dbServer;
        public static string dbUser;
        public static string dbPassword;
        public static void loadConfig()
        {
            using (StreamReader sw = new StreamReader("config.ini"))
            {
                dbServer = sw.ReadLine();
                dbUser = sw.ReadLine();
                dbPassword = sw.ReadLine();
            }
        }
        public static string checkFile()
        {
            string html;
            if (File.Exists("error.error"))
            {
                MessageBoxButtons mess = MessageBoxButtons.OKCancel;
                if (MessageBox.Show("上次异常退出", "是否恢复", mess) == DialogResult.OK)
                {
                    using (StreamReader sw = new StreamReader("~save.temp"))
                    {
                        html = sw.ReadToEnd();
                        sw.Close();
                    }
                    return html;
                }
            }
            return "";
        }
    }
}
