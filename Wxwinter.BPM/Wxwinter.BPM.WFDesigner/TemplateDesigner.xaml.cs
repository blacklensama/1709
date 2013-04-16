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
    // TemplateDesigner.xaml 的交互逻辑
    public partial class TemplateDesigner
    {
        private Template template = new Template();
        public TemplateDesigner()
        {
            InitializeComponent();
            template.Name = "test";

        }
       
        
        //添加用户MenuItem-------------------------
        private static RoutedUICommand addtemplate;
        public static RoutedUICommand AddTemplate
        {
            get
            {
                if (addtemplate == null)
                {
                    //为什么不显示快捷键？
                    addtemplate = new RoutedUICommand("添加表单模板", "AddTemplate", typeof(TemplateDesigner), new InputGestureCollection(new InputGesture[] { new KeyGesture(Key.A, ModifierKeys.Control | ModifierKeys.Shift) }));
                }
                return addtemplate;
            }
        }
        private void CanAddTemplate(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            dialog.OpenTemplateDialog openWin = new dialog.OpenTemplateDialog();
            openWin.ShowDialog();
            TemplateModel um = openWin.getSelected();
            if (null != um)
            {
                name.Text = um.name;
                name.Focus();
                tb_name.Text = um.name;
                tb_name.Focus();
            }
            openWin.Close();
        }
        
    }
}
