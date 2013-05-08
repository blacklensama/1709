using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls
{
    public class imageCtrl : controlBase
    {
        public imageCtrl(controlTypes t)
            : base(t)
        {
        }
        public override string getMyHtmlContent()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();
            string logoimg = imgPath;

     //       string bakimg = rootpath + "/imageobj-bak-1.PNG";
     //       imgPath = imgPath.Replace("./", "");
    //        string logoimg = rootpath + imgPath;
    //        logoimg.Replace("/", "\\");
          //  logoimg = "E:\\devwork\\csExWB\\csExWB\\bin\\Debug\\icon\\1_211319_2.jpg";// 
            rootpath += "/icon";
            //string html = "<img src = \"" + logoimg + "\"/>";
            //html += "<table cellpadding=0 cellspacing=0 style='border:1px;border-color:#aaaaaa;border-style:solid;'>";
           // html += "<tr><td style='height:auto'><img width='100%' height='auto' src=" + rootpath + "\\bak-h-top-bar.PNG" + "></td></tr>";


          //  html += "<tr><td ><img height='100%' width='auto' src=" + rootpath + "\\bak-v-left-bar.PNG" + "></td>";
          //  html += "<td align='center' valign='middle' style='font-size:16;border:0px;border-color:#aaaaaa;border-style:solid'>";
          //  html += "<img width='96px' height='96px' src='" + logoimg + "'></td> ";
           //html += "<tr>" ;
           //html += "<td align='center' valign='middle' style='background-image:url(\"" + logoimg + "\");background-repeat:no-repeat;background-position: center;font-size:16;' ></td>";
            //html += "</tr>";
        //    html += "<div style='height:100%;width:100%;background-image:url('" + logoimg + "')'>&nbsp</div>";
         //   html += "<td  ><img height='100%' width='auto' src=" + rootpath + "\\bak-v-right-bar.PNG" + "></td></tr>";


          //  html += "<tr><td style='height:auto'><img width='100%' height='auto' src=" + rootpath + "\\bak-h-bottom-bar.PNG" + "></td></tr>";
            //html += "</table>";

            return logoimg;
        }
    }
}
