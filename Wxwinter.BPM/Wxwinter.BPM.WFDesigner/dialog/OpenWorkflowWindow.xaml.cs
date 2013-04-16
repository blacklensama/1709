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
using System.Data.Odbc;
using MySQLDriverCS;
using Wxwinter.BPM.WFDesigner.model;
using System.IO;
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.WFDesigner.dialog
{
    /// <summary>
    /// OpenWorkflowWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OpenWorkflowWindow : Window
    {
        public OpenWorkflowWindow()
        {
            InitializeComponent();
        }
       
        String selectName;
        String selectContent;
        String selectPath;
        WFModel selectedModel;

        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<WFModel> models = new ObservableCollection<WFModel>();


        public String SelectName()
        {
            return selectName;
        }

        public String SelectPath()
        {
            return selectPath;
        }

        public WFModel SelectModel()
        {
            return selectedModel;
        }

        //加载模板列表
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetTemplateData();
            
        }

        //获得数据库数据
        void GetTemplateData()
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


                string sql = "select model_name,owner,model_content,lastedit_time,model_disc,create_time from wf_model";
                MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);

                DataTable ds = new DataTable();
                mda.Fill(ds);

                DBConn.Close();
                foreach (DataRow dr in ds.Rows)
                {
                    WFModel wfm = new WFModel();
                    wfm.WFModel_CreateTime = dr["create_time"].ToString();
                    wfm.WFModel_LasteditTime = dr["lastedit_time"].ToString();
                    wfm.WFModel_Name = dr["model_name"].ToString();
                    wfm.WFModel_Owner = dr["owner"].ToString();
                    string test = dr["model_content"].ToString();
                    if (dr["model_content"] == null || dr["model_content"].ToString().Length<=0)
                    {
                        wfm.WFModel_Content = "";
                    }else
                        wfm.WFModel_Content = Encoding.Default.GetString((Byte[])dr["model_content"]);

                    models.Add(wfm);
                }
                view.Source = models;
                this.listView1.DataContext = view;
            }
            catch (Exception e)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }

        }

       
        //取消本次操作
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            MySQLConnection DBConn = null;
            DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
             try
            {

                DBConn.Open();
                MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                setformat.ExecuteNonQuery();
                setformat.Dispose();

                string sql = "delete from wf_model where model_name = '";
                sql += ((WFModel)listView1.SelectedItem).WFModel_Name;
                 sql += "'";
                MessageBox.Show(sql);
                MySQLCommand cmd = new MySQLCommand(sql, DBConn);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e1)
            {
                MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                return;
            }
             GetTemplateData();
        }

        //打开操作
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {


            //得到新的添加项
            WFModel selected = listView1.SelectedItem as WFModel;
            selectedModel = selected;

            if (selected == null)
            {
                return;
            }
            selectName = selected.WFModel_Name;
            selectContent = selected.WFModel_Content;

            
            if (selectContent.Length > 0)
            {
               // StreamWriter sw = new StreamWriter("template\\temp.xaml", false, Encoding.UTF8);
               // sw.WriteLine(content);
              //  sw.Close();
                //写文件：
                StreamWriter sw = new StreamWriter("template\\temp.xaml", false, Encoding.UTF8);//

                sw.Write(selectContent);
             //   FileStream fs = new FileStream("template\\temp.xaml", FileMode.Create);
            //    String str = new String(selectContent,Encoding.ASCII);
             //   Byte[] decoded = str.getBytes("UTF-8");
                //开始写入
            //    fs.Write(selectContent, 0, selectContent.Length);
                //清空缓冲区、关闭流
                sw.Flush();
                sw.Close();
                selectPath = "template\\temp.xaml";
            }
            else
            {
                selectPath = null;
            }


            this.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
