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
    public partial class frmDelMapType : Form
    {
        public frmDelMapType()
        {
            InitializeComponent();
            this.comboBox.Items.Clear();
            Hashtable mapTypeList = DataBase.getMapTypeList();
            foreach (DictionaryEntry de in mapTypeList)
            {
                this.comboBox.Items.Add(de.Key);
            }
        }
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            this.comboBox.Items.Clear();
            Hashtable mapTypeList = DataBase.getMapTypeList();
            foreach (DictionaryEntry de in mapTypeList)
            {
                this.comboBox.Items.Add(de.Key);
            }
            return base.ShowDialog(owner);

        } 

        private void frmDelMapType_Load(object sender, EventArgs e)
        {

        }
    }
}
