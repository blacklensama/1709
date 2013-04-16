using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class information:controlBase
    {
        public information(controlTypes t)
            : base(t)
        {
        }
        public override string getMyHtmlContent()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();
            string bakimg = rootpath + "\\imageobj-bak-1.PNG";
            imgPath = imgPath.Replace("./", "");
            string logoimg = rootpath + imgPath;
            string html = "";
            html += "<table width='100%' height='auto' cellpadding=0 cellspacing=0 style='border:1px;border-color:#aaaaaa;border-style:solid'>";



            html += "<tr>";
            html += "<td align='left' valign='middle' style='height:200px;font-size:12;border:0px;border-color:#aaaaaa;border-style:solid'>";
            // html += "<img width='96px' height='96px' src='" + logoimg + "'></td> ";
            html += "&nbsp;<font color='#999999' >灾情速报:描述强震发生初期各种研判信息，包括人员死亡、经济损失等；</font><br/><br/>";


            html += "</tr>";



            html += "</table>";

            return html;
        }
    }
}
