using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DemoApp
{
    public partial class frmRadioProp : Form
    {
        public frmRadioProp()
        {
            InitializeComponent();
            this.comboBox1.Items.Clear();
            ArrayList radioNames = frmHTMLeditor.radioNames;
            foreach (object o in radioNames)
            {
                this.comboBox1.Items.Add((string)o);
            }
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            this.comboBox1.Items.Clear();
            ArrayList radioNames = frmHTMLeditor.radioNames;
            foreach (object o in radioNames)
            {
                this.comboBox1.Items.Add((string)o);
            }
            return base.ShowDialog(owner);

        } 

        private void frmRadioProp_Load(object sender, EventArgs e)
        {

        }
    }
}
