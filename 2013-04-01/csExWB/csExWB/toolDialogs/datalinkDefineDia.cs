using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.toolDialogs
{
    public partial class datalinkDefineDia : Form
    {
        public datalinkDefineDia()
        {
            InitializeComponent();
        }

        private void datalinkDefineDia_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.targetKeyNameTxt.Text.Equals(""))
                MessageBox.Show("请完整填写连接定义内容");
            if (this.targetValueTxt.Text.Equals(""))
                MessageBox.Show("请完整填写连接定义内容");
            if (this.linkKeyNameTxt.Text.Equals(""))
                MessageBox.Show("请完整填写连接定义内容");
        }


    }
}
