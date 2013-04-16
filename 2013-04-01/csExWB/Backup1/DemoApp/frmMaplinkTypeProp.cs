using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class frmMaplinkTypeProp : Form
    {
        public frmMaplinkTypeProp()
        {
            InitializeComponent();
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
            this.textSourceId.Text = SourceId;
        }
        private void FillinGlobals()
        {
            m_Result = DialogResult.OK;
            
            m_Width = textWidth.Text;
            m_Height = textHeight.Text;

            m_MapType = string.Empty;

            m_MapSourceId = textSourceId.Text;
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
