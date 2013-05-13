using MySQLDriverCS;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// newPeople.xaml 的交互逻辑
    /// </summary>
    public partial class newPeople : Window
    {
        public newPeople()
        {
            InitializeComponent();
        }

        public void setInf(string name)
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "select * from tb_user where User_Name = '" + name + "'" ;
                MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

                DataTable ds = new DataTable();
                mda.Fill(ds);
                DBConn.Close();

                foreach (DataRow dr in ds.Rows)
                {
                    peopleName.Text = dr["User_Name"].ToString();
                    UserJob.Text = dr["User_Job"].ToString();
                    UserDept.Text = dr["User_Dept"].ToString();
                    UserMail.Text = dr["User_Mail"].ToString();
                    Userpass.Text = dr["User_Password"].ToString();
                    Usercell.Text = dr["User_Cell"].ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        public void loadForm()
        {
            UserDept.Items.Clear();
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "select display_name from tb_per";
                MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

                DataTable ds = new DataTable();
                mda.Fill(ds);
                DBConn.Close();

                foreach (DataRow dr in ds.Rows)
                {
                    UserDept.Items.Add(dr["display_name"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            loadForm();
        }

        private void add_people()
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString); 
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();
                string sql = "delete from tb_user where User_Name = '" + peopleName.Text.ToString() + "'";
                MessageBox.Show(sql);
                MySQLCommand mda = new MySQLCommand(sql, DBConn);
                mda.ExecuteNonQuery();
                sql = "insert into tb_user values ( '" + peopleName.Text.ToString() + "','" + UserDept.Text.ToString() + "','" + UserJob.Text.ToString() + "','" + UserMail.Text.ToString() + "','" + Usercell.Text.ToString() + "','" + Userpass.Text.ToString() + "','" + '0' + "')";
                MessageBox.Show(sql);
                mda = new MySQLCommand(sql, DBConn);
                mda.ExecuteNonQuery();
                mda.Dispose();
                DBConn.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            add_people();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
