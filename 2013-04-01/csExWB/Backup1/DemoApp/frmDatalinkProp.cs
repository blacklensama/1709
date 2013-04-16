using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class frmDatalinkProp : Form
    {
        public frmDatalinkProp()
        {
            InitializeComponent();
        }

        public void setParams(string targetKeyNameTxt, string targetValueTxt, string linkKeyNameTxt)
        {
            this.targetKeyNameTxt.Text = targetKeyNameTxt;
            this.targetValueTxt.Text = targetValueTxt;
            this.linkKeyNameTxt.Text = linkKeyNameTxt;
        }

        private void datalinkDefineDia_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.targetKeyNameTxt.Text.Equals(""))
            {
                MessageBox.Show("请完整填写连接定义内容");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            if (this.targetValueTxt.Text.Equals(""))
            {
                MessageBox.Show("请完整填写连接定义内容");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            } 
            if (this.linkKeyNameTxt.Text.Equals(""))
            {
                MessageBox.Show("请完整填写连接定义内容");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }


    }
}
