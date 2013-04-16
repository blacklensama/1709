using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;


namespace DemoApp.eq_controls.controls
{
    public class test:controlBase
    {
        public test(controlTypes t, string str)
            : base(t)
        {
            json = new jsonBase(str);
        }

        public override string getMyHtmlContent()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();
            string bakimg = rootpath + "\\imageobj-bak-1.PNG";
            imgPath = imgPath.Replace("./", "");
            string logoimg = rootpath + imgPath;
            string html = json.getHtmlTable();
            return html;
        }

        jsonBase json;
    }
}
