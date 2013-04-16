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
    // WhileDesigner.xaml 的交互逻辑
    public partial class WhileDesigner
    {
        public WhileDesigner()
        {
            InitializeComponent();
        }
        public class Operators
        {
            public string Operator { set; get; }
        }
        public class OperatorArr : ObservableCollection<Operators>
        {
            public OperatorArr()
            {
                this.Add(new Operators { Operator = "<" });
                this.Add(new Operators { Operator = "<=" });
                this.Add(new Operators { Operator = "=" });
                this.Add(new Operators { Operator = "=>" });
                this.Add(new Operators { Operator = ">" });
            }
        }
        public class InputItems
        {
            public string InputItem { set; get; }
        }
        public class InputItemArr : ObservableCollection<InputItems>
        {
            public InputItemArr()
            {
                this.Add(new InputItems { InputItem = "震级" });
                this.Add(new InputItems { InputItem = "地区" });
                this.Add(new InputItems { InputItem = "响应等级" });
            }
        }
    }
}
