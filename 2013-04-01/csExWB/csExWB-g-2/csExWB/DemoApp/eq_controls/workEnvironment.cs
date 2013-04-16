using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace DemoApp.eq_controls
{
    public class workEnvironment
    {
        public frmHTMLeditor parentForm = null;
        public workEnvironment(frmHTMLeditor p)
        {
            this.parentForm = p; 
        }

        public void init()
        {
            loadControlViews();
            loadLayoutView();
        }
        public void loadLayoutView()
        {

            if (parentForm == null)
            {
                MessageBox.Show("无法初始化窗口控件");
                return;
            }
            ListView lv = parentForm.layoutView;
            if (lv == null)
            {
                MessageBox.Show("无法初始化布局视图");
                return;
            }

            lv.View = View.Details;
            lv.GridLines = true;
            lv.Columns.Add("布局控件", -2, HorizontalAlignment.Left);
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(32, 32);
            lv.LargeImageList = imgList;
            lv.SmallImageList = imgList;

            ListViewGroup groupItem = createGroup("表格", lv);



            createItem(groupItem, baseLayout.layoutTypes.CUS_LAYL_TABLE, ".//icon//table.png", "dnlk2", lv);

            createItem(groupItem, baseLayout.layoutTypes.CUS_LAYL_DIV, ".//icon//div.png", "dnlk2", lv);
             
        }
        public void loadControlViews()
        {
            if (parentForm == null)
            {
                MessageBox.Show("无法初始化窗口控件");
                return;
            }
            ListView lv = parentForm.controlListView;
            if (lv == null)
            {
                MessageBox.Show("无法初始化控件视图");
                return;
            }

            lv.View = View.Details;
            lv.GridLines = true;
            lv.Columns.Add("模版控件", -2, HorizontalAlignment.Left);
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(32, 32);
            lv.LargeImageList = imgList;
            lv.SmallImageList = imgList;

            ListViewGroup groupItem = createGroup("汇报", lv);



            createItem(groupItem, controlBase.controlTypes.CUS_CTRL_BRIEF, ".//icon//pr_title_file_document.jpg", "dnlk2", lv);
            createItem(groupItem, controlBase.controlTypes.CUS_CTRL_ALERT, ".//icon//alert.jpg", "dnlk1", lv);
            createItem(groupItem, controlBase.controlTypes.CUS_CTRL_INFORMATION, ".//icon//Notepad.png", "dnlk2", lv);


            ListViewGroup groupItem2 = createGroup("态势图", lv);

            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_DECAY, ".//icon//net.png", "dnlk3", lv);
            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_HUMAN, ".//icon//users 1.png", "dnlk4", lv);
            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_HOUSE, ".//icon//iHome.png", "dnlk4", lv);
            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_DANGER, ".//icon//danger.png", "dnlk5", lv);
            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_ECONOMY, ".//icon//chart.png", "dnlk4", lv);
            createItem(groupItem2, controlBase.controlTypes.CUS_CTRL_LIEFLINE, ".//icon//Ambulance.png", "dnlk4", lv);



            ListViewGroup groupItem3 = createGroup("救援分析", lv);
            createItem(groupItem3, controlBase.controlTypes.CUS_CTRL_RESCUEREQ, ".//icon//Maps.png", "dnlk4", lv);
            createItem(groupItem3, controlBase.controlTypes.CUS_CTRL_RESCUEPLAN, ".//icon//compass.png", "dnlk4", lv);

            ListViewGroup groupItem4 = createGroup("交互控件", lv);

            createItem(groupItem4, controlBase.controlTypes.CUS_CTRL_RADIO, ".//icon//doc trans.png", "dnlk4", lv);
            createItem(groupItem4, controlBase.controlTypes.CUS_CTRL_TEXTAREA, ".//icon//doc trans.png", "dnlk4", lv);


            ListViewGroup groupItem5 = createGroup("自定义", lv);

        }
        private ListViewItem createItem(ListViewGroup groupItem, baseLayout.layoutTypes cType, string iconpath, string linkId, ListView lv)
        {
            ListViewItem item = new ListViewItem(" " + baseLayout.getControTypeName(cType), groupItem);

            loadICON(item, iconpath, lv);
            lv.Items.Add(item);
            item.Tag = baseLayout.createLayout(cType);
            
            return item;
        }

        private ListViewItem createItem(ListViewGroup groupItem, controlBase.controlTypes cType, string iconpath, string linkId, ListView lv)
        {
            ListViewItem item = new ListViewItem(" " + controlBase.getControTypeName(cType), groupItem);

            loadICON(item, iconpath, lv);
            lv.Items.Add(item);
            item.Tag =controlBase.createBase( cType);
           // item.ToolTipText = iconpath;
            return item;
        }
        private ListViewGroup createGroup(string groupname, ListView lv)
        {
            ListViewGroup groupItem = new ListViewGroup(groupname);

            lv.Groups.Add(groupItem);
            return groupItem;
        }

        private void loadICON(ListViewItem item, string path, ListView lv)
        {
            Bitmap map = null;
            try
            {
                map = new Bitmap(path);
            }
            catch (Exception exp)
            {
                map = new Bitmap(".//icon//doc trans.png");
                path = ".//icon//doc trans.png";
            }
            map.Tag = path;

            lv.SmallImageList.Images.Add(map);

            item.ImageIndex = lv.SmallImageList.Images.Count - 1;

           
        }
        public void appendLayout(ListViewItem item)
        {
            if (item == null)
                return;

            if (item.Tag == null)
                return;

            if (item.Tag is baseLayout)
            {
                baseLayout ctrl = (baseLayout)(item.Tag);
                if (item.Tag is layouts.table)
                    ctrl = (layouts.table)(item.Tag);

                if (item.Tag is layouts.div)
                    ctrl = (layouts.div)(item.Tag);


                string html = ctrl.createHtmlView();
                parentForm.addControl(html);
            }
        }

        public void appendControl(ListViewItem item)
        {
            if (item == null)
                return  ;

            if (item.Tag == null)
                return;

            if (item.Tag is controlBase)
            {
                controlBase ctrl = (controlBase)(item.Tag);
                if (item.Tag is controls.alert)
                    ctrl = (controls.alert)(item.Tag);
                if (item.Tag is controls.brief)
                    ctrl = (controls.brief)(item.Tag);
                if (item.Tag is controls.information)
                    ctrl = (controls.information)(item.Tag);

                if (item.Tag is controls.rescueReq)
                    ctrl = (controls.rescueReq)(item.Tag);

                if (item.Tag is controls.rescuePlan)
                    ctrl = (controls.rescuePlan)(item.Tag);

                if (item.Tag is controls.radioSelection)
                    ctrl = (controls.radioSelection)(item.Tag);

                if (item.Tag is controls.textArea)
                    ctrl = (controls.textArea)(item.Tag);

                
                string html = ctrl.createHtmlView();
                parentForm.addControl(html); 
            }
        }
       

        public void resizeObj(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e.tagName.Equals("DIV"))
            {
                if (e.getAttribute("ltype",1) != null && e.getAttribute("ltype",1) != "_containerDIV")
                {
                    layouts.table t = new DemoApp.eq_controls.layouts.table();
                    t.resize(e);
                }
            }
        }

        public void doDoubleClick(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return;

            if (e.getAttribute("cType", 1).ToString().Equals("eq_ctrl"))
            {
                 controlBase.doDoubleClick(e);
              /*  if (parentForm.deleteObj())
                {
                    string dlk = e.getAttribute("dblink", 1).ToString();
                    if (e.getAttribute("dblink", 1) == null)
                        return;
                    controlBase.controlTypes ct = (controlBase.controlTypes)Enum.Parse(typeof(controlBase.controlTypes), dlk);
                    if (ct == controlBase.controlTypes.CUS_CTRL_RADIO) ;
                    {

                        controls.radioSelection ctrl = new DemoApp.eq_controls.controls.radioSelection(ct);


                        string html = ctrl.createHtmlView();
                        parentForm.addControl(html); 
                    }


                   
                }*/
            }
        }

    }
}
