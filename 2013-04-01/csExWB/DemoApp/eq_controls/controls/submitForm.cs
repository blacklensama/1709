using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.eq_controls.controls
{
    public partial class submitForm : Form
    {
        public DemoApp.frmHTMLeditor pform = null;
        public submitForm(DemoApp.frmHTMLeditor p)
        {
            InitializeComponent();
            pform = p; 
        }

        private void submitForm_Load(object sender, EventArgs e)
        {
            initListView();
        }
        public void loadElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return;
            IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("input")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }
            if (e.getAttribute("listObj", 1) == null)
            {
                c = null;
                return;
            }
            string s = e.getAttribute("listObj", 1).ToString();
            List<string> ss = utility.parseStrings(s, ";");
            listView2.Items.Clear();
            for (int i = 0; i < ss.Count; i++)
            {
                ListViewItem item2 = new ListViewItem(ss[i]);
                item2.Tag = ss[i];
                listView2.Items.Add(item2);
            }
            if (e.getAttribute("action", 1) == null)
                return;

            this.textBox1.Text = e.getAttribute("action", 1).ToString(); 
        }
        public void initListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("对象", -2, HorizontalAlignment.Left);

            listView2.View = View.Details;
            listView2.Columns.Add("对象", -2, HorizontalAlignment.Left);
            if (pform == null)
                return;
            IfacesEnumsStructsClasses.IHTMLElement2 bd = pform.cEXWB1.GetActiveDocument().body as IfacesEnumsStructsClasses.IHTMLElement2;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (bd.getElementsByTagName("input")) as IfacesEnumsStructsClasses.IHTMLElementCollection;

            foreach (IfacesEnumsStructsClasses.IHTMLElement te in c)
            {
                string type = te.getAttribute("type" , 1).ToString();
                if (type == null || type.Equals(""))
                    continue;
                addObj(te);
            }

            c = (bd.getElementsByTagName("textarea")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            foreach (IfacesEnumsStructsClasses.IHTMLElement te in c)
            {
                
                addObj(te);
            }
            c = null;
        }
        public void addObj(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return;
            if (e.getAttribute("id",1) == null)
                return;
            string objName = e.getAttribute("id", 1).ToString();
            string type = e.getAttribute("type", 1).ToString();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem item = listView1.Items[i];
                if (item.Text.Equals(objName))
                    return ;

                
                if (type.Equals("radio"))
                {
                    string gname =   e.getAttribute("name" , 1).ToString();
                    if (gname == null || gname.Equals(""))
                        continue;
                    if (item.Text.Equals(gname))
                        return ;
                }
                
            }
            if (type.Equals("submit"))
                return;

            if (type.Equals("radio"))
            {
                string gname = e.getAttribute("name", 1).ToString();
                if (gname == null || gname.Equals(""))
                    return ;
                ListViewItem item = new ListViewItem(gname);
                item.Tag = objName  ;
                listView1.Items.Add(item);
            }
            else
            {
                ListViewItem item = new ListViewItem(objName);
                item.Tag = objName;
                listView1.Items.Add(item);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0)
                return;

            ListViewItem item1 = this.listView1.SelectedItems[0];
            if (item1 == null)
                return;

            for (int i = 0; i < listView2.Items.Count; i++)
            {
                if (listView2.Items[i].Text.Equals(item1.Text))
                    return;
            }
            ListViewItem item2 = new ListViewItem(item1.Text);
            item2.Tag = item1.Tag;
            listView2.Items.Add(item2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count <= 0)
                return;

            listView2.Items.Remove(listView2.SelectedItems[0]);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count <= 0)
                return;

            ListViewItem item1 = this.listView1.SelectedItems[0];
            if(item1.Tag == null )
                return ;

            string id = (string)item1.Tag;
            IfacesEnumsStructsClasses.IHTMLElement2 bd = pform.cEXWB1.GetActiveDocument().body as IfacesEnumsStructsClasses.IHTMLElement2;
            IfacesEnumsStructsClasses.IHTMLElement c =   pform.cEXWB1.GetElementByID(true , id) ;
            if (c == null)
                return;

            pform.hightLight(c);
        }
    }
}
