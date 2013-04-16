using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.layouts
{
    public class div : baseLayout
    {
        public override string createHtmlView()
        {
            string html = "<div id=\"map\"  style=\" float:left;width:50%;height:200px;border:1px;color:#00FF00;border-style:solid\" cellpadding=0 cellspacing=0><p>This is a map</p></div>";
            return html;
        }
    }
}
