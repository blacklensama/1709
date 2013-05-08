using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DemoApp.eq_controls.controls
{
    public class submit:controlBase
    {
        public DemoApp.frmHTMLeditor pform = null; 
        public submit(controlTypes t)
            : base(t)
        {
        }
        public static submit getCtrl(DemoApp.frmHTMLeditor f )
        {
            controlTypes t = controlTypes.CUS_CTRL_SUBMIT;
           
            submit s = new submit(t  )  ;

            s.pform = f; 

            return s ; 
        }
        public override string getMyHtmlContent()
        {
            if (pform == null)
            {
                MessageBox.Show("不能初始化doc对象");
                return "" ;
            }
            submitForm sform = new submitForm(pform);
            if (sform.ShowDialog() != DialogResult.OK)
                return "";

            string listObj = "";
            for (int i = 0; i < sform.listView2.Items.Count; i++)
            {
                listObj += sform.listView2.Items[i].Text + ";";
            }
            string html = "" ;
            html += "<input type='submit' id='sub_" + new Random().Next().ToString() + "' action='" + sform.textBox1.Text + "' listObj='" + listObj + "'></input>";
            return html;

        }
        override public   IfacesEnumsStructsClasses.IHTMLElement loadInfoFromElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null ;

            submitForm sform = new submitForm(this.pform);
            sform.loadElement(e);
            if (sform.ShowDialog() != DialogResult.OK)
                return  null ;
            string listObj = "";
            for (int i = 0; i < sform.listView2.Items.Count; i++)
            {
                listObj += sform.listView2.Items[i].Text + ";";
            }

            IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("input")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return null ;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }

            e.setAttribute("listObj", listObj, 1);
            e.setAttribute("action", sform.textBox1.Text, 1);

            return e;
        }
    }

}
