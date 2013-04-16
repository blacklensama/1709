using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using IfacesEnumsStructsClasses;

namespace DemoApp
{
    public partial class frmAddControl : Form
    {
        public frmAddControl()
        {
            InitializeComponent();
        }
        public string html;
        IfacesEnumsStructsClasses.IHTMLElement hitElement = null;
        private void frmAddControl_Load(object sender, EventArgs e)
        {
            loadName();
        }

        private void loadName()
        {
            comboBox1.Items.Clear();
            DirectoryInfo TheFolder = new DirectoryInfo("template");
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                comboBox1.Items.Add(NextFile.Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.ToString() == "")
            {
                return;
            }
            
            StreamReader sw = new StreamReader("template\\" + comboBox1.Text.ToString());
            html = sw.ReadToEnd();
            cEXWB1.Clear();
            addControl(html);
            sw.Close();
        }

        public void addControl(string html)
        {

            cEXWB1.Focus();

            IHTMLDocument2 doc2 = null;
            IHTMLSelectionObject selobj = null;
            IHTMLTxtRange range = null;


            doc2 = cEXWB1.GetActiveDocument();
            if ((doc2 == null) || (doc2.selection == null))
                return;

            selobj = doc2.selection as IHTMLSelectionObject;
            if (selobj == null)
                return;

            if ((selobj.EventType == "none") || (selobj.EventType == "control"))
                return;

            try
            {
                range = selobj.createRange() as IHTMLTxtRange;

                if (range == null)
                    return;


                range.pasteHTML(html);

            }
            catch (Exception exp)
            {

            }

            return;
        }

        public bool isControl(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return false;
            if (e.getAttribute("cType", 0) != null && e.getAttribute("cType", 0).Equals("eq_ctrl"))
                return true;

            if (e.parentElement != null)
                return isControl(e.parentElement);

            return false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.ToString() == "")
            {
                return;
            }
            File.Delete("template\\" + comboBox1.Text.ToString());
            loadName();
        }
    }
}
