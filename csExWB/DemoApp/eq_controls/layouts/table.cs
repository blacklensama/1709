using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IfacesEnumsStructsClasses;
using System.Runtime.InteropServices;
using System.Collections;


namespace DemoApp.eq_controls.layouts
{
    public class table:baseLayout
    {
        public override string createHtmlView()
        {
            tableLayout layform = new tableLayout();
            if (layform.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return "";

            int colnum = 1;
            int rownum=1 ;
            string w = layform.width.Text ;
            string h = layform.height.Text ;

            int wal = 0;
            int hval = 0; 
           
            try
            {
                colnum = Convert.ToInt32(layform.colnum.Text);
                rownum = Convert.ToInt32(layform.rownum.Text);

                wal = Convert.ToInt32(w);
                hval = Convert.ToInt32(h);

                if(colnum<1 || rownum<1)
                {
                     System.Windows.Forms.MessageBox.Show("请正确填写行列值") ;
                     return "" ;
                }

                
            }
            catch(Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("请正确填写行列值") ;
                return "" ;
            }
         /*  string html = "<table ltype='_table' style='width:800px;height:" + h + ";border:1px;border-color:#aaaaaa;border-style:solid' cellpadding=0 cellspacing=0>";
            for (int i = 0; i < colnum; i++)
            {
                html += "<tr ltype='_tableTR' >";
                //html += "<td style='white-space:nowrap;'>"; 
                 for (int j = 0; j < rownum; j++)
                 {
                    html += "<td ltype='_tableTD' style='height:auto;width:auto;border:1px;border-color:#aaaaaa;border-style:solid' cellpadding=0 cellspacing=0>";

                    
                   // html += "<div   ltype='_tableDIV' style='width;auto;height:100%;border:1px;border-color:#aadddd;border-style:solid'>&nbsp</div>";
                    
                    //    html += "<table style='width:100%;height:100%;border:1px;border-color:#aadddd;border-style:solid'>";
                     //   html += "<tr><td>&nbsp;</td></tr>";
                     //   html += "</table>";
                      html += "</td>";
                      
                 }
               // html += "</td>";
                 html += "</tr>";
            }
            html += "</table>";*/


            string html = "<table style='width:auto;height:auto' cType='eq_lay' dblink='" + baseLayout.layoutTypes.CUS_LAYL_TABLE + "'>";// "<div ltype='_containerDIV'  style=' height:300px;width:" + (204 * colnum + 4).ToString() + "px;border:1px;border-color:#aaaaaa;border-style:solid' cellpadding=0 cellspacing=0>";

            for (int i = 0; i < colnum; i++)
            {
                int v = (int)((float)hval / colnum);
                string hs = v.ToString() + "px";
              //   html += "<div colidx='" + i.ToString() + "' style='width:auto' ltype='_colDIV'>";
                html += "<tr><td  ltype='_colDIV' style='width:auto;height:auto'>";
                for (int j = 0; j < rownum; j++)
                {

                    int val = (int)((float)wal / rownum);

                    string ws =   val.ToString() + "px";
                   // html += "<div  ltype='_tableDIV' style='width;"+ws+";height:" + hs + ";border:1px;border-color:#aadddd;border-style:solid;float:left'><div style='width:200px'/></div>";

                    html += "<div colidx='" + i.ToString() + "' rowidx ='" + j.ToString() + "' ltype='_cellDIV' style='width:" + ws + ";height:" + hs + ";border:1px;border-color:#aadddd;border-style:solid;float:left'> </div>";
 

                }
                //html += "</div>";
                html += "</td></tr>";
                
            }
            html += "</table>";
            //html += "</div>" ; 
          //   html += "<script type=\"text/javascript\">";
           //  html += "function resizeLay(obj){";
          //   html += "alert('hi')";
          //   html += "}</script>";



            return html;
        }

        public void resize(IfacesEnumsStructsClasses.IHTMLElement e)
        {

            if (e == null)
                return;

           
           if (e.getAttribute("ltype", 1) != null && e.getAttribute("ltype", 1).ToString() == "_colDIV")
            {
                resizeCol(e);
                return;
            }

           if (e.getAttribute("ltype", 1) != null && e.getAttribute("ltype", 1).ToString() != "_cellDIV")
           {
               return;
           }


            int w = getMaxWidthInRow(e);
            if (w >= e.parentElement.offsetWidth)
            {
                int val = w + 2;
                e.parentElement.style.width = val.ToString() + "px";

                // if(w>e.parentElement.parentElement.offsetWidth)
                //     e.parentElement.parentElement.style.width = val.ToString() + "px";
            }
            else
            {
                int val = w + 2;
                e.parentElement.style.width = val.ToString() + "px";
            }

            alignWidth(e);

            //int h = getMaxHeightInRow(e);
            alignHeight(e);

        }
        public void alignHeight(IfacesEnumsStructsClasses.IHTMLElement e)
        {
           // int h = getMaxHeightInRow(e);


            IfacesEnumsStructsClasses.IHTMLElement p = e.parentElement;
            if (p.getAttribute("ltype", 1) != null && p.getAttribute("ltype", 1).ToString() != "_colDIV")
            {
                return  ;
            }
            

            IHTMLDOMNode node = (IHTMLDOMNode)(p);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                if (ne.getAttribute("ltype", 1) != null && ne.getAttribute("ltype", 1).ToString() != "_cellDIV")
                    continue;
                ne.style.height =e.offsetHeight.ToString() + "px";
            }



        }

        public void alignWidth(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            // int h = getMaxHeightInRow(e);


            IfacesEnumsStructsClasses.IHTMLElement p = e.parentElement;
          

            if (p.parentElement != null)
                p = p.parentElement; // tr ;
            else return;
            
            if (p.parentElement != null)
                p = p.parentElement; // tbody ;
            else return;

            IfacesEnumsStructsClasses.IHTMLElement table = p.parentElement;


            object obj = e.getAttribute("rowidx", 1);
            if (obj == null)
                return;
            string fgIdx = obj.ToString();
            int maxW = getMaxWidthInRow(e);


         //   table.style.width = (maxW+2).ToString() + "px";
           


            IHTMLDOMNode node = (IHTMLDOMNode)(p);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                //tr
                IHTMLDOMNode tr = (IHTMLDOMNode)(node.childNodes.item(i));
                if (tr == null)
                    continue;
                for (int j = 0; j < tr.childNodes.length; j++)
                {
                    IHTMLDOMNode td = (IHTMLDOMNode)(tr.childNodes.item(j));
                    if (td == null)
                        continue;
                    for (int k = 0; k < td.childNodes.length; k++)
                    {
                        IHTMLDOMNode div = (IHTMLDOMNode)(td.childNodes.item(k));
                        if (div == null)
                            continue;
                        IfacesEnumsStructsClasses.IHTMLElement dive = (IfacesEnumsStructsClasses.IHTMLElement)div;
                        obj = dive.getAttribute("rowidx", 1);
                        if (obj == null)
                            continue;
                        string idx = obj.ToString();
                        if (idx.Equals(fgIdx))
                        {
                            
                            if (dive == e)
                                continue;
                            dive.style.width = e.offsetWidth.ToString() + "px";
                            dive.parentElement.style.width = "auto";
                        }
                    }

                }
            }

            table.style.width = "auto";



        }
        public void resizeCol(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            int w = getMaxWidthChildren(e) ;
            if (e.offsetWidth > w)
                return;
            IHTMLDOMNode node = (IHTMLDOMNode)(e);
            int ccount = 0; 
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                if (ne.getAttribute("ltype", 1) != null && ne.getAttribute("ltype", 1).ToString() != "_cellDIV")
                    continue;
                float neww = (float)(ne.offsetWidth) / w * e.offsetWidth;

                IHTMLDOMNode ne_node = (IHTMLDOMNode)(ne);
                IfacesEnumsStructsClasses.IHTMLElement cc = (IfacesEnumsStructsClasses.IHTMLElement)(ne_node.firstChild);
                if(cc !=null && cc.tagName=="DIV")
                    cc.style.width = ((int)neww).ToString() + "px";

                ne.style.width = ((int)neww).ToString() + "px";
            }

        }
        private int getMaxWidthChildren(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            int w = 0 ;

            IHTMLDOMNode node = (IHTMLDOMNode)(e);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                if (ne.getAttribute("ltype", 1) != null && ne.getAttribute("ltype", 1).ToString() != "_cellDIV")
                    continue; 
                w += ne.offsetWidth;
            }

            return w;
        }
        private int getMaxWidthInRow(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            int w = e.offsetWidth;

            string rowidx = e.getAttribute("rowidx",1).ToString();
            if (rowidx == null || rowidx.Equals(""))
                return w;


            IfacesEnumsStructsClasses.IHTMLElement p = e.parentElement;
            if (p.getAttribute("ltype", 1) != null && p.getAttribute("ltype", 1).ToString() != "_colDIV")
            {
                return w;
            }
            w = 0;

            IHTMLDOMNode node = (IHTMLDOMNode)(p);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                 if (ne.getAttribute("ltype", 1) != null && ne.getAttribute("ltype", 1).ToString() != "_cellDIV")
                    continue;
               // string idxs = ne.getAttribute("rowidx",1).ToString();
               // if (!idxs.Equals(rowidx))
               //     continue;

             //   if (ne.offsetTop != e.offsetTop)
             //       continue;
                w += ne.offsetWidth;
            }

            return w;
        }

        private int getMaxHeightInRow(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            int w = 0;

            string rowidx = e.getAttribute("rowidx", 1).ToString();
            if (rowidx == null || rowidx.Equals(""))
                return w;


            IfacesEnumsStructsClasses.IHTMLElement p = e.parentElement;
            if (p.getAttribute("ltype", 1) != null && p.getAttribute("ltype", 1).ToString() != "_colDIV")
            {
                return w;
            }
            w = 0;

            IHTMLDOMNode node = (IHTMLDOMNode)(p);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                if (ne.getAttribute("ltype", 1) != null && ne.getAttribute("ltype", 1).ToString() != "_cellDIV")
                    continue;

                if (w < ne.offsetHeight)
                    w = ne.offsetHeight;
            }

            return w;
        }

        private bool isMaxWidthInRow(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            int w = e.offsetWidth;

            string rowidx = e.getAttribute("rowidx",1).ToString();
            if (rowidx == null || rowidx.Equals(""))
                return false;


            IfacesEnumsStructsClasses.IHTMLElement p = e.parentElement;
            if (p.getAttribute("ltype", 1) != null && p.getAttribute("ltype", 1).ToString() != "_containerDIV")
            {
                return false;
            }

            IHTMLDOMNode node = (IHTMLDOMNode)(p);
            for (int i = 0; i < node.childNodes.length; i++)
            {
                if (!(node.childNodes.item(i) is IfacesEnumsStructsClasses.IHTMLElement))
                    continue;
                IfacesEnumsStructsClasses.IHTMLElement ne = (IfacesEnumsStructsClasses.IHTMLElement)(node.childNodes.item(i));
                if (ne.getAttribute("ltype",1) != null && ne.getAttribute("ltype",1).ToString() != "_cellDIV")
                    continue;
                string idxs = ne.getAttribute("rowidx",1).ToString();
                if (!idxs.Equals(rowidx))
                    continue;
                if (ne.offsetWidth >= w)
                    return false;
            }

            return true;


        }
    }
}
