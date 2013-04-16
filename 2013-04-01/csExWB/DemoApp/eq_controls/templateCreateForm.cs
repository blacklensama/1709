using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.eq_controls
{
    public partial class templateCreateForm : Form
    {
        public templateCreateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("请正确填写名称");
                return;
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
