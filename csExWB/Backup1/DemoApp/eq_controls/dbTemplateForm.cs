using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.eq_controls
{
    public partial class dbTemplateForm : Form
    {
        public string [] sel_template = null;
        public dbTemplateForm()
        {
            InitializeComponent();
        }

        private void dbTemplateForm_Load(object sender, EventArgs e)
        {

            loadFilesFromDB();
            sel_template = null;
        }
        public void loadFilesFromDB()
        {
           // filesListView.View = View.Details;
           // filesListView.GridLines = true;
            filesListView.Columns.Add("模版文件", -2, HorizontalAlignment.Left);

            List<string> templates = dbTools.dbTool.getTemplateNameList();
            if (templates == null)
                return;
            for (int i = 0; i < templates.Count; i++)
            {
                string name = templates[i];
                if (name.Equals(""))
                    name = "未命名模版文件";
                ListViewItem item = new ListViewItem("模版：" + name);
                item.Tag = templates[i];
                item.ToolTipText = templates[i];
                item.ImageIndex = 0;
                this.filesListView.Items.Add(item);
            }
            templates.Clear();
            templates = null;
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            sel_template = null;
            if (filesListView.SelectedItems.Count <= 0)
                return;
            ListViewItem item = filesListView.SelectedItems[0];
            if (item.Tag == null)
                return;
            string tg = (string )(item.Tag);

            sel_template = dbTools.dbTool.getTemplate(tg);
            if (sel_template == null)
            {
                MessageBox.Show("加载模版文件错误");
                return;
            }
            this.Close();
        }
    }
}
