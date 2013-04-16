using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp.eq_controls.controls
{
    public partial class radioForm : Form
    {
        public radioForm()
        {
            InitializeComponent();
        }

        private void radioForm_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Add("对象", -2, HorizontalAlignment.Left);
        }
        public string getHTML()
        {
            string html = "<table style='font-size:12px;line-height:24px;'>";
           /* html += "<tr><td>" + this.title.Text + "</td></tr>";
            html += "<tr><td>" + this.description.Text + "</td></tr>";

            html += "<tr><td>";
            string groupname = new Random().Next().ToString() ;
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                html += "<input type='radio' id='" + groupname + "_" + (i + 1).ToString() + "' name='" + groupname + "' value='" + listView1.Items[i].Text + "'>" + listView1.Items[i].Text + "</input><br/>";
            }
            html += "</td></tr>";*/
            html += getContent();
            html += "</table>";

            return html;
        }

        public string getContent()
        {
            string html = "";
            html += "<tr><td>" + this.title.Text + "</td></tr>";
            html += "<tr><td>" + this.description.Text + "</td></tr>";

            html += "<tr><td>";
            string groupname = new Random().Next().ToString();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                html += "<input type='radio' id='" + groupname + "_" + (i + 1).ToString() + "' name='" + groupname + "' value='" + listView1.Items[i].Text + "'>" + listView1.Items[i].Text + "</input><br/>";
            }
            html += "</td></tr>";
            

            return html;
        }

        public IfacesEnumsStructsClasses.IHTMLElement updateElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {

            IfacesEnumsStructsClasses.IHTMLElement2 e2 =( IfacesEnumsStructsClasses.IHTMLElement2 )e;
            if (e2 == null)
                return null;

            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("table")) as IfacesEnumsStructsClasses.IHTMLElementCollection;

            IfacesEnumsStructsClasses.IHTMLElement contentTbl = null;// (IfacesEnumsStructsClasses.IHTMLElement)(e2.getElementsByTagName("table"));

            foreach (IfacesEnumsStructsClasses.IHTMLElement te in c)
            {
                contentTbl = te;
                break;
            }
            IfacesEnumsStructsClasses.IHTMLDOMNode nod = (IfacesEnumsStructsClasses.IHTMLDOMNode)e;

            if (contentTbl != null)
                nod = (IfacesEnumsStructsClasses.IHTMLDOMNode)(contentTbl.parentElement);

            nod.removeChild(nod.firstChild);

           
            

            IfacesEnumsStructsClasses.IHTMLDocument2 doc = (IfacesEnumsStructsClasses.IHTMLDocument2)(e.document);
         
           
            IfacesEnumsStructsClasses.IHTMLElement tbl = doc.createElement("table");
            IfacesEnumsStructsClasses.IHTMLElement tbldy = doc.createElement("tbody");

            IfacesEnumsStructsClasses.IHTMLElement tr = doc.createElement("tr");
            IfacesEnumsStructsClasses.IHTMLElement td = doc.createElement("td");
         //   IfacesEnumsStructsClasses.IHTMLDOMTextNode td = doc.createElement("td");


            td.innerHTML = this.title.Text;

            appendNewChild((IfacesEnumsStructsClasses.IHTMLElement)nod, tbl);
            appendNewChild(tbl, tr);
            appendNewChild(tr, td);


            tr = doc.createElement("tr");
            td = doc.createElement("td");
            td.innerHTML = this.description.Text;
            appendNewChild(tr, td);
            appendNewChild(tbl, tr);


            tr = doc.createElement("tr");
            td = doc.createElement("td");


            string groupname = new Random().Next().ToString();
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                IfacesEnumsStructsClasses.IHTMLElement inpute = doc.createElement("input");
                inpute.setAttribute("type", "radio", 1);
                if (this.listView1.Items[i].Tag != null)
                {
                    inpute.setAttribute("name", this.listView1.Items[i].Tag.ToString(), 1);
                    groupname = this.listView1.Items[i].Tag.ToString(); 
                }

                 else inpute.setAttribute("name", groupname, 1);
                inpute.setAttribute("value", this.listView1.Items[i].Text, 1);
                IfacesEnumsStructsClasses.IHTMLElement br = doc.createElement("br");
                
            //    inpute.innerText =  this.listView1.Items[i].Text;
                appendNewChild(  td, inpute);
                //td.insertAdjacentText("", this.listView1.Items[i].Text);
                td.innerHTML += this.listView1.Items[i].Text;
                appendNewChild(td, br);

                
            }
            appendNewChild(tr, td);
            appendNewChild(tbl, tr);
          
             
            return tbl;

        }
        private void appendNewChild(IfacesEnumsStructsClasses.IHTMLElement e1, IfacesEnumsStructsClasses.IHTMLElement e2)
        {
              IfacesEnumsStructsClasses.IHTMLDOMNode nod1 = (IfacesEnumsStructsClasses.IHTMLDOMNode)e1;
            IfacesEnumsStructsClasses.IHTMLDOMNode nod2 = (IfacesEnumsStructsClasses.IHTMLDOMNode)e2;
            nod1.appendChild(nod2);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            radioEdit form = new radioEdit();
            
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.title.Text.Equals(""))
                {
                    MessageBox.Show("请正确填写选项名称");
                    return;
                }
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Text.Equals(form.title.Text))
                    {
                        MessageBox.Show("对象已经存在请正确填写");
                        return;
                    }
                }
                ListViewItem item = new ListViewItem(form.title.Text);
                listView1.Items.Add(item);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx <= 0)
                return;

            radioEdit form = new radioEdit();
            form.title.Text = listView1.SelectedItems[0].Text;

            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.title.Text.Equals(""))
                {
                    MessageBox.Show("请正确填写选项名称");
                    return;
                }
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].Text.Equals(form.title.Text))
                    {
                        MessageBox.Show("对象已经存在请正确填写");
                        return;
                    }
                }
                listView1.SelectedItems[0].Text = form.title.Text;
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int idx = listView1.SelectedItems.Count;
            if (idx <= 0)
                return;
            listView1.Items.RemoveAt(idx);
        }
        public IfacesEnumsStructsClasses.IHTMLElement getContent(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null;
            IfacesEnumsStructsClasses.IHTMLElement2 n = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = n.getElementsByTagName("TABLE") as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return null;

            foreach(object obj in c )
            {
                  IfacesEnumsStructsClasses.IHTMLElement r = (  IfacesEnumsStructsClasses.IHTMLElement)obj ;
                return r ;
            }

            return null;
        }
        public void loadInfo(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return;


            e= getContent(  e) ;
            if (e == null)
                return;

            IfacesEnumsStructsClasses.IHTMLElement2 n = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = n.getElementsByTagName("TD") as IfacesEnumsStructsClasses.IHTMLElementCollection;
          if (c != null)
          {
              if (c.length >= 2)
              {

                  foreach (object obj in c)
                  {
                      IfacesEnumsStructsClasses.IHTMLElement r = (IfacesEnumsStructsClasses.IHTMLElement)obj;
                      title.Text = r.innerText;
                      break;
                  }

                  foreach (object obj in c)
                  {
                      IfacesEnumsStructsClasses.IHTMLElement r = (IfacesEnumsStructsClasses.IHTMLElement)obj;
                      description.Text = r.innerText;
                      break;
                  }

                 // title.Text = ((c.)) as IfacesEnumsStructsClasses.IHTMLElement).innerText;
                 // description.Text = ((c.item(1)) as IfacesEnumsStructsClasses.IHTMLElement).innerText;
              }
          }
          c = n.getElementsByTagName("INPUT") as IfacesEnumsStructsClasses.IHTMLElementCollection;
          if (c != null)
          {
              foreach(object obj in c)  
              {
                  IfacesEnumsStructsClasses.IHTMLElement input = (IfacesEnumsStructsClasses.IHTMLElement)obj;
                  if (input != null)
                  {
                      if (input.getAttribute("TYPE", 1).ToString().Equals("RADIO") || input.getAttribute("type", 1).ToString().Equals("radio"))
                      {
                          ListViewItem item = new ListViewItem(input.getAttribute("value",1).ToString());
                          item.Tag =  input.getAttribute("name", 1).ToString() ;
                          this.listView1.Items.Add(item);
                      }
                  }
              }
          }

        }
    }
}
