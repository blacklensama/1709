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
using System.Data;
using MySQLDriverCS;
using Wxwinter.BPM.WFDesigner.model;
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.WFDesigner.dialog
{
    /// <summary>
    /// OpenUserDialog.xaml 的交互逻辑
    /// </summary>
    public partial class OpenUserGroupDialog : Window
    {
        public OpenUserGroupDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetUserData();
        }

        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<UserModel> models = new ObservableCollection<UserModel>();

        UserModel selected=null;
        public UserModel getSelected()
        {
            return selected;
        }
        //获得数据库数据
        private void GetUserData()
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            DBConn.Open();
            MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
            setformat.ExecuteNonQuery();
            setformat.Dispose();


            string sql = "select distinct User_Dept from tb_user";
            MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

            DataTable ds = new DataTable();
            mda.Fill(ds);

            DBConn.Close();
            foreach (DataRow dr in ds.Rows)
            {
                UserModel um = new UserModel();
              
                um.Department = dr["User_Dept"].ToString();
             
                models.Add(um);
            }
            view.Source = models;
            this.listView1.DataContext = view;
        }

        //取消本次操作
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //打开操作
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {


            //得到新的添加项
            selected = listView1.SelectedItem as UserModel; 

            if (selected == null)
            {
                return;
            }
            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
