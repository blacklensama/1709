using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.eq_controls
{
    public partial class templateForm : Form
    {
        public string connectString = "";
        public int type = 0;
        public string tname = "";
        public string tdescription = "";

        public templateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            connectString = "";
            type = 0;
            templateCreateForm cf = new templateCreateForm();
            if (cf.ShowDialog() == DialogResult.OK)
            {
                tname = cf.textBox1.Text;
                tdescription = cf.textBox2.Text;
                this.Close();
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                connectString = openFileDialog1.FileName;
                type = 1;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             
                type = 2;
                this.Close();
             
        }
    }
}
