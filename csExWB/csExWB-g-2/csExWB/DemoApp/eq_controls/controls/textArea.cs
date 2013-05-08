using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls.controls
{
    public class textArea:controlBase
    {
        public textArea(controlTypes t)
            : base(t)
        {
        }
        public override string getMyHtmlContent()
        {
            radioEdit form = new radioEdit();
            form.ShowDialog();
            string html = "<label style='font-size:12;font-weight:bold'>" + form.title.Text + "</label><br/><br/><textarea style='width:90%;height:300px' ></textarea>";
            return html;
        }
    }
}
