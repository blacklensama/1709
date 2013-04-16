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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using MySQLDriverCS;
using System.Collections.ObjectModel;
using Wxwinter.BPM.WFDesigner.model;

namespace Wxwinter.BPM.WFDesigner
{
    /// <summary>
    /// UserSelection.xaml 的交互逻辑
    /// </summary>
    public partial class UserSelection : Window
    {
        public UserSelection()
        {
            InitializeComponent();
        }

        DataTable UserDataTable; //定义用户数据表
        static public DataTable UserSelectTable;
        static public bool AddFlag=false;//判断添加用户操作是否成功

        private DataTable SelectTable()
        {
            // 创建一个名为“SelectUser”的DataTable
            DataTable selectTable = new DataTable("SelectUser");

            // Add six column objects to the table.
            selectTable.Columns.Add("User_Id", typeof(String));
            selectTable.Columns.Add("User_Name", typeof(String));
            selectTable.Columns.Add("User_Dept", typeof(String));
            selectTable.Columns.Add("User_Job", typeof(String));
            selectTable.Columns.Add("User_Mail", typeof(String));
            selectTable.Columns.Add("User_Cell", typeof(String));

            // 设置主键为User_Id
            selectTable.PrimaryKey = new DataColumn[] { selectTable.Columns["User_Id"] };
            // 返回新的DataTable.
            return selectTable;
        }

        //加载用户列表
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserDataTable = GeteData();
            BindData();
            UserSelectTable = SelectTable();
        }

        //获得数据库数据
        DataTable GeteData()
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "template", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            DBConn.Open();
            //MySQLCommand DBCmd = new MySQLCommand("set names gb2312", DBConn);
            // DBCmd.ExecuteNonQuery();

            string sql = "select * from tb_user";
            MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

            DataTable ds = new DataTable();
            mda.Fill(ds);

            DBConn.Close();

            return ds;
        }

        //绑定数据库数据到listBox
        void BindData()
        {
            //绑定用户栏
            ListBox2.DataContext = UserDataTable;
            ListBox2.SetBinding(ListBox.ItemsSourceProperty, new Binding());

            //绑定信息栏
            Binding binding1 = new Binding("SelectedItem.User_Id") { Source = ListBox2 };
            tb1.SetBinding(TextBlock.TextProperty, binding1);
            Binding binding2 = new Binding("SelectedItem.User_Name") { Source = ListBox2 };
            tb2.SetBinding(TextBlock.TextProperty, binding2);
            Binding binding3 = new Binding("SelectedItem.User_Dept") { Source = ListBox2 };
            tb3.SetBinding(TextBlock.TextProperty, binding3);
            Binding binding4 = new Binding("SelectedItem.User_Job") { Source = ListBox2 };
            tb4.SetBinding(TextBlock.TextProperty, binding4);
            Binding binding5 = new Binding("SelectedItem.User_Mail") { Source = ListBox2 };
            tb5.SetBinding(TextBlock.TextProperty, binding5);
            Binding binding6 = new Binding("SelectedItem.User_Cell") { Source = ListBox2 };
            tb6.SetBinding(TextBlock.TextProperty, binding6);
        }
       
        //确认本次添加操作
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            

            //得到新的添加项
            string id;
            DataRow row;
            row = UserSelectTable.NewRow();
            DataRowView selectrow = ListBox2.SelectedItem as DataRowView;
            if (selectrow == null)
            {
                MessageBox.Show("请选择要添加的用户！");
                return;
            }
            id = selectrow["User_Id"].ToString();
            row["User_Id"] = id;
            row["User_Name"] = selectrow["User_Name"].ToString();
            foreach (DataRow dr in UserDataTable.Rows)
            {
                if (dr["User_Id"].ToString() == id)
                {
                    row["User_Dept"] = dr["User_Dept"].ToString();
                    row["User_Job"] = dr["User_Job"].ToString();
                    row["User_Mail"] = dr["User_Mail"].ToString();
                    row["User_Cell"] = dr["User_Cell"].ToString();
                    break;
                }
            }
            UserSelectTable.Rows.Add(row);
            UserSelectTable.AcceptChanges();
            AddFlag = true;

            //将添加的用户信息填入数据库的tb_selectlist表中
            /* MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString("192.168.61.100", "workflow", "root", "123456").AsString);
            DBConn.Open();
            AddFlag = false;
            for (int i = 0; i < UserSelectTable.Rows.Count; i++)
            {
                string sqlInsert ="insert into tb_selectlist(User_Id , User_Name,User_Dept,User_Job,User_Mail,User_Cell) values ('" + UserSelectTable.Rows[i]["User_Id"] + "' , '" + UserSelectTable.Rows[i]["User_Name"] + "' , '" + UserSelectTable.Rows[i]["User_Dept"] + "' , '" + UserSelectTable.Rows[i]["User_Job"] +"' , '" + UserSelectTable.Rows[i]["User_Mail"] +"' , '"+UserSelectTable.Rows[i]["User_Cell"]+  "')";
                MySQLCommand mySqlCommand = new MySQLCommand(sqlInsert, DBConn);
                try
                {
                    mySqlCommand.ExecuteNonQuery();
                    AddFlag = true;
                }
                catch (Exception ex)
                {
                    String message = ex.Message;
                    MessageBox.Show("添加用户数据失败！该用户数据已存在于表中。" + message);
                }
            }
            DBConn.Close();*/
            this.Close();
        }

        //取消本次操作
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
