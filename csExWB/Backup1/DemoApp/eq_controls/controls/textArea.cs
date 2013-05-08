using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class textArea:controlBase
    {
        public textArea(controlTypes t)
            : base(t)
        {
        }
        public override string getMyHtmlContent()
        {
            radioEdit form = new radioEdit();
            form.ShowDialog();
            string html = "<label style='font-size:12;font-weight:bold'>" + form.title.Text + "</label><br/><br/><textarea id='txt_"+new Random().Next().ToString()+"' style='width:90%;height:300px' ></textarea>";
            return html;
        }
        public override void deletMeRelation(IfacesEnumsStructsClasses.IHTMLElement e, DemoApp.frmHTMLeditor pform)
        {
            IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("textarea")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return;
            e = null ;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }
            if(e == null )
                return ;

            if (e.getAttribute("id", 1) == null)
                return;
            string id = e.getAttribute("id", 1).ToString();

            IfacesEnumsStructsClasses.IHTMLElement2 bd = pform.cEXWB1.GetActiveDocument().body as IfacesEnumsStructsClasses.IHTMLElement2;
             c = (bd.getElementsByTagName("input")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                object o = ce.getAttribute("type", 1);
                if (o == null)
                    continue;
                string type = o.ToString();
                if (!type.Equals("submit"))
                    continue;

                o = ce.getAttribute("listObj", 1);
                if (o == null)
                    return;
                string sp = o.ToString();
                sp = sp.Replace(id + ";", "");
                ce.setAttribute("listObj", sp, 1);

            }
        }
    }
}
