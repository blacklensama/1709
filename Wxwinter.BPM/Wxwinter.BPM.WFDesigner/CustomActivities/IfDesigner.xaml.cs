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

namespace Wxwinter.BPM.WFDesigner.CustomActivities
{
    // IfDesigner.xaml 的交互逻辑
    public partial class IfDesigner
    {
        public IfDesigner()
        {
            InitializeComponent();
        }
    }
    public class Operators
    {
        public string Operator { set; get; }
    }
    public class OperatorArr : ObservableCollection<String>
    {
        public OperatorArr()
        {
            this.Add("<" );
            this.Add("<=");
            this.Add("=" );
            this.Add(">=" );
            this.Add(">" );
        }
    }
    public class InputItems
    {
        public string InputItem { set; get; }
    }
    public class InputItemArr : ObservableCollection<String>
    {
        public InputItemArr()
        {
            this.Add("震级" );
            this.Add("地区");
            this.Add("响应等级" );
        }
    }



    public class Days : ObservableCollection<String>
    {
        public Days()
        {
            for (int i = 0; i < 8; i++)
            {
                this.Add(i.ToString());

            }
        }
    }

    public class Hours : ObservableCollection<String>
    {
        public Hours()
        {
            for (int i = 0; i < 24; i++)
            {
                this.Add(i.ToString());

            }
        }
    }
    public class Minutes : ObservableCollection<String>
    {
        public Minutes()
        {
            for (int i = 0; i < 60; i++)
            {
                this.Add(i.ToString());
            }
        }
    }
}
