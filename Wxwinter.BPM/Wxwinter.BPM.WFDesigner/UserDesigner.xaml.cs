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
using MySQLDriverCS;

using Wxwinter.BPM.WFDesigner.model;

namespace Wxwinter.BPM.WFDesigner
{
    // UserDesigner.xaml 的交互逻辑
    public partial class UserDesigner
    {
        public UserDesigner()
        {
            InitializeComponent();
        }
        private User user = new User();
  
        //添加用户MenuItem-------------------------
        private static RoutedUICommand adduser;
        public static RoutedUICommand AddUser
        {
            get
            {
                if (adduser == null)
                {
                    //为什么不显示快捷键？
                    adduser = new RoutedUICommand("添加用户", "AddUser", typeof(UserDesigner), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.A, ModifierKeys.Control | ModifierKeys.Shift) }));
                }
                return adduser;
            }
        }
        private void CanAddUser(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void AddUserCommandExecuted(object sender, RoutedEventArgs e)
        {
            UserSelection userselection = new UserSelection();
            userselection.ShowDialog();
           /* if (UserSelection.AddFlag)
            {
                tb1.Text = UserSelection.UserSelectTable.Rows[0]["User_Id"].ToString();
                tb2.Text = UserSelection.UserSelectTable.Rows[0]["User_Name"].ToString();
                tb3.Text = UserSelection.UserSelectTable.Rows[0]["User_Dept"].ToString();
                tb4.Text = UserSelection.UserSelectTable.Rows[0]["User_Job"].ToString();
                tb5.Text = UserSelection.UserSelectTable.Rows[0]["User_Mail"].ToString();
                tb6.Text = UserSelection.UserSelectTable.Rows[0]["User_Cell"].ToString();
            }*/
        }
        //------------------------------------------


        //移除用户MenuItem-------------------------现在只能从数据库中移除
        //如何将图片一同移除呢？
        //系统自带删除只能删除图片
        private static RoutedUICommand removeuser;
        public static RoutedUICommand RemoveUser
        {
            get
            {
                if (removeuser == null)
                {
                    //为什么不显示快捷键？
                    removeuser = new RoutedUICommand("移除用户", "RemoveUser", typeof(UserDesigner), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.R, ModifierKeys.Control | ModifierKeys.Shift) }));
                }
                return removeuser;
            }
        }
        private void CanRemoveUser(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dialog.OpenUserDialog openWin = new dialog.OpenUserDialog();
            openWin.ShowDialog();
            UserModel um = openWin.getSelected();
            if (null != um)
            {
                tb_id.Text = um.ID;
                tb_id.Focus();
                tb_name.Text = um.Name;
                tb_name.Focus();
                tb_mail.Text = um.Email;
                tb_mail.Focus();
                tb_dept.Text = um.Department;
                tb_dept.Focus();
                tb_cell.Text = um.Telephone;
                tb_cell.Focus();
                tb_pos.Text = um.PersonPosition;
                tb_pos.Focus();
                t_id.Text = um.ID;
                t_id.Focus();
                t_name.Text = um.Name;
                t_name.Focus();
                t_mail.Text = um.Email;
                t_mail.Focus();

            }
            openWin.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            dialog.OpenUserGroupDialog openWin = new dialog.OpenUserGroupDialog();
            openWin.ShowDialog();
            UserModel um = openWin.getSelected();
            if (null != um)
            {
                t_group.Text = um.Department;
                t_group.Focus();
                tb_group.Text = um.Department;
                tb_group.Focus();
            }
            openWin.Close();
        }

       
    }
}

