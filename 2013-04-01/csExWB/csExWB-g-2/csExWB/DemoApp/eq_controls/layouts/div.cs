using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.layouts
{
    public class div : baseLayout
    {
        public override string createHtmlView()
        {
            string html = "<div   style=' float:left;width:50%;height:200px;border:1px;border-color:#aaaaaa;border-style:solid' cellpadding=0 cellspacing=0></div>";
            return html;
        }
    }
}
