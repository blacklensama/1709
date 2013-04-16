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
    /// TemplateSelection.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateSelection : Window
    {
        public TemplateSelection()
        {
            InitializeComponent();
        }
        DataTable TemplateDataTable; //定义用户数据表
        static public DataTable TemplateSelectTable;
        static public bool AddFlag = false;//判断添加用户操作是否成功

        private DataTable SelectTable()
        {
            // 创建一个名为“SelectUser”的DataTable
            DataTable selectTable = new DataTable("SelectTemplate");

            // Add six column objects to the table.
            selectTable.Columns.Add("Template_Id", typeof(Int32));
            selectTable.Columns.Add("Template_Name", typeof(String));
            selectTable.Columns.Add("Template_HtmlSource", typeof(String));
            selectTable.Columns.Add("Template_Description", typeof(String));

            // 设置主键为Template_Id
            selectTable.PrimaryKey = new DataColumn[] { selectTable.Columns["Template_Id"] };
            // 返回新的DataTable.
            return selectTable;
        }

        //加载用户列表
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TemplateDataTable = GeteData();
            BindData();
            TemplateSelectTable = SelectTable();
        }

        //获得数据库数据
        DataTable GeteData()
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "template", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            DBConn.Open();
            //MySQLCommand DBCmd = new MySQLCommand("set names gb2312", DBConn);
            // DBCmd.ExecuteNonQuery();

            string sql = "select * from template";
            MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

            DataTable ds = new DataTable();
            mda.Fill(ds);

            DBConn.Close();

            return ds;
        }

        //绑定数据库数据到listBox
        void BindData()
        {
            //绑定表单选择栏
            ListBox2.DataContext = TemplateDataTable;
            ListBox2.SetBinding(ListBox.ItemsSourceProperty, new Binding());

            //绑定信息栏
            Binding binding1 = new Binding("SelectedItem.ID") { Source = ListBox2 };
            tb1.SetBinding(TextBlock.TextProperty, binding1);
            Binding binding2 = new Binding("SelectedItem.NAME") { Source = ListBox2 };
            tb2.SetBinding(TextBlock.TextProperty, binding2);
            Binding binding3 = new Binding("SelectedItem.HTMLSOURCE") { Source = ListBox2 };
            tb3.SetBinding(TextBlock.TextProperty, binding3);
            Binding binding4 = new Binding("SelectedItem.DESCRIPTION") { Source = ListBox2 };
            tb4.SetBinding(TextBlock.TextProperty, binding4);
        }

        //确认本次添加操作
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //得到新的添加项
            int id;
            DataRow row;
            row = TemplateSelectTable.NewRow();
            DataRowView selectrow = ListBox2.SelectedItem as DataRowView;
            if (selectrow == null)
            {
                MessageBox.Show("请选择要添加的表单模板！");
                return;
            }
            id = Convert.ToInt32(selectrow["ID"].ToString());
            row["Template_Id"] = id;
            row["Template_Name"] = selectrow["NAME"].ToString();
            foreach (DataRow dr in TemplateDataTable.Rows)
            {
                if ( Convert.ToInt32(dr["ID"].ToString()) == id)
                {
                    row["Template_HtmlSource"] = dr["HTMLSOURCE"].ToString();
                    row["Template_Description"] = dr["DESCRIPTION"].ToString();
                    break;
                }
            }
            TemplateSelectTable.Rows.Add(row);
            TemplateSelectTable.AcceptChanges();


            //将添加的表单模板信息填入数据库的tb_templateselectlist表中
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "template", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            DBConn.Open();
            AddFlag = false;
            for (int i = 0; i < TemplateSelectTable.Rows.Count; i++)
            {
                string sqlInsert = "insert into tb_templateselectlist(Template_Id , Template_Name,Template_HtmlSource,Template_Description) values ('" + TemplateSelectTable.Rows[i]["Template_Id"] + "' , '" + TemplateSelectTable.Rows[i]["Template_Name"] + "' , '" + TemplateSelectTable.Rows[i]["Template_HtmlSource"] + "' , '" + TemplateSelectTable.Rows[i]["Template_Description"] + "')";
                MySQLCommand mySqlCommand = new MySQLCommand(sqlInsert, DBConn);
                try
                {
                    mySqlCommand.ExecuteNonQuery();
                    AddFlag = true;
                }
                catch (Exception ex)
                {
                    String message = ex.Message;
                    MessageBox.Show("添加表单模板数据失败！该表单模板数据已存在于表中。" + message);
                }
            }
            DBConn.Close();
            this.Close();
        }

        //取消本次操作
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
