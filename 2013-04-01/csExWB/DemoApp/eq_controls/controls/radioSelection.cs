using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class radioSelection:controlBase
    {
        public radioSelection(controlTypes t)
            : base(t)
        {
        }

        public override string getMyHtmlContent()
        {
            string html = "";
            radioForm form = new radioForm();
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return "";

            html = form.getHTML();

            
            return html; 
        }

        override public IfacesEnumsStructsClasses.IHTMLElement loadInfoFromElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            radioForm form = new radioForm();
            form.loadInfo(e);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string html = form.getContent();

                if (e == null)
                    return null ;
                /*IfacesEnumsStructsClasses.IHTMLElement2 n = (IfacesEnumsStructsClasses.IHTMLElement2)e;
                IfacesEnumsStructsClasses.IHTMLElementCollection c = n.getElementsByTagName("TABLE") as IfacesEnumsStructsClasses.IHTMLElementCollection;
                if (c == null)
                    return  ;

                foreach (object obj in c)
                {
                    IfacesEnumsStructsClasses.IHTMLElement r = (IfacesEnumsStructsClasses.IHTMLElement)obj;
                    try
                    {
                        r.innerHTML = html;
                    }
                    catch (Exception exp)
                    {
                    }
                    return  ;
                }*/
                //IfacesEnumsStructsClasses.IHTMLDOMNode nod = (IfacesEnumsStructsClasses.IHTMLDOMNode)e;
                //nod.removeChild(nod.firstChild);

                //e.innerHTML = html;

                //IfacesEnumsStructsClasses.IHTMLDocument2 doc = e.document as IfacesEnumsStructsClasses.IHTMLDocument2;
                //doc.createElement("Table");
               IfacesEnumsStructsClasses.IHTMLElement newe =  form.updateElement(e);
               return newe; 

               
            }
            return null;
        }

        public override void deletMeRelation(IfacesEnumsStructsClasses.IHTMLElement e, DemoApp.frmHTMLeditor pform)
        {
            IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("input")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return;
            e = null;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }
            if (e == null)
                return;

            if (e.getAttribute("id", 1) == null)
                return;
            string id = e.getAttribute("id", 1).ToString();

            int idx = id.LastIndexOf("_");
            id = id.Substring(0, idx  );

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
