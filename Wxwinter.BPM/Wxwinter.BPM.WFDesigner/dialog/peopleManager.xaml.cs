using MySQLDriverCS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// peopleManager.xaml 的交互逻辑
    /// </summary>
    public partial class peopleManager : Window
    {
        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<peopleInfo> models = new ObservableCollection<peopleInfo>();
        string nowUser;
        public peopleManager()
        {
            InitializeComponent();
        }

        public void GetPeopleDate(string str)
        {
            nowUser = str;
            models.Clear();
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "SELECT * FROM tb_user WHERE tb_user.User_Dept = ";
                sql += "'" + str + "'";
                MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

                DataTable ds = new DataTable();
                mda.Fill(ds);
                DBConn.Close();

                foreach (DataRow dr in ds.Rows)
                {
                    peopleInfo wfm = new peopleInfo();
                    wfm.peopleName = dr["user_name"].ToString();
                    wfm.peopleEmail = dr["User_Mail"].ToString();
                    wfm.peopleJob = dr["User_Job"].ToString();
                    wfm.peopleDept = dr["User_Dept"].ToString();
                    wfm.peoplePassword = dr["User_Password"].ToString();
                    wfm.peopleCell = dr["User_Cell"].ToString();
                    models.Add(wfm);
                }
                view.Source = models;
                this.peopleListView.DataContext = view;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        private void testLoad(object sender, RoutedEventArgs e)
        {
            getinformation();
        }

        private void getinformation()
        {
            peopleList.Items.Clear();
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
                    peopleList.Items.Add(dr["display_name"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        private void select_change(object sender, SelectionChangedEventArgs e)
        {
            if (peopleList.SelectedItem == null)
            {
                return;
            }
            string str = peopleList.SelectedItem.ToString();
            GetPeopleDate(str);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newPeople people = new newPeople();
            people.ShowDialog();
            people.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = ((peopleInfo)(peopleListView.SelectedItem)).peopleName;
            MessageBox.Show(name);
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "delete from tb_user where User_Name = " + "'" + name + "'";
                MySQLCommand mda = new MySQLCommand(sql, DBConn);

                mda.ExecuteNonQuery();
                mda.Dispose();
                DBConn.Close();
                GetPeopleDate(nowUser);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            newGroup gp = new newGroup();
            gp.ShowDialog();
            testLoad(null, null);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string name = peopleList.SelectedItem.ToString();
            if (name == "")
            {
                return;
            }
            peopleList.Items.Remove(peopleList.SelectedItem);
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "delete from tb_per where display_name = " + "'" + name + "'";
                MySQLCommand mda = new MySQLCommand(sql, DBConn);
                //MessageBox.Show(sql);
                mda.ExecuteNonQuery();
                sql = "UPDATE tb_user set User_Dept = '未分组' where User_Dept = '" + name + "'";
                mda = new MySQLCommand(sql, DBConn);
                MessageBox.Show(sql);
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

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string name = ((peopleInfo)(peopleListView.SelectedItem)).peopleName;
            newPeople np = new newPeople();
            np.setInf(name);
            np.Show();
        }
    }
}
