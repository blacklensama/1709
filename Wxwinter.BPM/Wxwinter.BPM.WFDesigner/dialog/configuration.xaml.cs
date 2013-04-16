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
    /// Interaction logic for configuration.xaml
    /// </summary>
    public partial class configuration : Window
    {
        String dbIp = null;
        String dbUsername = null;
        String dbPassword = null;

        
        public configuration()
        {
            InitializeComponent();
            //Configuration.setDBIp("192.168.61.1");
            //Configuration.setDBPassword("123456");
            //Configuration.setDBUsername("root");
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbIp = Configuration.getDBIp();
            dbUsername = Configuration.getDBUsername();
            dbPassword = Configuration.getDBPassword();
            textBox_DBIp.Text = dbIp;
            textBox_DBUsername.Text = dbUsername;
            passwordBox_DBPassword1.Password = dbPassword;
            passwordBox_DBPassword2.Password = dbPassword;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String messageBoxText;
            String caption;
            MessageBoxButton messagebutton = MessageBoxButton.OK;
            MessageBoxImage messageicon = MessageBoxImage.Warning;

            if (button == null)
            {
                return;
            }

            string buttonValue = button.Content.ToString();

            switch (buttonValue)
            {
                case "取消":
                   // this.Visibility = System.Windows.Visibility.Hidden;
                    this.Close();
                    return;
                case "重置":
                    textBox_DBIp.Text = dbIp;
                    textBox_DBUsername.Text = dbUsername;
                    passwordBox_DBPassword1.Password = dbPassword;
                    passwordBox_DBPassword2.Password = dbPassword;
                    return;
                case "应用":
                    if(textBox_DBIp.Text == ""){
                        messageBoxText = "请填写数据库服务器IP";
                        caption = "提示";
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                        
                        return;
                    }
                    if (textBox_DBUsername.Text == "")
                    {
                        messageBoxText = "请填写数据库服务器登录名";
                        caption = "提示";
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                        return;
                    }
                    if (passwordBox_DBPassword1.Password == "")
                    {
                        messageBoxText = "请填写数据库服务器登录密码";
                        caption = "提示";
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                        return;
                    }
                    if (passwordBox_DBPassword2.Password == "")
                    {
                        messageBoxText = "请确认数据库服务器登录密码";
                        caption = "提示";
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                        return;
                    }
                    if (passwordBox_DBPassword1.Password != passwordBox_DBPassword2.Password)
                    {
                        messageBoxText = "两次数据库服务器登录密码不一致";
                        caption = "提示";
                        MessageBoxResult result = MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                        return;
                    }
                    

                    dbIp = textBox_DBIp.Text;
                    dbPassword = passwordBox_DBPassword2.Password;
                    dbUsername = textBox_DBUsername.Text;
                    //IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(passwordBox_DBPassword1.SecurePassword);
                    // 使用.NET内部算法把IntPtr指向处的字符集合转换成字符串   
                    //dbPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
                    Configuration.setDBIp(dbIp);
                    Configuration.setDBUsername(dbUsername);
                    Configuration.setDBPassword(dbPassword);
                    messageBoxText = "修改成功";
                    caption = "提示";
                    MessageBox.Show(messageBoxText, caption, messagebutton, messageicon);
                    return;
            }
        }
    }
}
