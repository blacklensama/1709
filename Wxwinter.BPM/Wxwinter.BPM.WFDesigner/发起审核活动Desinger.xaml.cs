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
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.WFDesigner
{
    // Interaction logic for 发起审核活动.xaml
    public partial class 发起审核活动Designer
    {
        public 发起审核活动Designer()
        {
            InitializeComponent();
        }
        
    }

    public class SelectItems : ObservableCollection<String>
    {
        public SelectItems()
        {
            this.Add("是");
            this.Add("否");
        }
    }
}
