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
    public partial class frmMaplinkProp : Form
    {
        public frmMaplinkProp()
        {

            InitializeComponent();
            comboAlignment.SelectedIndex = 0; //Default
            this.comboType.Items.Clear();
            Hashtable mapTypeList = DataBase.getMapTypeList();
            foreach (DictionaryEntry de in mapTypeList) 
            {
                this.comboType.Items.Add(de.Key);
            }
        }
        public DialogResult m_Result = DialogResult.Cancel;
        public string m_Width = "200px";
        public string m_Height = "200px";
        public string m_Alignment = string.Empty;
        public int m_BorderSize = 1;
        public string m_MapType = string.Empty;
        public string m_MapSourceId = string.Empty;

        public void setParams(string height, string width, int borderSize, string alignment, string Type, string SourceId)
        {
            this.textWidth.Text = width + "";
            this.textHeight.Text = height + "";
            this.UpDownBorderSize.Value = borderSize;
            this.textSourceId.Text = SourceId;
            int indexSelected = this.comboType.FindString(Type, 0);
            this.comboType.SelectedIndex = indexSelected;
        }
        private void FillinGlobals()
        {
            m_Result = DialogResult.OK;
            
            m_Width = textWidth.Text;
            m_Height = textHeight.Text;

            m_MapType = string.Empty;
            if (comboType.SelectedIndex >= 0)
                m_MapType = (string)comboType.SelectedItem;

            m_MapSourceId = textSourceId.Text;
            
            m_BorderSize = (int)UpDownBorderSize.Value;

            m_Alignment = string.Empty;
            if (comboAlignment.SelectedIndex > 0)
                m_Alignment = comboAlignment.SelectedText;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            FillinGlobals();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            m_Result = DialogResult.Cancel;
        }
        private void frmTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
                m_Result = DialogResult.Cancel;
            }
        }

    }
}
