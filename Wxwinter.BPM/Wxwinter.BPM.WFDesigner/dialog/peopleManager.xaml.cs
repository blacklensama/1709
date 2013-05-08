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
        public peopleManager()
        {
            InitializeComponent();
        }

        public void GetPeopleDate(string str)
        {
            models.Clear();
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            try
            {
                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "select tb_per.User_Name , tb_user.User_Mail from tb_user, tb_per where tb_per.user_name = tb_user.User_Name and tb_per.user_authority = ";
                sql += "'" + str + "'";
                //MessageBox.Show(sql);
                MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

                DataTable ds = new DataTable();
                mda.Fill(ds);
                DBConn.Close();

                foreach (DataRow dr in ds.Rows)
                {
                    peopleInfo wfm = new peopleInfo();
                    wfm.peopleName = dr["user_name"].ToString();
                    wfm.peopleEmail = dr["User_Mail"].ToString();       
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
            peopleList.Items.Add("管理员");
            peopleList.Items.Add("专业用户");
            peopleList.Items.Add("普通用户");
        }

        private void select_change(object sender, SelectionChangedEventArgs e)
        {
            string str = peopleList.SelectedItem.ToString();
            if (str == "管理员")
            {
                GetPeopleDate("admin");
            }
            else if (str == "专业用户")
            {
                GetPeopleDate("special");
            }
            else if (str == "普通用户")
            {
                GetPeopleDate("normal");
            }
        }
    }
}
