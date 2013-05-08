using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class pDiv:controlBase
    {
        public pDiv(controlTypes t)
            : base(t)
        {

        }

        public override string getMyHtmlContent()
        {
            string html = "<time> template time </time>";
            return html;
        }
    }
}
