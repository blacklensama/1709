using MySQLDriverCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wxwinter.BPM.WFDesigner.model;

namespace Wxwinter.BPM.WFDesigner.dialog
{
    /// <summary>
    /// newGroup.xaml 的交互逻辑
    /// </summary>
    public partial class newGroup : Window
    {
        public newGroup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "insert into tb_per values ( '" + peopleName.Text.ToString() + "','" + des.Text.ToString() + "','" + pow.Text.ToString() + "')";
                MySQLCommand mda = new MySQLCommand(sql, DBConn);

                mda.ExecuteNonQuery();
                mda.Dispose();
                DBConn.Close();
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }
    }
}
