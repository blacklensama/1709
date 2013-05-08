using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class rescuePlan:controlBase
    {
         public rescuePlan(controlTypes t)
            : base(t)
        {
        }

        public override string getMyHtmlContent()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();

            string logoimg = imgPath;

            string html = "";
            html += "<table width='100%' height='auto' cellpadding=0 cellspacing=0 style='font-size:12px;line-height:32px;border:1px;border-color:#aaaaaa;border-style:solid'>";

           


            html += "<tr cellpadding=0 cellspacing=0>";
            html += "<td align='left' nowrap valign='middle' style='font-size:16;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "救援行动方案</td>";
            html += "</tr>";

            html += "<tr cellpadding=0 cellspacing=0>";
            html += "<td align='center' valign='middle' style='border-bottom:#aaaaaa 1px solid;height:128px;background-image:url(\"" + logoimg + "\");background-repeat:no-repeat;background-position: center;;'>";
            //html += "<td align='center' nowrap valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            //html += "<img src='"+logoimg+"'/> <br/><br/> 行动地图</td>";
            html += "</td></tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "队伍编制：......</td>";
            html += "</tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "领队：......</td>";
            html += "</tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "联系人：......</td>";
            html += "</tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "预期行程路线：........</td>";
            html += "</tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "保障部门：......</td>";
            html += "</tr>";

            html += "<tr style=''>";
            html += "<td align='left' valign='middle' style='color:#888888;border:0px;border-color:#aaaaaa;border-style:solid'>";
            html += "其他：......</td>";
            html += "</tr>";

            html += "</table>";

            return html;
        }
    }
}
