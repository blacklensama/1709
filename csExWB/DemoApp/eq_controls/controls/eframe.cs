using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class eframe : controlBase
    {
        public eframe(controlTypes t)
            : base(t)
        {
        }
        public void insertFrame(DemoApp.frmHTMLeditor pform,string html)
        {
           /* if (pform == null)
                return;
            IfacesEnumsStructsClasses.IHTMLDocument2 doc2 = (IfacesEnumsStructsClasses.IHTMLDocument2)(pform.cEXWB1.GetActiveDocument());
            IfacesEnumsStructsClasses.IHTMLElement  frame = (IfacesEnumsStructsClasses.IHTMLElement )(doc2.createElement("iframe"));
            frame.setAttribute("src" ,"http://www.baidu.com" , 1);

            IfacesEnumsStructsClasses.IHTMLDOMNode bn = doc2.body as IfacesEnumsStructsClasses.IHTMLDOMNode;
            bn.appendChild((IfacesEnumsStructsClasses.IHTMLDOMNode)frame);*/
           

          //  pform.addControl(html);

           // System.Windows.Forms.HtmlDocument doc = (System.Windows.Forms.HtmlDocument)(pform.cEXWB1.GetActiveDocument());
              // html = doc.documentElement.outerHTML;
           //    IfacesEnumsStructsClasses.IHTMLDocument2 doc2 = (IfacesEnumsStructsClasses.IHTMLDocument2 )(pform.cEXWB1.GetActiveDocument());
          //     html = "<html>" + doc2.body.outerHTML + "</html>"; 
            //   doc2.body.innerHTML = html;
//
          //   string tempfilepath =System.Windows.Forms. Application.StartupPath;
          //    if (!tempfilepath.EndsWith("\\"))
           //      tempfilepath = tempfilepath + "\\";

           //   tempfilepath = tempfilepath + "tempFile.html";

             // System.IO.File.WriteAllText(tempfilepath, html, Encoding.GetEncoding("gb2312"));
            //
           //   pform.cEXWB1.SetDesignMode("off");
            //  pform.cEXWB1.Navigate2(tempfilepath);

         //   IfacesEnumsStructsClasses.IHTMLDocument2 doc2 = (pform.cEXWB1.GetActiveDocument());
         //   html = doc2.body.innerHTML;

         //   doc2.body.innerHTML = html;


          //   pform.cEXWB1.SetDesignMode("off");
        //    System.Windows.Forms.MessageBox.Show("a");

           //    Encoding ec = Encoding.GetEncoding("utf-8");
           //    html = ec.GetString(System.Text.Encoding.GetEncoding("utf-8").GetBytes(html));
           
          //    pform.cEXWB1.LoadHtmlIntoBrowser(html);

              
       //     pform.cEXWB1.SetDesignMode("on");
        //    
        }
        public override string getMyHtmlContent()
        {
            iframeForm form = new iframeForm();
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return "";
            string url = form.textBox2.Text;
            if (!url.StartsWith("http://"))
                url  = "http://" + url;
            string title = form.textBox1.Text;
             if (title.Equals(""))
               title = url;
           // string html = "<label style='font-size:12;font-weight:bold'>引用网页:" + title + "</label><br/><br/><iframe id='eframe_" + new Random().Next().ToString() + "' style='width:90%;height:100%'  src='"+url+"'></iframe>";

             string html = "<label style='font-size:12;font-weight:bold'>引用网页:" + title + "</label><br/><br/><iframe id='eframe_" + new Random().Next().ToString() + "' style='width:90%;height:90%'  src='" + url + "'/> ";
            return html;
        }


        override public IfacesEnumsStructsClasses.IHTMLElement loadInfoFromElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null ;

            
            IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)e;
            IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("label")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return null;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }

            IfacesEnumsStructsClasses.IHTMLElement titlee = (IfacesEnumsStructsClasses.IHTMLElement)e; 
            iframeForm form = new iframeForm();
            form.textBox1.Text = e.innerText;


            c = (e2.getElementsByTagName("iframe")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
            if (c == null)
                return null ;
            foreach (IfacesEnumsStructsClasses.IHTMLElement ce in c)
            {
                e = ce;
                break;
            }
            if (e.getAttribute("src", 1) != null)
            {
                string http = e.getAttribute("src", 1).ToString(); ;
                http = http.Replace("http://", "");
                form.textBox2.Text = http;
            }
            if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return  null  ;
            string url = form.textBox2.Text;
            if (!url.StartsWith("http://"))
                url = "http://" + url;
            string title = form.textBox1.Text;
            if (title.Equals(""))
                title = url;

            e.setAttribute("src", url, 1);
            titlee.innerText = title;
            return e;

            //IfacesEnumsStructsClasses.IHTMLDocument2 doc = (IfacesEnumsStructsClasses.IHTMLDocument2)e.document;
           // doc.foc
        }
    }
}
