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
    public partial class frmChooseTemplate : Form
    {
        public frmChooseTemplate()
        {
            InitializeComponent();
            this.comboBox.Items.Clear();
            ArrayList mapTypeList = DataBase.getTemplateList();
            foreach (object o in mapTypeList)
            {
                this.comboBox.Items.Add((string)o);
            }
        }

        public new  DialogResult ShowDialog(IWin32Window owner)
        {
            this.comboBox.Items.Clear();
            ArrayList mapTypeList = DataBase.getTemplateList();
            foreach (object o in mapTypeList)
            {
                this.comboBox.Items.Add((string)o);
            }
            return base.ShowDialog(owner);
            
        }   

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
