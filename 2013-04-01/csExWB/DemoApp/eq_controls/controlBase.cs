using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IfacesEnumsStructsClasses;
using System.Runtime.InteropServices;
using System.Collections;
namespace DemoApp.eq_controls
{
    public class controlBase
    {
        public enum controlTypes{
           
            CUS_CTRL_ALERT = 0 ,
            CUS_CTRL_BRIEF= 1, 
            CUS_CTRL_INFORMATION = 2 , 

            CUS_CTRL_DECAY = 3 , 
            CUS_CTRL_HUMAN= 4 ,
            CUS_CTRL_HOUSE= 5 , 
            CUS_CTRL_LIEFLINE= 6 ,
            CUS_CTRL_DANGER = 7 ,
            CUS_CTRL_ECONOMY = 8,

            CUS_CTRL_RESCUEREQ = 9,
            CUS_CTRL_RESCUEPLAN= 10,
            CUS_CTRL_MAPLINK=11 ,


            CUS_CTRL_RADIO = 50,
            CUS_CTRL_TEXTAREA = 51,
            CUS_CTRL_SUBMIT = 52,
            CUS_CTRL_EFRAME = 53,
            

            CUS_CTRL_OTHER = 60,
            CUS_CTRL_NULL=100,

            CUS_CTRK_JSON=101,

            CUS_CTRK_JSON_PEOPLE = 102,
            CUS_CTRK_JSON_HORSE = 103, 

            CUS_CTRK_DIV_TIME = 104,
            CUS_DIV = 105
        }
        public controlBase(controlTypes t)
        {
            this.myType = t;
        }

        public controlTypes myType = controlTypes.CUS_CTRL_OTHER;

        public static string getControTypeName(controlTypes e)
        {
            if (e == controlTypes.CUS_CTRL_ALERT)
                return "灾情警报";
            if (e == controlTypes.CUS_CTRL_BRIEF)
                return "灾情简报";
            if (e == controlTypes.CUS_CTRL_INFORMATION)
                return "灾情汇报";

            if (e == controlTypes.CUS_CTRL_DECAY)
                return "衰减分布";

            if (e == controlTypes.CUS_CTRL_HUMAN)
                return "人员损失";
            if (e == controlTypes.CUS_CTRL_HOUSE)
                return "房屋损失";
            if (e == controlTypes.CUS_CTRL_LIEFLINE)
                return "生命线损失";
            if (e == controlTypes.CUS_CTRL_DANGER)
                return "重要目标";
            if (e == controlTypes.CUS_CTRL_ECONOMY)
                return "经济损失";
            if (e == controlTypes.CUS_CTRL_MAPLINK)
                return "地图连接";

            if (e == controlTypes.CUS_CTRL_RESCUEREQ)
                return "救援需求";

            

            if (e == controlTypes.CUS_CTRL_RESCUEPLAN)
                return "救援方案";

            if (e == controlTypes.CUS_CTRL_RADIO)
                return "选择对象";
            if (e == controlTypes.CUS_CTRL_TEXTAREA)
                return "文本栏";

            if (e == controlTypes.CUS_CTRL_SUBMIT)
                return "确认按钮";

            if (e == controlTypes.CUS_CTRL_EFRAME)
                return "引用网站";

            if (e == controlTypes.CUS_CTRL_OTHER)
                return "自定义";

            if (e == controlTypes.CUS_CTRK_JSON)
            {
                return "自定义控件";
            }

            if (e == controlTypes.CUS_CTRK_JSON_PEOPLE)
            {
                return "JSON人口";
            }

            if (e == controlTypes.CUS_CTRK_JSON_HORSE)
            {
                return "JSON房子";
            }

            if (e == controlTypes.CUS_CTRK_DIV_TIME)
            {
                return "时间DIV";
            }

            if (e == controlTypes.CUS_DIV)
            {
                return "地图DIV";
            }
            return "";
        }
        public static controlBase createBase(controlTypes e)
        {
            if (e == controlTypes.CUS_CTRL_ALERT)
                return  new eq_controls.controls.alert(e);
            if (e == controlTypes.CUS_CTRL_BRIEF)
                return new eq_controls.controls.brief(e);
            if (e == controlTypes.CUS_CTRL_INFORMATION)
                return new eq_controls.controls.information(e);

            if (e == controlTypes.CUS_CTRL_RESCUEREQ)
                return new eq_controls.controls.rescueReq(e);

            if (e == controlTypes.CUS_CTRL_RESCUEPLAN)
                return new eq_controls.controls.rescuePlan(e);

            if (e == controlTypes.CUS_CTRL_RADIO)
                return new eq_controls.controls.radioSelection(e);

            if (e == controlTypes.CUS_CTRL_TEXTAREA)
                return new eq_controls.controls.textArea(e);

            if (e == controlTypes.CUS_CTRL_SUBMIT)
                return new eq_controls.controls.submit(e);

            if (e == controlTypes.CUS_CTRL_EFRAME)
                return new eq_controls.controls.eframe(e);
            
            if (e == controlTypes.CUS_CTRK_JSON)
            {
                return new eq_controls.controls.test(e, "test1.json");//测试使用
            }

            if (e == controlTypes.CUS_CTRK_DIV_TIME)
            {
                return new eq_controls.controls.pDiv(e);
            }
            return new eq_controls.imageCtrl(e);             
        }
        virtual public IfacesEnumsStructsClasses.IHTMLElement loadInfoFromElement(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            return null;
        }
        virtual public void deletMeRelation(IfacesEnumsStructsClasses.IHTMLElement e, DemoApp.frmHTMLeditor pform)
        {

        }
        public string createHtmlView()
        {
            string html2 = getMyHtmlContent();
            if (html2.Equals(""))
                return "";

            string title = getTitle();
            string rootpath = System.Windows.Forms.Application.StartupPath;
            string imgPath = getImagePath();
            string bakimg = rootpath + "\\imageobj-bak-1.PNG";
            imgPath = imgPath.Replace("./", "");
            string logoimg = rootpath + imgPath;
            string html = "<table id='"+ new Random().Next().ToString()+"' style='cursor:pointer;width:90%;height:auto' style='border:1px;border-color:#dddddd;border-style:solid' cellpadding=0 cellspacing=0 cType='eq_ctrl' dblink='"+this.myType+"'>";
            html += "<tr><td style='height:24;font-size:12;border:0px;border-color:#dddddd;border-style:solid' color='#aaaaaa' align='left' valign='middle'>";
            html += "<label style='line-height:24px;background:navy;color:#ffffff';font-weight:bold>&nbsp;插入一个\"" + title + "\"对象&nbsp;</label>";
            html += "</td></tr>";


            html += "<tr><td>";

           // html += "<div style=\"-moz-user-select:-moz-none;\"  onselectstart=\"return false;\">";
           
            html += html2; 

            //html += "</div>";
            html += "</td></tr>";
            html += "</table><br/>";
            return html;
        }
        virtual public string getMyHtmlContent()
        {
            return "" ;
        }
        
        virtual public bool loadFromDB()
        {
            return true;
        }
        public string getTitle()
        {
            if (this.myType <= controlTypes.CUS_CTRL_MAPLINK)
                return getControTypeName(myType);

            return "";
        }
        public string getImagePath()
        {

            if (myType == controlTypes.CUS_CTRL_ALERT)
                return ".//icon//alert.jpg";
            if (myType == controlTypes.CUS_CTRL_BRIEF)
                return ".//icon//pr_title_file_document.jpg";
            if (myType == controlTypes.CUS_CTRL_INFORMATION)
                return ".//icon//Notepad.png";

            if (myType == controlTypes.CUS_CTRL_DECAY)
                return ".//icon//net.png";

            if (myType == controlTypes.CUS_CTRL_HUMAN)
                return ".//icon//users 1.png";
            if (myType == controlTypes.CUS_CTRL_HOUSE)
                return ".//icon//iHome.png";
            if (myType == controlTypes.CUS_CTRL_LIEFLINE)
                return ".//icon//Ambulance.png";
            if (myType == controlTypes.CUS_CTRL_DANGER)
                return ".//icon//danger.png";
            if (myType == controlTypes.CUS_CTRL_ECONOMY)
                return ".//icon//chart.png";


            if (myType == controlTypes.CUS_CTRL_RESCUEREQ)
                return ".//icon//Maps.png";



            if (myType == controlTypes.CUS_CTRL_RESCUEPLAN)
                return ".//icon//compass.png";

            return ".//icon//doc trans.png";
 
        }


        public static void doDoubleClick(IfacesEnumsStructsClasses.IHTMLElement e , DemoApp.frmHTMLeditor pform)
        {
            if (e == null)
                return;

            if (e.getAttribute("cType", 1).ToString().Equals("eq_ctrl"))
            {
                string dlk = e.getAttribute("dblink", 1).ToString();
                if (e.getAttribute("dblink", 1) == null)
                    return;
                // (markElement.markStateDef)Enum.Parse(typeof(markElement.markStateDef), (string)(dr["state"]));
                controlTypes ct = (controlTypes)Enum.Parse(typeof(controlTypes), dlk);
                if (ct == controlTypes.CUS_CTRL_RADIO)  
                {
                    controls.radioSelection rs = new DemoApp.eq_controls.controls.radioSelection(ct);
                    IfacesEnumsStructsClasses.IHTMLElement newe = rs.loadInfoFromElement(e);
                  //  if (newe != null)
                  //      pform.addControl(newe);

                }
                if (ct == controlTypes.CUS_CTRL_EFRAME)
                {


                    pform.iframeLoadCount = 1;
                   
                    controls.eframe rs = new DemoApp.eq_controls.controls.eframe(ct);
                    rs.loadInfoFromElement(e);
                    return;
                }
                if (ct == controlTypes.CUS_CTRL_SUBMIT)
                {
                    controls.submit s = new DemoApp.eq_controls.controls.submit(ct);
                    s.pform = pform;

                    s.loadInfoFromElement(e);
                }
            }
        }
        public static void deleteRelation(DemoApp.frmHTMLeditor pform, IfacesEnumsStructsClasses.IHTMLElement e)
        {
            string dlk = e.getAttribute("dblink", 1).ToString();
            if (e.getAttribute("dblink", 1) == null)
                return;
            // (markElement.markStateDef)Enum.Parse(typeof(markElement.markStateDef), (string)(dr["state"]));
            controlTypes ct = (controlTypes)Enum.Parse(typeof(controlTypes), dlk);
            if (ct == controlTypes.CUS_CTRL_TEXTAREA)
            {
                controls.textArea t = new DemoApp.eq_controls.controls.textArea(ct);
                t.deletMeRelation(e, pform);
            }
            if (ct == controlTypes.CUS_CTRL_RADIO)
            {
                controls.radioSelection t = new DemoApp.eq_controls.controls.radioSelection(ct);
                t.deletMeRelation(e, pform);
            }
            
        }
        
    }
}
