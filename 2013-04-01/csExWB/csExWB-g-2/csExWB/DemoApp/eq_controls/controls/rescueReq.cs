using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class rescueReq:controlBase
    {
        public rescueReq(controlTypes t)
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
            html += "救援需求分析</td>";
            html += "</tr>";

            html += "<tr style='background:#555555;color:#ffffff;'>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "物资</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "数量</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "单位</td>";
            html += "</tr>";


            html += "<tr>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "帐篷</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "描述灾区所需的帐篷数量</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "个</td>";
            html += "</tr>";

            html += "<tr>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "s食品</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "描述灾区所需救援粮食数量</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "吨</td>";
            html += "</tr>";

            html += "<tr>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "其他</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "......</td>";
            html += "<td align='left' valign='middle' style='color:#888888;border:1px;border-color:#aaaaaa;border-style:solid'>";
            html += "......</td>";
            html += "</tr>";
           
            html += "</table>";

            return html;
        }
    }
}
