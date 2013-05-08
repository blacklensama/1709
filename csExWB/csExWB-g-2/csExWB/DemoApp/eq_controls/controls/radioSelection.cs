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

        override public void loadInfoFromElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            radioForm form = new radioForm();
            form.loadInfo(e);
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string html = form.getContent();

                if (e == null)
                    return  ;
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
                form.updateElement(e);

               
            }
        }
    }
}
