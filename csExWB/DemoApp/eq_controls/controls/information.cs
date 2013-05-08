using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
            StreamReader sw = new StreamReader("templateStatic\\information");
            html = sw.ReadToEnd();
            return html;
        }
    }
}
