using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class alert:controlBase
    {
        public alert(controlTypes t)
            : base(t)
        {
        }

        public override string getMyHtmlContent()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();
           // string bakimg = rootpath + "\\imageobj-bak-1.PNG";
           // imgPath = imgPath.Replace("./", "");
           // string logoimg = rootpath + imgPath;
            string logoimg = imgPath;

            string html = "";
            html += "<table width='100%' height='auto' cellpadding=0 cellspacing=0 style='border:1px;border-color:#aaaaaa;border-style:solid'>";
             


            html += "<tr>";
            html += "<td align='left' valign='middle' style='height:200px;font-size:16;border:0px;border-color:#aaaaaa;border-style:solid'>";
           // html += "<img width='96px' height='96px' src='" + logoimg + "'></td> ";
            html += "&nbsp;<font size='12'>强震名称</font><br/><br/>";
            html += "&nbsp;&nbsp;&nbsp;&nbsp;时间：<br/><br/>";
            html += "&nbsp;&nbsp;&nbsp;&nbsp;地点（经纬度）<br/><br/>";
            html += "&nbsp;&nbsp;&nbsp;&nbsp;震级：<br/><br/>";

            html += "</tr>";


           
            html += "</table>";

            return html;
        }
    }
}
