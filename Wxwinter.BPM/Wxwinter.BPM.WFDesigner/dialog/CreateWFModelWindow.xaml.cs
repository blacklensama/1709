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
using MySQLDriverCS;
using Wxwinter.BPM.WFDesigner.dialog;
using Wxwinter.BPM.WFDesigner.model;

namespace Wxwinter.BPM.WFDesigner.dialog
{
    /// <summary>
    /// Interaction logic for CreateWFModelWindow.xaml
    /// </summary>
    public partial class CreateWFModelWindow : Window
    {
        public model.WFModel WFModelInst;// = new model.WFModel();
        public CreateWFModelWindow()
        {
            InitializeComponent();
            var WFTypeList = new List<string> { "_IMMEDIATE", "_30MIN", "_1HR", "_6HR", "_10HR", "_WARN" };
            comboBox_WFModelType.DataContext = WFTypeList;
            var WFNameList = new List<string> { };
            textBox_WFModelName.DataContext = WFNameList;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
            {
                return;
            }

            string buttonValue = button.Content.ToString();

            switch (buttonValue)
            {
                case "取消":
                    this.Visibility = System.Windows.Visibility.Hidden;
                    WFModelInst = null;
                    break;


                case "新建":
                    WFModelInst = new model.WFModel();
                    if (textBox_WFModelName.Text != "")
                    {
                        WFModelInst.WFModel_Name = textBox_WFModelName.Text;
                    }
                    else
                    {
                        MessageBox.Show("请填写流转任务模型名称");
                        WFModelInst = null;
                        //this.Visibility = System.Windows.Visibility.Hidden;
                        break;
                    }
                    if (comboBox_WFModelType.Text != "")
                    {
                        WFModelInst.WFModel_Type = comboBox_WFModelType.Text;
                    }
                    else
                    {
                        MessageBox.Show("请选择流转任务模型类型");
                        WFModelInst = null;
                        //this.Visibility = System.Windows.Visibility.Hidden;
                        break;
                    }
                    if (Check_Profe.IsChecked == true)
                    {
                        WFModelInst.WFModel_Owner += "Professional,";
                    }
                    else
                    {

                        if (Check_Admin.IsChecked == true)
                        {
                            WFModelInst.WFModel_Owner += "Administrator,";
                        }
                        else
                        {
                            if (Check_Busin.IsChecked == true)
                            {
                                WFModelInst.WFModel_Owner += "Business";
                            }
                            else
                            {
                                MessageBox.Show("请选择用户类型");
                                WFModelInst = null;
                                //this.Visibility = System.Windows.Visibility.Hidden;
                                break;
                            }
                        }
                    }

                    
                    int saveReport = saveModel();
                    if (saveReport == 0)
                    {
                        MessageBox.Show("新建流转任务模型成功");
                        this.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (saveReport == -1)
                    {
                        MessageBox.Show("新建流转任务模型失败,流转任务模型名称已存在");
                        WFModelInst = null;
                        this.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (saveReport == -2)
                    {
                        MessageBox.Show("新建流转任务模型失败,网络暂时无法连接，请稍后重试");
                        WFModelInst = null;
                        this.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        MessageBox.Show("新建流转任务模型失败,请重试");
                        WFModelInst = null;
                        this.Visibility = System.Windows.Visibility.Visible;
                    }
                    break;

            }
        }

        private int saveModel()
        {
            if (WFModelInst == null)
                return -3;
            else
            {
                try
                {
                    MySQLConnection DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
                    string checkSql = "select model_name from wf_model where model_name = '" + WFModelInst.WFModel_Name + "';";
                    DBConn.Open();
                    MySQLCommand mcd = new MySQLCommand(checkSql, DBConn);
                    MySQLDataReader DBReader = mcd.ExecuteReaderEx(); //DBComm.ExecuteReaderEx();
                    if (DBReader.Read())
                    {
                        return -1;
                    }
                    else
                    {
                        string insertSql = "insert into wf_model (model_name,model_type,owner) values ('" + WFModelInst.WFModel_Name + "','" + WFModelInst.WFModel_Type + "','" + WFModelInst.WFModel_Owner + "');";
                        try
                        {
                            MySQLCommand mcd2 = new MySQLCommand(insertSql, DBConn);
                            mcd2.ExecuteNonQuery();
                            return 0;
                        }
                        catch (Exception Ex)
                        {
                            return -2;

                        }
                    }
                }
                catch (MySQLException e) {
                    //MessageBox.Show("数据库连接失败，请检查网络连接或者数据库配置");
                    return -2;
                }
            }

        }
    }
}
