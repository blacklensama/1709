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

namespace DemoApp
{
    public partial class frmHTMLeditor : Form, IHTMLEventCallBack
    {

        public static ArrayList checkBoxNames = new ArrayList();
        public static ArrayList radioNames = new ArrayList();
        public static string templateName = string.Empty;
        

        public frmHTMLeditor()
        {
            InitializeComponent();
            editorfontCombo.InitComboBox(ToolStripCustomComboBox.ToolStripComboBoxType.Type_Font);

            selectionbackcolorSelector.Selector.SelectionChanged += new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            selectionforecolorSelector1.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            docbackcolorselector.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            docforecolorselector.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            doclinkcolorselector.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            docalinkcolorselector.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);
            docvlinkcolorselector.Selector.SelectionChanged +=new HtmlColorSelectorEventHandler(Selector_SelectionChanged);

           

            //fontsizes
            //Hopefully the fonts and the sizes should
            //update themselves
            SetupComboFontSize();
        }

        #region Private variables and methods

        //This class is used to capture and relay HTMLElementEvents2 of Document object
        //via IHTMLEventCallBack interface implementation in this class
        private csExWB.cHTMLElementEvents m_elemEvents = new csExWB.cHTMLElementEvents();

        //Html editing helper class
        private HTMLEditHelper htmledit = new HTMLEditHelper();

        private Hashtable mapTypeList = null;

        private frmDOM m_frmDOM = new frmDOM();

        private frmCheckboxProp m_frmCheckboxProp = new frmCheckboxProp();
        private frmRadioProp m_frmRadioProp = new frmRadioProp();
        private frmChooseTemplate m_frmChooseTemplate = new frmChooseTemplate();
        private frmDelMapType m_frmDelMapType = new frmDelMapType();
        private frmAddMapType m_frmAddMapType = new frmAddMapType();
        private frmTable m_frmTable = new frmTable();
        private frmMaplinkProp m_frmMaplinkProp = new frmMaplinkProp();
        private frmMaplinkTypeProp m_frmMaplinkTypeProp = new frmMaplinkTypeProp();
        private frmSaveHtmldocProp m_frmSaveHtmldocProp = new frmSaveHtmldocProp();
        private frmSendEmailProp m_frmSendEmailProp = new frmSendEmailProp();
        private frmTableCellProp m_frmTableCellProp = new frmTableCellProp();
        private frmDatalinkProp m_frmDatallinkProp = new frmDatalinkProp();
        private frmFind m_frmFind = new frmFind();
        private frmSaveToDBProp m_frmSaveToDBProp = new frmSaveToDBProp();

        //Is used in browser context menu event to hold a ref
        //to the HTML element under the mouse for further processing
        private IHTMLElement m_oHTMLCtxMenu = null;

        private void SynchEditButtons()
        {
            tsBtnSelectAll.Enabled = this.cEXWB1.IsCommandEnabled("SelectAll");
            //tsBtnCopy.Enabled = this.cEXWB1.IsCommandEnabled("Copy");
            tsBtnCut.Enabled = true;// this.cEXWB1.IsCommandEnabled("Cut");
            tsBtnCopy.Enabled = tsBtnCut.Enabled;
            tsBtnRedo.Enabled = true;//this.cEXWB1.IsCommandEnabled("Redo");
            tsBtnUndo.Enabled = true;//this.cEXWB1.IsCommandEnabled("Undo");
            tsBtnPaste.Enabled = Clipboard.ContainsText();

            cutToolStripMenuItem.Enabled = tsBtnCut.Enabled;
            copyToolStripMenuItem.Enabled = tsBtnCopy.Enabled;
            pasteToolStripMenuItem.Enabled = tsBtnPaste.Enabled;

            tsBtnLeftAlign.Checked = this.cEXWB1.QueryCommandState(true, "JustifyLeft");
            tsBtnRightAlign.Checked = this.cEXWB1.QueryCommandState(true, "JustifyRight");
            tsBtnCenterAlign.Checked = this.cEXWB1.QueryCommandState(true, "JustifyCenter");

            tsBtnBold.Checked = this.cEXWB1.QueryCommandState(true, "Bold");
            tsBtnItalic.Checked = this.cEXWB1.QueryCommandState(true, "Italic");
            tsBtnUnderline.Checked = this.cEXWB1.QueryCommandState(true, "Underline");

            tsBtnBulletList.Checked = this.cEXWB1.QueryCommandState(true, "InsertUnorderedList");
            tsBtnNumberList.Checked = this.cEXWB1.QueryCommandState(true, "InsertOrderedList");
        }

        private void AdjustForHeading(string sTag)
        {
            if (string.IsNullOrEmpty(sTag))
                return;
            int index = 0;
            if (sTag == "H1")
                index = 5; //24pt
            else if (sTag == "H2")
                index = 4; //18pt
            else if (sTag == "H3")
                index = 3; //14pt
            else if (sTag == "H4")
                index = 2; //12pt
            else if (sTag == "H5")
                index = 1; //10pt
            else if (sTag == "H6")
                index = 0; //8pt
            else
                return; //do nothing
            m_InternalCall = true;
            tsComboFontSize.SelectedIndex = index;
        }

        private void SynchFont(string sTagName)
        {
            //Times Roman New
            string fontname = string.Empty;

            object obj = cEXWB1.QueryCommandValue("FontName");
            if (obj == null)
                return;
            fontname = obj.ToString();
            obj = cEXWB1.QueryCommandValue("FontSize");
            if (obj == null)
                return;

            //Could indicate a headingxxx, P, or BODY
            m_InternalCall = true;
            if (obj.ToString().Length > 0)
                tsComboFontSize.SelectedIndex = (int)Convert.ToInt32(obj) - 1; //x (x - 1)
            else
                AdjustForHeading(sTagName);

            editorfontCombo.SelectedFontNameItem = fontname;
        }

        #endregion

        #region Form Events
 
        private void frmHTMLeditor_Load(object sender, EventArgs e)
        {
            try
            {
                
                //Setup icons...
                tsBtnBold.Image = AllForms.m_imgListMain.Images[51];
                tsBtnItalic.Image = AllForms.m_imgListMain.Images[52];
                tsBtnUnderline.Image = AllForms.m_imgListMain.Images[53];
                tsBtnLeftAlign.Image = AllForms.m_imgListMain.Images[56];
                tsBtnCenterAlign.Image = AllForms.m_imgListMain.Images[54];
                tsBtnRightAlign.Image = AllForms.m_imgListMain.Images[57];
                tsBtnFullAlign.Image = AllForms.m_imgListMain.Images[55];
                tsBtnNumberList.Image = AllForms.m_imgListMain.Images[49];
                tsBtnBulletList.Image = AllForms.m_imgListMain.Images[50];
                tsBtnNew.Image = AllForms.m_imgListMain.Images[19];
                tsBtnOpenEdit.Image = AllForms.m_imgListMain.Images[43];
               // tsBtnSave.Image = AllForms.m_imgListMain.Images[21];
                tsBtnPrint.Image = AllForms.m_imgListMain.Images[8];
                tsBtnCut.Image = AllForms.m_imgListMain.Images[23];
                tsBtnCopy.Image = AllForms.m_imgListMain.Images[24];
                tsBtnPaste.Image = AllForms.m_imgListMain.Images[25];
                tsBtnSelectAll.Image = AllForms.m_imgListMain.Images[28];
                tsBtnUndo.Image = AllForms.m_imgListMain.Images[26];
                tsBtnRedo.Image = AllForms.m_imgListMain.Images[27];
                tsBtnTable.Image = AllForms.m_imgListMain.Images[47];
                tsBtnPicture.Image = AllForms.m_imgListMain.Images[46];
                tsBtnLink.Image = AllForms.m_imgListMain.Images[48];
                tsBtnBR.Image = AllForms.m_imgListMain.Images[61];
                tsBtnHorizontalLine.Image = AllForms.m_imgListMain.Images[63];
                tsBtnIndent.Image = AllForms.m_imgListMain.Images[64];
                tsBtnOutdent.Image = AllForms.m_imgListMain.Images[65];

                this.Icon = AllForms.BitmapToIcon(11);

                //Setup HTML tags
                System.Collections.Specialized.StringCollection htmltags = DemoApp.Properties.Settings.Default.HtmlTags;
                foreach (string obj in htmltags)
                {
                    treeHTMLTags.Nodes.Add(obj.ToString());
                }
                
                //Setup richtextbox highlighting, a bit slow
                htmlRichTextBox1.SetupHighLighting(htmltags);
                htmlRichTextBox1.SuppressHightlighting = false;

                //Setup html events
                cEXWB1.RegisterAsBrowser = true; //using default webbrowser dragdrop
                int[] dispids = {  //(int)HTMLEventDispIds.ID_ONKEYUP,
                     (int)HTMLEventDispIds.ID_ONCLICK,
                    (int)HTMLEventDispIds.ID_ONCONTEXTMENU,
                    (int)HTMLEventDispIds.ID_ONDRAG,
                    (int)HTMLEventDispIds.ID_ONDRAGSTART,
                    //(int)HTMLEventDispIds.ID_ONDRAGENTER,
                    //(int)HTMLEventDispIds.ID_ONDRAGOVER,
                    //(int)HTMLEventDispIds.ID_ONDROP,
                    //(int)HTMLEventDispIds.ID_ONDRAGLEAVE,
                    (int)HTMLEventDispIds.ID_ONDRAGEND };
                m_elemEvents.InitHTMLEvents(this, dispids, Iid_Clsids.DIID_HTMLElementEvents2);
                //Initialize webbrowser events
                cEXWB1.NavToBlank();
                //Enter design mode
                this.cEXWB1.SetDesignMode("on");

                //Using our Find dlg
                m_frmFind.FindInPageEvent += new FindInPage(m_frmFind_FindInPageEvent);
                
                //To handle file drops, does not have eny effect on browsers
                //dragdrop functionality.
                this.AllowDrop = true;
                this.DragEnter += new DragEventHandler(frmHTMLeditor_DragEnter);
                //this.DragDrop += new DragEventHandler(frmHTMLeditor_DragDrop);
                this.DragLeave += new EventHandler(frmHTMLeditor_DragLeave);
                //this.DragOver += new DragEventHandler(frmHTMLeditor_DragOver);

               // mapTypeList = DataBase.getMapTypeList();

                /// ghm add

                
                m_elemEvents.canBeEditFunc = new csExWB.cHTMLElementEvents.canBeEdit(this.canBeEdit);
                m_elemEvents.isControlPartFunc = new csExWB.cHTMLElementEvents.isControlPart(this.isControlPartFunc);
                m_elemEvents.resizeFunc = new csExWB.cHTMLElementEvents.resizeWork(this.resizeFunc);
                m_elemEvents.doubleClickFunc = new csExWB.cHTMLElementEvents.doubleClickWork(this.doubleClickFunc);
                m_elemEvents.targetOnOutFunc = new csExWB.cHTMLElementEvents.targetOnOut(this.targetOnOutFunc);
                m_elemEvents.mouseUPFunc = new csExWB.cHTMLElementEvents.mouseUP(this.mouseUPFunc);
                ///
                initListViews();
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmHTMLeditor_Load\r\n" + ee.ToString());
            }
        }

        ////This is called, but not needed
        //void frmHTMLeditor_DragOver(object sender, DragEventArgs e)
        //{
        //    AllForms.m_frmLog.AppendToLog("frmHTMLeditor_DragOver");
        //}

        //This is called instead of DragDrop???
        void frmHTMLeditor_DragLeave(object sender, EventArgs e)
        {
            AllForms.m_frmLog.AppendToLog("frmHTMLeditor_DragLeave");
        }

        ////Never gets called
        //void frmHTMLeditor_DragDrop(object sender, DragEventArgs e)
        //{
        //    AllForms.m_frmLog.AppendToLog("frmHTMLeditor_DragDrop");
        //}

        void frmHTMLeditor_DragEnter(object sender, DragEventArgs e)
        {
            IDataObject dataobj = e.Data;
            //string[] sformats = dataobj.GetFormats();
            //foreach (string ite in sformats)
            //{
            //    AllForms.m_frmLog.AppendToLog("frmHTMLeditor_DragEnter ==>" + ite);
            //}
            //or "FileName" or "FileNameW"
            string[] fnames = (string[])dataobj.GetData("FileDrop");
            foreach(string item in fnames)
            {
                AllForms.m_frmLog.AppendToLog("frmHTMLeditor_DragEnter ==>" + item);
                //only the first filename is used
                break;
            }
        }

        void m_frmFind_FindInPageEvent(string FindPhrase, bool MatchWholeWord, bool MatchCase, bool Downward, bool HighlightAll, string sColor)
        {
            try
            {
                if (HighlightAll)
                {
                    int found = cEXWB1.FindAndHightAllInPage(FindPhrase, MatchWholeWord, MatchCase, sColor, "black");
                    MessageBox.Show(this, "Found " + found.ToString() + " matches.", "Finf in Page", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (cEXWB1.FindInPage(FindPhrase, Downward, MatchWholeWord, MatchCase, true) == false)
                        MessageBox.Show(this, "No more matches found for " + FindPhrase, "Find in Page", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("HTMLEditor_m_frmFind_FindInPageEvent\r\n" + ee.ToString());
            }
        }

        private void frmHTMLeditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void frmHTMLeditor_Activated(object sender, EventArgs e)
        {
            try
            {
                this.cEXWB1.SetFocus();
                htmledit.setCharset();
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmHTMLeditor_Activated\r\n" + ee.ToString());
            }
        }

        private void frmHTMLeditor_Shown(object sender, EventArgs e)
        {
            try
            {
                SynchEditButtons();
                SynchFont(string.Empty);
                ////This should initialize the document to have some sort of basic structure
                //string html = "<HTML><HEAD><meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\"></Head><title>New Page</title><Body><P>&nbsp;</P><Body></HTML>";
                //this.cEXWB1.LoadHtmlIntoBrowser(html);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmHTMLeditor_Shown\r\n" + ee.ToString());
            }
        }

        private void tsFile_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {

                if (e.ClickedItem.Name == tsBtnBulletList.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNORDERLIST);
                }

                else if (e.ClickedItem.Name == tsBtnViewLog.Name)
                {
                    AllForms.m_frmLog.Show(this);
                }
                else if (e.ClickedItem.Name == tsBtnViewDom.Name)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        //load and display DOM, passing Document object
                        m_frmDOM.LoadDOM(this.cEXWB1.WebbrowserObject.Document);
                        if (!m_frmDOM.Visible)
                            m_frmDOM.Show(this);
                        else
                            m_frmDOM.BringToFront();
                        this.Cursor = Cursors.Default;
                    }
                    catch (Exception eee)
                    {
                        this.Cursor = Cursors.Default;
                        AllForms.m_frmLog.AppendToLog("tsToolsMnuDocumentDOM\r\n" + eee.ToString());
                    }
                    return;
                }
                else if (e.ClickedItem.Name == tsBtnNumberList.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_ORDERLIST);
                }
                else if (e.ClickedItem.Name == tsBtnLeftAlign.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYLEFT);
                }
                else if (e.ClickedItem.Name == tsBtnCenterAlign.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYCENTER);
                }
                else if (e.ClickedItem.Name == tsBtnRightAlign.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYRIGHT);
                }
                else if (e.ClickedItem.Name == tsBtnFullAlign.Name)
                {
                    //MSDN, not curently supported
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_JUSTIFYFULL);
                }
                else if (e.ClickedItem.Name == tsBtnRedo.Name)
                {
                    //MSDN, not curently supported
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_REDO);
                }
                else if (e.ClickedItem.Name == tsBtnUndo.Name)
                {
                    //MSDN, not currently supported
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNDO);
                }
                else if (e.ClickedItem.Name == tsBtnBold.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_BOLD);
                }
                else if (e.ClickedItem.Name == tsBtnItalic.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_ITALIC);
                }
                else if (e.ClickedItem.Name == tsBtnUnderline.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNDERLINE);
                }
                else if (e.ClickedItem.Name == tsBtnNew.Name)
                {
                   // cEXWB1.Clear();
                   // setTitle(null);
                    loadDefault();
                }
                else if (e.ClickedItem.Name == tsBtnCut.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_CUT);
                    SynchEditButtons();
                    SynchFont(string.Empty);
                }
                else if (e.ClickedItem.Name == tsBtnCopy.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_COPY);
                }
                else if (e.ClickedItem.Name == tsBtnPaste.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_PASTE);
                    SynchEditButtons();
                    SynchFont(string.Empty);
                }
                else if (e.ClickedItem.Name == tsBtnSelectAll.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_SELECTALL);
                    SynchEditButtons();
                }
                else if (e.ClickedItem.Name == tsBtnTable.Name)
                {
                    m_frmTable.ShowDialog(this);
                    if (m_frmTable.m_Result == DialogResult.OK)
                    {
                        htmledit.AppendTable(m_frmTable.m_Cols, m_frmTable.m_Rows,
                            m_frmTable.m_BorderSize, m_frmTable.m_Alignment,
                            m_frmTable.m_CellPadding, m_frmTable.m_CellSpacing,
                            m_frmTable.m_WidthPercent, m_frmTable.m_WidthPixels,
                            m_frmTable.m_BackColor, m_frmTable.m_BorderColor,
                            m_frmTable.m_LightBorderColor, m_frmTable.m_DarkBorderColor);
                    }

                }
                else if (e.ClickedItem.Name == tsBtnMapLink.Name)
                {
                    m_frmMaplinkProp.ShowDialog(this);
                    if (m_frmMaplinkProp.m_Result == DialogResult.OK)
                    {
                        htmledit.AppendDiv(
                            m_frmMaplinkProp.m_Height,
                            m_frmMaplinkProp.m_Width,
                            m_frmMaplinkProp.m_BorderSize,
                            m_frmMaplinkProp.m_Alignment,
                            m_frmMaplinkProp.m_MapType,
                            m_frmMaplinkProp.m_MapSourceId);
                        m_frmMaplinkProp.m_Result = DialogResult.Cancel;
                    }
                    //                  * */
                }
                else if (e.ClickedItem.Name == tsBtnSaveHtmldoc.Name)
                {
                    if (m_frmSaveHtmldocProp.ShowDialog() == DialogResult.OK)
                    {
                        FileOperation.saveHtmldoc(m_frmSaveHtmldocProp.fileNameTxt.Text,
                                m_frmSaveHtmldocProp.filePathTxt.Text,
                                htmledit.getHtmlChanged(),
                                m_frmSaveHtmldocProp.fileDescriptionTxt.Text);
                    }
                }
                else if (e.ClickedItem.Name == tsBtnSendEmail.Name)
                {
                    if (m_frmSendEmailProp.ShowDialog() == DialogResult.OK)
                    {
                        FileOperation.sendEmail(m_frmSendEmailProp.toNameTxt.Text,
                                m_frmSendEmailProp.themeTxt.Text,
                                htmledit.getHtmlChanged(),
                                m_frmSendEmailProp.chaosongTxt.Text);
                    }
                }
                else if (e.ClickedItem.Name == tsBtnSaveToDb.Name)
                {

                    saveToDB();
                   /* if (frmHTMLeditor.templateName != string.Empty)
                    {
                        if (MessageBox.Show("更新原有模板", "存储为新模板", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (DataBase.updateHtml(frmHTMLeditor.templateName,
                                        htmledit.getHtmlSource(),
                                        m_frmSaveToDBProp.templateDescriptionTxt.Text) == false)
                                MessageBox.Show("存储失败：表单模板有重名！");
                        }
                        else
                            if (m_frmSaveToDBProp.ShowDialog() == DialogResult.OK)
                            {
                                if (DataBase.insertHtml(m_frmSaveToDBProp.templateNameTxt.Text,
                                        htmledit.getHtmlSource(),
                                        m_frmSaveToDBProp.templateDescriptionTxt.Text) == false)
                                    MessageBox.Show("存储失败：表单模板有重名！");
                                else
                                {
                                    frmHTMLeditor.templateName = m_frmSaveToDBProp.templateNameTxt.Text;
                                    setTitle(frmHTMLeditor.templateName);
                                }
                            }
                    }
                    else if (m_frmSaveToDBProp.ShowDialog() == DialogResult.OK)
                    {
                        if (DataBase.insertHtml(m_frmSaveToDBProp.templateNameTxt.Text,
                                htmledit.getHtmlSource(),
                                m_frmSaveToDBProp.templateDescriptionTxt.Text) == false)
                            MessageBox.Show("存储失败：表单模板有重名！");
                        else
                        {
                            frmHTMLeditor.templateName = m_frmSaveToDBProp.templateNameTxt.Text;
                            setTitle(frmHTMLeditor.templateName);
                        }
                    }*/
                }
                else if (e.ClickedItem.Name == tsBtnLink.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_HYPERLINK);
                }
                else if (e.ClickedItem.Name == tsBtnCheckbox.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_CHECKBOX);
                }
                else if (e.ClickedItem.Name == tsBtnRadiobox.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_RADIOBUTTON);
                }
                else if (e.ClickedItem.Name == tsBtnButton.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_BUTTON);
                }
                else if (e.ClickedItem.Name == tsBtnPicture.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_IMAGE);
                }
                else if (e.ClickedItem.Name == tsBtnDataLink.Name)
                {
                    if (m_frmDatallinkProp.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("结束当前定义");
                        return;
                    }
                    string tkname = m_frmDatallinkProp.targetKeyNameTxt.Text;
                    string tkval = m_frmDatallinkProp.targetValueTxt.Text;
                    string lkval = m_frmDatallinkProp.linkKeyNameTxt.Text;
                    htmledit.AppendDatalink(tkname, tkval, lkval);
                    //cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_TEXTBOX);
                }
                else if (e.ClickedItem.Name == tsBtnTextarea.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_TEXTAREA);
                }
                else if (e.ClickedItem.Name == tsBtnBR.Name)
                {
                    htmledit.EmbedBr();
                }
                else if (e.ClickedItem.Name == tsBtnIndent.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_INDENT);
                }
                else if (e.ClickedItem.Name == tsBtnOutdent.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_OUTDENT);
                }
                else if (e.ClickedItem.Name == tsBtnHorizontalLine.Name)
                {
                    cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_HORIZONTALLINE);
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                AllForms.m_frmLog.AppendToLog("tsFile_ItemClicked\r\n" + ee.ToString());
            }
        }


        private void tsBtnOpenEdit_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == fileToolStripMenuItem.Name)
            {
               /* if (AllForms.ShowStaticOpenDialog(this, AllForms.DLG_HTMLS_FILTER,
                    string.Empty, "C:", true) == DialogResult.OK)
                {
                    string src = string.Empty;
                    using(System.IO.StreamReader reader = new System.IO.StreamReader(AllForms.m_dlgOpen.FileName))
                    {
                        src = reader.ReadToEnd();
                    }
                    if(!string.IsNullOrEmpty(src))
                        this.cEXWB1.LoadHtmlIntoBrowser(src);
                    //cEXWB1.Navigate(AllForms.m_dlgOpen.FileName);
                }*/

                loadfromFile();
            }
            else if (e.ClickedItem.Name == dataBaseToolStripMenuItem.Name)
            {
              /*  Control ctl = this.Owner;
                if ((ctl != null) && (ctl is frmMain))
                {
                    if (m_frmChooseTemplate.ShowDialog(this) == DialogResult.OK)
                    {
                        string templateStr = DataBase.getTemplate((string)m_frmChooseTemplate.comboBox.Text);
                        //if (templateStr == null || templateStr == string.Empty)
                        //    templateStr = (string)m_frmChooseTemplate.comboBox.Items.GetEnumerator().Current;
                        if (templateStr == null)
                        {
                            MessageBox.Show("读取表单模板失败");
                        }
                        else
                        {
                            frmHTMLeditor.templateName = (string)m_frmChooseTemplate.comboBox.SelectedItem;
                            this.cEXWB1.LoadHtmlIntoBrowser(templateStr);
                            setTitle(frmHTMLeditor.templateName);

                        }
                    }
                }*/

                loadfromDB();
            }
            else if (e.ClickedItem.Name == clipboardToolStripMenuItem.Name)
            {
                if (Clipboard.ContainsText())
                {
                    string ssource = Clipboard.GetText();
                    if ((!string.IsNullOrEmpty(ssource)) &&
                        (ssource.StartsWith("<HTML>", StringComparison.CurrentCultureIgnoreCase)) &&
                        (ssource.EndsWith("</HTML>", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        setTitle("剪切板");
                        //htmledit.setHtmlSource(ssource);
                        this.cEXWB1.LoadHtmlIntoBrowser(ssource);
                    }
                }
            }
        }

        private void BrowserCtxMenuClickHandler(object sender, EventArgs e)
        {
            if (m_oHTMLCtxMenu == null)
                return;
            if (sender == cutToolStripMenuItem)
            {
                cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_CUT);
                SynchEditButtons();
                SynchFont(string.Empty);
            }
            else if (sender == copyToolStripMenuItem)
            {
                cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_COPY);
            }
            else if (sender == pasteToolStripMenuItem)
            {
                cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_PASTE);
                SynchEditButtons();
                SynchFont(string.Empty);
            }
            else if (sender == editLinkToolStripMenuItem)
            {
                IHTMLAnchorElement phref = (IHTMLAnchorElement)m_oHTMLCtxMenu;
                if (AllForms.m_frmInput.ShowDialogInternal(phref.href, this) == DialogResult.OK)
                {
                    //Normally, we check to see if this is a valid URL
                    if (AllForms.m_frmInput.GetInputBoxText().Length > 0)
                    {
                        phref.href = AllForms.m_frmInput.GetInputBoxText();
                    }
                }
            }
            else if (sender == undoLinkToolStripMenuItem)
            {
                cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_UNLINK);
            }
            else if( (sender == deleteLinkToolStripMenuItem) ||
                (sender == deleteImageToolStripMenuItem) )
            {
                if(AllForms.AskForConfirmation("Proceed to remove element?", this))
                    htmledit.RemoveNode(m_oHTMLCtxMenu, true);
            }
            else if (sender == editImageToolStripMenuItem)
            {
                IHTMLImgElement pimg = (IHTMLImgElement)m_oHTMLCtxMenu;
                if (AllForms.m_frmInput.ShowDialogInternal(pimg.src, this) == DialogResult.OK)
                {
                    //Normally, we check to see if this is a valid URL/File
                    if (AllForms.m_frmInput.GetInputBoxText().Length > 0)
                    {
                        pimg.src = AllForms.m_frmInput.GetInputBoxText();
                    }
                }
            }
            else if (sender == editTablePropertiesToolStripMenuItem)
            {
                IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
                int index = 0;
                if (cell != null)
                    index = cell.cellIndex;
                IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
                //Fill in the m_frmTable fields, display and reset
                if (table != null)
                {
                    string bgColor = string.Empty;
                    if (table.bgColor == null)
                        bgColor = null;
                    else
                        bgColor = (string)table.bgColor;
                    string borderColor = string.Empty;
                    if (table.borderColor == null)
                        borderColor = null;
                    else
                        borderColor = (string)table.borderColor;
                    string borderColorLight = string.Empty;
                    if (table.borderColorLight == null)
                        borderColorLight = null;
                    else
                        borderColorLight = (string)table.borderColorLight;
                    string borderColorDark = string.Empty;
                    if (table.borderColorDark == null)
                        borderColorDark = null;
                    else
                        borderColorDark = (string)table.borderColorDark;

                    m_frmTable.setParams(table.cols, table.rows.length, int.Parse((string)table.border), table.align, int.Parse((string)table.cellPadding), int.Parse((string)table.cellSpacing),
                                        table.width, table.height, bgColor, borderColor, borderColorLight, borderColorDark
                                        );
                    m_frmTable.ShowDialog(this);
                    if (m_frmTable.m_Result == DialogResult.OK)
                    {
                        htmledit.changeTable(table, 20, m_frmTable.m_Cols, m_frmTable.m_Rows,
                            m_frmTable.m_BorderSize, m_frmTable.m_Alignment,
                            m_frmTable.m_CellPadding, m_frmTable.m_CellSpacing,
                            m_frmTable.m_WidthPercent, m_frmTable.m_WidthPixels,
                            m_frmTable.m_BackColor, m_frmTable.m_BorderColor,
                            m_frmTable.m_LightBorderColor, m_frmTable.m_DarkBorderColor);
                    }
                }

            }
            else if (sender == editRadioPropertiesToolStripMenuItem)
            {
                IHTMLElement radio = m_oHTMLCtxMenu as IHTMLElement;
                if (radio == null)
                    return;
                DialogResult result = m_frmRadioProp.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    //添加到已有组别
                    string type = m_frmRadioProp.comboBox1.Text;
                    radio.setAttribute("name", type, 0);
                }
                if (result == DialogResult.Yes)
                {
                    //新建组别
                    string type = m_frmRadioProp.textBox1.Text;
                    if (frmHTMLeditor.radioNames.Contains(type))
                        MessageBox.Show("有重名组，请重新设置新组名");
                    else
                    {
                        radio.setAttribute("name", type, 1);
                        frmHTMLeditor.radioNames.Add(type);
                    }
                }
                if (result != DialogResult.Cancel)
                {
                    radio.outerHTML += m_frmRadioProp.textBox2.Text;
                    //radio.outerText = m_frmRadioProp.textBox2.Text;
                }
            }
            else if (sender == editCheckboxPropertiesToolStripMenuItem)
            {
                IHTMLElement checkbox = m_oHTMLCtxMenu as IHTMLElement;
                if (checkbox == null)
                    return;
                DialogResult result = m_frmCheckboxProp.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    //添加到已有组别
                    string type = m_frmCheckboxProp.comboBox1.Text;
                    checkbox.setAttribute("name", type, 0);
                }
                if (result == DialogResult.Yes)
                {
                    //新建组别
                    string type = m_frmCheckboxProp.textBox1.Text;
                    if (frmHTMLeditor.checkBoxNames.Contains(type))
                        MessageBox.Show("有重名组，请重新设置新组名");
                    else
                    {
                        checkbox.setAttribute("name", type, 1);
                        frmHTMLeditor.checkBoxNames.Add(type);
                    }
                }
                if (result != DialogResult.Cancel)
                {
                    checkbox.outerHTML += m_frmCheckboxProp.textBox2.Text;
                    //radio.outerText = m_frmRadioProp.textBox2.Text;
                }
            }
            else if (sender == editMaplinkPropertiesToolStripMenuItem)
            {
                //还没写 还没写
                IHTMLElement div = m_oHTMLCtxMenu as IHTMLElement;
                if (div == null)
                    return;
                //Need to examine all link properties
                //before passing them to frmLinkProp
                try
                {

                    string height = (String)div.style.height;
                    string width = (String)div.style.width;
                    object obj1 = div.style.borderWidth;
                    string borderSizeStr = (string)div.style.borderWidth;
                    char [] a=new char[2];
                    a[0] = 'p';
                    a[1] = 'x';
                    string borderSizeNum = borderSizeStr.TrimEnd(a);
                    int borderSize = int.Parse(borderSizeNum);
                    string alignment = "center";
                    string mapType = (String)div.getAttribute("mapType", 0);
                    string mapSourceId = (String)div.getAttribute("mapSourceId", 0);

                    m_frmMaplinkProp.setParams(height, width, borderSize, alignment,mapType,mapSourceId);
                    m_frmMaplinkProp.ShowDialog(this);
                    if (m_frmMaplinkProp.m_Result == DialogResult.OK)
                    {
                        string url = "C:\\ICON\\" + mapType + ".jpg";
                        //div.setAttribute("background",url,0);
                        //div.style.backgroundImage = (string)url;
                        div.style.background = "yellow url(" + url + ") "; 
                        div.style.width = m_frmMaplinkProp.m_Width;
                        div.style.height = m_frmMaplinkProp.m_Height;
                        div.style.borderWidth = m_frmMaplinkProp.m_BorderSize + "";

                        div.setAttribute("mapSourceId", m_frmMaplinkProp.m_MapSourceId, 0);
                        div.setAttribute("mapType", m_frmMaplinkProp.m_MapType, 0);

                    }


                }
                catch (Exception eTabelCellProp)
                {
                    AllForms.m_frmLog.AppendToLog("frmHTMLEditor.Cellproperties\r\n" + eTabelCellProp.ToString());
                }
            }
            else if (sender == editDatalinkPropertiesToolStripMenuItem)
            {
                IHTMLElement div = m_oHTMLCtxMenu as IHTMLElement;
                if (div == null)
                    return;
                //Need to examine all link properties
                //before passing them to frmLinkProp
                try
                {
                    string targetKeyNameTxt = string.Empty;
                    if (!string.IsNullOrEmpty((String)div.getAttribute("targerKeyname", 0)))
                        targetKeyNameTxt = (String)div.getAttribute("targerKeyname", 0);

                    string targetValueTxt = string.Empty;
                    if (!string.IsNullOrEmpty((String)div.getAttribute("targerValue", 0)))
                        targetValueTxt = (String)div.getAttribute("targerValue", 0);

                    string linkKeyNameTxt = string.Empty;
                    if (!string.IsNullOrEmpty((String)div.getAttribute("valueKeyname", 0)))
                        linkKeyNameTxt = (String)div.getAttribute("valueKeyname", 0);
                    m_frmDatallinkProp.setParams(targetKeyNameTxt, targetValueTxt, linkKeyNameTxt);
                    if (m_frmDatallinkProp.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("结束当前定义");
                        return;
                    }
                    string tkname = m_frmDatallinkProp.targetKeyNameTxt.Text;
                    string tkval = m_frmDatallinkProp.targetValueTxt.Text;
                    string lkval = m_frmDatallinkProp.linkKeyNameTxt.Text;

                    div.setAttribute("targerKeyname", tkname, 0);
                    div.setAttribute("targerValue", tkval, 0);
                    div.setAttribute("valueKeyname", lkval, 0);

                }
                catch (Exception eTabelCellProp)
                {
                    AllForms.m_frmLog.AppendToLog("frmHTMLEditor.Cellproperties\r\n" + eTabelCellProp.ToString());
                }
            }
            else if (sender == editCellPropertiesToolStripMenuItem)
            {
                IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
                if (cell == null)
                    return;


                //Need to examine all cell properties
                //before passing them to frmTableCellProp

                try
                {
                    string align = string.Empty;
                    if (!string.IsNullOrEmpty(cell.align))
                        align = cell.align;

                    string valign = string.Empty;
                    if (!string.IsNullOrEmpty(cell.vAlign))
                        valign = cell.vAlign;

                    Color bgcolor = Color.Empty;
                    if (cell.bgColor != null)
                        bgcolor = ColorTranslator.FromHtml(cell.bgColor.ToString()); // "#003399"

                    Color bordercolor = Color.Empty;
                    if (cell.borderColor != null)
                        bordercolor = ColorTranslator.FromHtml(cell.borderColor.ToString());

                    Color bordercolorlight = Color.Empty;
                    if (cell.borderColorLight != null)
                        bordercolorlight = ColorTranslator.FromHtml(cell.borderColorLight.ToString());

                    Color bordercolordark = Color.Empty;
                    if (cell.borderColorDark != null)
                        bordercolordark = ColorTranslator.FromHtml(cell.borderColorDark.ToString());

                    m_frmTableCellProp.SetupValues(align, valign, bgcolor,
                        bordercolor, bordercolorlight,
                        bordercolordark, cell.noWrap);

                    m_frmTableCellProp.ShowDialog(this);
                    if (m_frmTableCellProp.m_Result == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_VAlignment))
                            cell.vAlign = m_frmTableCellProp.m_VAlignment;

                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_HAlignment))
                            cell.align = m_frmTableCellProp.m_HAlignment;

                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_BackColor))
                            cell.bgColor = m_frmTableCellProp.m_BackColor;
                        else
                            cell.bgColor = null;

                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_BorderColor))
                            cell.borderColor = m_frmTableCellProp.m_BorderColor;
                        else
                            cell.borderColor = null;

                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_LightBorderColor))
                            cell.borderColorLight = m_frmTableCellProp.m_LightBorderColor;
                        else
                            cell.borderColorLight = null;

                        if (!string.IsNullOrEmpty(m_frmTableCellProp.m_DarkBorderColor))
                            cell.borderColorDark = m_frmTableCellProp.m_DarkBorderColor;
                        else
                            cell.borderColorDark = null;

                        cell.noWrap = m_frmTableCellProp.m_NoWrap;
                    }
                }
                catch (Exception eTabelCellProp)
                {
                    AllForms.m_frmLog.AppendToLog("frmHTMLEditor.Cellproperties\r\n" + eTabelCellProp.ToString());
                }
            }
            else if (sender == insertColToolStripMenuItem)
            {
                IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
                int index = 0;
                if (cell != null)
                    index = cell.cellIndex;
                IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
                if (table == null)
                    return;
                htmledit.InsertCol(table, index); //Need to account for width factor
            }
            else if (sender == insertRowToolStripMenuItem)
            {
                IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
                IHTMLTableRow row = htmledit.GetParentRow(m_oHTMLCtxMenu);
                int index = 0;
                if (row != null)
                    index = row.rowIndex;
                IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
                if (table == null)
                    return;
                htmledit.InsertRow(table, index, htmledit.Row_GetCellCount(row));
            }
            else if (sender == deleteColToolStripMenuItem)
            {
                IHTMLTableCell cell = m_oHTMLCtxMenu as IHTMLTableCell;
                if (cell == null)
                    return;
                int index = cell.cellIndex;
                if (index < 0)
                    index = 0;
                IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
                if (table == null)
                    return;
                htmledit.DeleteCol(table, index);
            }
            else if (sender == deleteRowToolStripMenuItem)
            {
                IHTMLTableRow row = htmledit.GetParentRow(m_oHTMLCtxMenu);
                int index = 0;
                if (row != null)
                    index = row.rowIndex;
                IHTMLTable table = htmledit.GetParentTable(m_oHTMLCtxMenu);
                if (table == null)
                    return;
                htmledit.DeleteRow(table, index);
            }
            else if (sender == viewSourceToolStripMenuItem)
            {
                tabControl1.SelectedTab = tabPageSource;
                //cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_VIEWSOURCE);
            }
        }
        
        private void treeHTMLTags_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if(e.Node != null)
                {
                    string source = "<" + e.Node.Text + ">";
                    //Do we have a selection?
                    if (htmlRichTextBox1.SelectionLength > 0)
                        source = source + htmlRichTextBox1.SelectedText;
                    source = source + "</" + e.Node.Text + ">";
                    htmlRichTextBox1.Focus();
                    htmlRichTextBox1.SelectedText = source;
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("treeHTMLTags_NodeMouseClick\r\n" + ee.ToString());
            }
        }

        private void treeHTMLTags_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (e.Node == null)
                return;
            e.Node.ToolTipText = "Click to insert or surround selection with\r\n<" + e.Node.Text + "> tag.";
        }

        #endregion

        #region Webbrowser Events



        public int iframeLoadCount = 0; 
        private void cEXWB1_DocumentComplete(object sender, csExWB.DocumentCompleteEventArgs e)
        {//ghm modified
            if (iframeLoadCount > 1)
            {
                iframeLoadCount = 0;
                return;
            }
            iframeLoadCount++;

            try
            {
                //A liitle trick to get the IHTMLElement of the document object
                //so we can sink events, using IHTMLDocument2 does not work, neither QIing
                //IHTMLDocument2 for IHTMLElement
               
                
                IHTMLDocument3 pBody = cEXWB1.GetActiveDocument() as IHTMLDocument3;
                
               // IHTMLDocument3 pBody = ((IWebBrowser2)e.browser).Document as IHTMLDocument3;
                m_elemEvents.ConnectToHtmlEvents(pBody.documentElement);
                //AllForms.m_frmLog.AppendToLog("onclick ==> " + m_elemEvents.ConnectToHtmlEvents(pBody.documentElement).ToString());

                IHTMLDocument2 pDoc2 = ((IWebBrowser2)e.browser).Document as IHTMLDocument2;
                //Set the htmleditor document object
                htmledit.DOMDocument = pDoc2;

                if (e.url == "about:blank")
                    return;
                //In response to openning a file
                //See if the document has any bgcolor, fgcolors, ... get them
                if (pDoc2 != null)
                    SynchDocumentColorCombos(pDoc2);
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog(this.Name + "_cEXWB1_DocumentComplete\r\n" + ee.ToString());
            }
        }

        private void cEXWB1_StatusTextChange(object sender, csExWB.StatusTextChangeEventArgs e)
        {
            tsStatus.Text = e.text;
        }

        private void cEXWB1_BeforeNavigate2(object sender, csExWB.BeforeNavigate2EventArgs e)
        {
            m_elemEvents.DisconnectHtmlEvents();
        }

        private void cEXWB1_WBKeyDown(object sender, csExWB.WBKeyDownEventArgs e)
        {
            //Consume keys
            try
            {
                Keys code = e.keycode;
                //ghm

                if (e.virtualkey == Keys.ControlKey)
                {
                    switch (e.keycode)
                    {
                        case Keys.F:
                            m_frmFind.Show(this);
                            e.handled = true;
                            break;
                        case Keys.N:
                            e.handled = true;
                            break;
                        case Keys.O:
                            e.handled = true;
                            break;
                    }
                }
            }
            catch (Exception eex)
            {
                AllForms.m_frmLog.AppendToLog("cEXWBxx_WBKeyDown\r\n" + eex.ToString());
            }
        }

        #endregion

        #region Tab Control

        private TabPage m_LastTabPage = null;

        //For now, I am disabling toolbars if not in edit tab
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage page = tabControl1.SelectedTab;
            if (page == null)
                return;

            this.Cursor = Cursors.WaitCursor;
            try
            {
                string source = string.Empty;
                if (page.Name == tabPageEdit.Name)
                {
                    tsMain.Enabled = true;
                    tsFile.Enabled = true;
                    tsColors.Enabled = true;

                    if (htmlRichTextBox1.Modified)
                    {
                        cEXWB1.SetFocus();
                        source = htmlRichTextBox1.Text;
                        if (!string.IsNullOrEmpty(source))
                            cEXWB1.LoadHtmlIntoBrowser(source);
                    }
                }
                else if (page.Name == tabPagePreview.Name)
                {
                    tsMain.Enabled = false;
                    tsFile.Enabled = false;
                    tsColors.Enabled = false;

                    if ((m_LastTabPage != null) &&
                        (m_LastTabPage.Name == tabPageSource.Name))
                    {
                        source = htmlRichTextBox1.Text;
                        if ((htmlRichTextBox1.Modified) &&
                            (!string.IsNullOrEmpty(source)))
                            cEXWB1.LoadHtmlIntoBrowser(source);
                    }
                    else
                        source = cEXWB1.GetSource(cEXWB1.WebbrowserObject);  //cEXWB1.GetSource(true);
                    if (!string.IsNullOrEmpty(source))
                        cEXWB2.LoadHtmlIntoBrowser(source);
                }
                else if (page.Name == tabPageSource.Name)
                {
                    tsMain.Enabled = false;
                    tsFile.Enabled = false;
                    tsColors.Enabled = false;
                    htmlRichTextBox1.Text = cEXWB1.GetSource(cEXWB1.WebbrowserObject); //cEXWB1.GetSource(true);
                    htmlRichTextBox1.Modified = false;
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("frmHTMLEditor.tabControl1_SelectedIndexChanged\r\n" + ee.ToString());
            }
            this.Cursor = Cursors.Default;
            m_LastTabPage = page;
        }

        #endregion

        #region RichTextBox ContextMenu

        private void ctxMnuRichTextBox_Opening(object sender, CancelEventArgs e)
        {
            cutToolStripMenuItem.Enabled = (htmlRichTextBox1.SelectionLength > 0);
            copyToolStripMenuItem.Enabled = cutToolStripMenuItem.Enabled;
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
            undoLinkToolStripMenuItem.Enabled = htmlRichTextBox1.CanUndo;
            redoToolStripMenuItem.Enabled = htmlRichTextBox1.CanRedo;
            selectAllToolStripMenuItem.Enabled = htmlRichTextBox1.CanSelect;
            saveToolStripMenuItem.Enabled = (htmlRichTextBox1.TextLength > 0);
        }

        private void ctxMnuRichTextBoxMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
                return;
            if (item == cutRtToolStripMenuItem)
                htmlRichTextBox1.Cut();
            else if (item == copyRtToolStripMenuItem)
                htmlRichTextBox1.Copy();
            else if(item == pasteRtToolStripMenuItem)
                htmlRichTextBox1.Paste();
            else if(item == selectAllToolStripMenuItem)
                htmlRichTextBox1.SelectAll();
            else if(item == undoToolStripMenuItem)
                htmlRichTextBox1.Undo();
            else if(item == redoToolStripMenuItem)
                htmlRichTextBox1.Redo();
            else if (item == leftarrowtoolStripMenuItem)
            {
                htmlRichTextBox1.Focus();
                htmlRichTextBox1.SelectedText = htmledit.HtmlTagOpen;
            }
            else if (item == rightarrowtoolStripMenuItem)
            {
                htmlRichTextBox1.Focus();
                htmlRichTextBox1.SelectedText = htmledit.HtmlTagClose;
            }
            else if (item == spaceToolStripMenuItem)
            {
                htmlRichTextBox1.Focus();
                htmlRichTextBox1.SelectedText = htmledit.HtmlSpace;
            }
            else if (item == amptoolStripMenuItem)
            {
                htmlRichTextBox1.Focus();
                htmlRichTextBox1.SelectedText = htmledit.HtmlAmp;
            }
            else if (item == saveToolStripMenuItem)
            {
                if (AllForms.ShowStaticSaveDialogForHTML(this) == DialogResult.OK)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(AllForms.m_dlgSave.FileName))
                    {
                        sw.Write(htmlRichTextBox1.Text);
                    }
                }
            }
        }

        #endregion

        #region Fonts and Color Handling

        //To handle the fact that setting SelectedIndex property calls SelectedIndexChanged event
        //We set this value to true whenever SelectedIndex is set. 
        private bool m_InternalCall = false;
        private void SetupComboFontSize()
        {
            tsComboFontSize.DropDownStyle = ComboBoxStyle.DropDownList;
            tsComboFontSize.Items.Add("1 (8 pt)");
            tsComboFontSize.Items.Add("2 (10 pt)");
            tsComboFontSize.Items.Add("3 (12 pt)");
            tsComboFontSize.Items.Add("4 (14 pt)");
            tsComboFontSize.Items.Add("5 (18 pt)");
            tsComboFontSize.Items.Add("6 (24 pt)");
            tsComboFontSize.Items.Add("7 (36 pt)");
            tsComboFontSize.Click += new EventHandler(tsComboFontSize_Click);
            tsComboFontSize.SelectedIndexChanged += new EventHandler(tsComboFontSize_SelectedIndexChanged);
        }

        void tsComboFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Fontsize changed 1 to 7
                if ((tsComboFontSize.SelectedIndex > -1) && (!m_InternalCall))
                {
                    object obj = tsComboFontSize.SelectedIndex + 1;
                    cEXWB1.ExecCommand(true, "FontSize", false, obj);
                    cEXWB1.SetFocus();
                }
                m_InternalCall = false;
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("tsComboFontSize_SelectedIndexChanged\r\n" + ee.ToString());
            }
        }

        void tsComboFontSize_Click(object sender, EventArgs e)
        {
            m_InternalCall = false;
        }

        void Selector_SelectionChanged(object sender, HtmlColorSelectorEventArgs e)
        {
            HtmlColorSelector item = sender as HtmlColorSelector;
            if( (this.cEXWB1.WebbrowserObject == null) || (item == null) )
                return;
            if (item == selectionbackcolorSelector.Selector)
            {
                //Remove colors?
                if(e.SelectedColor != Color.Empty)
                    cEXWB1.ExecCommand(true, "BackColor", false, ColorTranslator.ToHtml(e.SelectedColor));
                else
                    cEXWB1.ExecCommand(true, "BackColor", false, "");
                return;
            }
            else if (item == selectionforecolorSelector1.Selector)
            {
                if(e.SelectedColor != Color.Empty)
                    cEXWB1.ExecCommand(true, "ForeColor", false, ColorTranslator.ToHtml(e.SelectedColor));
                else
                    cEXWB1.ExecCommand(true, "ForeColor", false, "");
                return;
            }
            IHTMLDocument2 pDoc2 = this.cEXWB1.WebbrowserObject.Document as IHTMLDocument2;
            if (pDoc2 == null)
                return;
            if (item == docbackcolorselector.Selector)
            {
                //Reset backcolor to nothing
                if (e.SelectedColor == Color.Empty)
                {
                    pDoc2.bgColor = "";
                    docbackcolor.BackColor = Control.DefaultBackColor;
                }
                else
                {
                    pDoc2.bgColor = e.SelectedColor.Name;
                    docbackcolor.BackColor = e.SelectedColor;
                }
            }
            else if (item == docforecolorselector.Selector)
            {
                if (e.SelectedColor == Color.Empty)
                {
                    pDoc2.fgColor = "";
                    docforecolor.BackColor = Control.DefaultBackColor;
                }
                else
                {
                    pDoc2.fgColor = e.SelectedColor.Name;
                    docforecolor.BackColor = e.SelectedColor;
                }
            }
            else if (item == doclinkcolorselector.Selector)
            {
                if (e.SelectedColor == Color.Empty)
                {
                    pDoc2.linkColor = "";
                    doclinkcolor.BackColor = Control.DefaultBackColor;
                }
                else
                {
                    pDoc2.linkColor = e.SelectedColor.Name;
                    doclinkcolor.BackColor = e.SelectedColor;
                }
            }
            else if (item == docalinkcolorselector.Selector)
            {
                if (e.SelectedColor == Color.Empty)
                {
                    pDoc2.alinkColor = "";
                    docalinkcolor.BackColor = Control.DefaultBackColor;
                }
                else
                {
                    pDoc2.alinkColor = e.SelectedColor.Name;
                    docalinkcolor.BackColor = e.SelectedColor;
                }
            }
            else if (item == docvlinkcolorselector.Selector)
            {
                if (e.SelectedColor == Color.Empty)
                {
                    pDoc2.vlinkColor = "";
                    docvlinkcolor.BackColor = Control.DefaultBackColor;
                }
                else
                {
                    pDoc2.vlinkColor = e.SelectedColor.Name;
                    docvlinkcolor.BackColor = e.SelectedColor;
                }
            }
        }

        private void editorfontCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((editorfontCombo.SelectedIndex > -1) && 
                    (!editorfontCombo.InternalCall))
                {
                    Font f = editorfontCombo.SelectedFontItem;
                    editorfontCombo.Text = f.Name;
                    cEXWB1.ExecCommand(true, "FontName", false, f.Name);
                    cEXWB1.SetFocus();
                }
            }
            catch (Exception ee)
            {
                AllForms.m_frmLog.AppendToLog("editorfontCombo_SelectedIndexChanged\r\n" + ee.ToString());
            }
        }

        private void SynchDocumentColorCombos(IHTMLDocument2 pDoc2)
        {
            if (pDoc2 == null)
                return;
            if (pDoc2.bgColor != null)
            {
                docbackcolorselector.Selector.SelectedColor = ColorTranslator.FromHtml(pDoc2.bgColor.ToString());
                docbackcolor.BackColor = docbackcolorselector.Selector.SelectedColor;
            }
            else
            {
                docbackcolorselector.Selector.SelectedColor = Color.Empty;
                docbackcolor.BackColor = Control.DefaultBackColor;
            }

            if (pDoc2.fgColor != null)
            {
                docforecolorselector.Selector.SelectedColor = ColorTranslator.FromHtml(pDoc2.fgColor.ToString());
                docforecolor.BackColor = docforecolorselector.Selector.SelectedColor;
            }
            else
            {
                docforecolorselector.Selector.SelectedColor = Color.Empty;
                docforecolor.BackColor = Control.DefaultBackColor;
            }

            if (pDoc2.linkColor != null)
            {
                doclinkcolorselector.Selector.SelectedColor = ColorTranslator.FromHtml(pDoc2.linkColor.ToString());
                doclinkcolor.BackColor = doclinkcolorselector.Selector.SelectedColor;
            }
            else
            {
                doclinkcolorselector.Selector.SelectedColor = Color.Empty;
                doclinkcolor.BackColor = Control.DefaultBackColor;
            }

            if (pDoc2.alinkColor != null)
            {
                docalinkcolorselector.Selector.SelectedColor = ColorTranslator.FromHtml(pDoc2.alinkColor.ToString());
                docalinkcolor.BackColor = docalinkcolorselector.Selector.SelectedColor;
            }
            else
            {
                docalinkcolorselector.Selector.SelectedColor = Color.Empty;
                docalinkcolor.BackColor = Control.DefaultBackColor;
            }


            if (pDoc2.vlinkColor != null)
            {
                docvlinkcolorselector.Selector.SelectedColor = ColorTranslator.FromHtml(pDoc2.vlinkColor.ToString());
                docvlinkcolor.BackColor = docvlinkcolorselector.Selector.SelectedColor;
            }
            else
            {
                docvlinkcolorselector.Selector.SelectedColor = Color.Empty;
                docvlinkcolor.BackColor = Control.DefaultBackColor;
            }

        }

        #endregion

        #region IHTMLEventCallBack Members

        bool IHTMLEventCallBack.HandleHTMLEvent(HTMLEventType EventType, HTMLEventDispIds EventDispId, IHTMLEventObj pEvtObj)
        {
            bool bret = true; //always allow bubbling of events except for contextmenu

            if ((EventDispId == HTMLEventDispIds.ID_ONCLICK) ||
                (EventDispId == HTMLEventDispIds.ID_ONKEYUP))
            {
                checkObjectType(pEvtObj);


                resizeLayout(pEvtObj);

                SynchEditButtons();
                if ((pEvtObj != null) && (pEvtObj.SrcElement != null))
                    SynchFont(pEvtObj.SrcElement.tagName);
                else
                    SynchFont(string.Empty); 
                //if ((pEvtObj != null) && (pEvtObj.SrcElement != null))
                //    AllForms.m_frmLog.AppendToLog("HTMLEvent==>pEvtObj.SrcElement.tagName\r\n" + pEvtObj.SrcElement.tagName);
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONCONTEXTMENU)
            {
                bret = false;
                editLinkToolStripMenuItem.Visible = false;
                undoLinkToolStripMenuItem.Visible = false;
                deleteLinkToolStripMenuItem.Visible = false;
                editImageToolStripMenuItem.Visible = false;
                deleteImageToolStripMenuItem.Visible = false;
                editRadioPropertiesToolStripMenuItem.Visible = false;
                editCheckboxPropertiesToolStripMenuItem.Visible = false;
                editTablePropertiesToolStripMenuItem.Visible = false;
                editDatalinkPropertiesToolStripMenuItem.Visible = false;
                editMaplinkPropertiesToolStripMenuItem.Visible = false;
                insertColToolStripMenuItem.Visible = false;
                insertRowToolStripMenuItem.Visible = false;
                deleteRowToolStripMenuItem.Visible = false;
                deleteColToolStripMenuItem.Visible = false;
                editCellPropertiesToolStripMenuItem.Visible = false;

                m_oHTMLCtxMenu = null;

                if (pEvtObj != null)
                {
                    if (pEvtObj.SrcElement != null)
                    {
                        m_oHTMLCtxMenu = pEvtObj.SrcElement;
                        //AllForms.m_frmLog.AppendToLog("HTMLEvent==>epEvtObj.SrcElement.tagName\r\n" + e.m_pEvtObj.SrcElement.tagName);
                        if (pEvtObj.SrcElement.tagName == "A")
                        {
                            editLinkToolStripMenuItem.Visible = true;
                            undoLinkToolStripMenuItem.Visible = true;
                            deleteLinkToolStripMenuItem.Visible = true;
                        }
                        else if (pEvtObj.SrcElement.tagName == "IMG")
                        {
                            editImageToolStripMenuItem.Visible = true;
                            deleteImageToolStripMenuItem.Visible = true;
                        }
                        else if ((pEvtObj.SrcElement.tagName == "TD"))
                        //|| (pEvtObj.SrcElement.tagName == "TABLE"))
                        {
                            editTablePropertiesToolStripMenuItem.Visible = true;
                            insertColToolStripMenuItem.Visible = true;
                            insertRowToolStripMenuItem.Visible = true;
                            deleteRowToolStripMenuItem.Visible = true;
                            deleteColToolStripMenuItem.Visible = true;
                            editCellPropertiesToolStripMenuItem.Visible = true;
                        }
                        else if ((pEvtObj.SrcElement.tagName == "INPUT"))
                        //|| (pEvtObj.SrcElement.tagName == "TABLE"))
                        {

                            //editRadioPropertiesToolStripMenuItem.Visible = true;
                            //editCheckboxPropertiesToolStripMenuItem.Visible = true;
                            string mapKeyName = string.Empty;
                            try
                            {
                                if (pEvtObj.SrcElement.getAttribute("type", 0) != null && !string.IsNullOrEmpty((String)pEvtObj.SrcElement.getAttribute("type", 0)))
                                {
                                    mapKeyName = (String)pEvtObj.SrcElement.getAttribute("type", 0);
                                    if (mapKeyName.Equals("radio"))
                                        editRadioPropertiesToolStripMenuItem.Visible = true;
                                    else if (mapKeyName.Equals("checkbox"))
                                        editCheckboxPropertiesToolStripMenuItem.Visible = true;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.Write(e.ToString());
                            }

                        }
                        else if ((pEvtObj.SrcElement.tagName == "DIV"))
                        {
                            //editDatalinkPropertiesToolStripMenuItem.Visible = true;
                            //editMaplinkPropertiesToolStripMenuItem.Visible = true;
                            string mapKeyName = string.Empty;
                            try
                            {
                                if (pEvtObj.SrcElement.getAttribute("mapKeyName", 0) != null && !string.IsNullOrEmpty((String)pEvtObj.SrcElement.getAttribute("mapKeyName", 0)))
                                {
                                    mapKeyName = (String)pEvtObj.SrcElement.getAttribute("mapKeyName", 0);
                                    if (!string.IsNullOrEmpty(mapKeyName))
                                        editDatalinkPropertiesToolStripMenuItem.Visible = true;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.Write(e.ToString());
                            }
                            try
                            {
                                string mapSourceId = string.Empty;
                                if (pEvtObj.SrcElement.getAttribute("mapSourceId", 0) != null && !string.IsNullOrEmpty((String)pEvtObj.SrcElement.getAttribute("mapSourceId", 0)))
                                {
                                    mapSourceId = (String)pEvtObj.SrcElement.getAttribute("mapSourceId", 0);
                                    if (!string.IsNullOrEmpty(mapSourceId))
                                        editMaplinkPropertiesToolStripMenuItem.Visible = true;
                                }
                            }
                            catch (Exception e)
                            {
                                System.Console.Write(e.ToString());
                            }
                            

                        }
                        //else if ((pEvtObj.SrcElement.tagName == "P") ||
                        //        (pEvtObj.SrcElement.tagName == "BODY"))
                        //{
                        //}
                    }
                    ctxMnuHTMLEditor.Show(pEvtObj.ScreenX, pEvtObj.ScreenY);
                }
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAG) //fires
            {
                AllForms.m_frmLog.AppendToLog("ID_ONDRAG");
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAGSTART) //fires
            {
                //this is the element that started the drag
                if ((pEvtObj != null) && (pEvtObj.SrcElement != null) ) 
                    AllForms.m_frmLog.AppendToLog("HTMLEvent_ONDRAGSTART==>pEvtObj.SrcElement.tagName\r\n" + pEvtObj.SrcElement.tagName);
                else
                    AllForms.m_frmLog.AppendToLog("ID_ONDRAGSTART");
            }
            else if (EventDispId == HTMLEventDispIds.ID_ONDRAGEND) //fires
            {
                /*
                 * IHTMLEventObj2::dataTransfer
                 * The IHTMLDataTransfer interface retrieved by this method also provides 
                 * access to IDataObject. Call QueryInterface on the IHTMLDataTransfer
                 * interface pointer to obtain an IServiceProvider interface pointer.
                 * Then call IServiceProvider::QueryService, using IID_IDataObject
                 * for the service and interface identifiers, to obtain an IDataObject
                 * interface pointer.
                 * 
                 * IHTMLEventObj2::reason
                 */
                //this is the element which data was dropped on
                
                if ((pEvtObj != null) && (pEvtObj.SrcElement != null))
                {
                    IHTMLEventObj2 eveobj2 = pEvtObj as IHTMLEventObj2;
                    if( (eveobj2 != null) && (eveobj2.dataTransfer != null) )
                    {
                        IfacesEnumsStructsClasses.IServiceProvider pSP = eveobj2.dataTransfer as IfacesEnumsStructsClasses.IServiceProvider;
                        if (pSP != null)
                        {
                            IntPtr pdataobj = IntPtr.Zero;
                            int iret = pSP.QueryService(ref Iid_Clsids.IID_IDataObject, ref Iid_Clsids.IID_IDataObject, out pdataobj);
                            object obj = Marshal.GetObjectForIUnknown(pdataobj);
                            System.Runtime.InteropServices.ComTypes.IDataObject idataobj = obj as System.Runtime.InteropServices.ComTypes.IDataObject;
                            DataObject obja = new DataObject(idataobj);
                            //string[] formats = obja.GetFormats(false);
                            //foreach (string str in formats)
                            //{
                            //    AllForms.m_frmLog.AppendToLog("format ==> " + str);
                            //}
                            AllForms.m_frmLog.AppendToLog("HTMLEvent_ONDRAGEND==> " + obja.GetText(TextDataFormat.Html) );
                        }
                    }
                }
                else
                    AllForms.m_frmLog.AppendToLog("ID_ONDRAGEND");
            }
            //Do not fire
            //else if (EventDispId == HTMLEventDispIds.ID_ONDROP)
            //{
            //    AllForms.m_frmLog.AppendToLog("ID_ONDROP");
            //}
            //else if (EventDispId == HTMLEventDispIds.ID_ONDRAGOVER)
            //{
            //    AllForms.m_frmLog.AppendToLog("ID_ONDRAGOVER");
            //}
            //else if (EventDispId == HTMLEventDispIds.ID_ONDRAGENTER)
            //{
            //    AllForms.m_frmLog.AppendToLog("ID_ONDRAGENTER");
            //}
            //else if (EventDispId == HTMLEventDispIds.ID_ONDRAGLEAVE)
            //{
            //    AllForms.m_frmLog.AppendToLog("ID_ONDRAGLEAVE");
            //}

            return bret;
        }

        #endregion

        private void btnMapType_Click(object sender, EventArgs e)
        {
            ArrayList propList = (ArrayList)mapTypeList[((Button)sender).Name];
            m_frmMaplinkTypeProp.ShowDialog(this);
            if (m_frmMaplinkTypeProp.m_Result == DialogResult.OK)
            {
                htmledit.AppendDiv(
                    m_frmMaplinkTypeProp.m_Height,
                    m_frmMaplinkTypeProp.m_Width,
                    0,
                    (string)propList[1],
                    ((Button)sender).Name,
                    m_frmMaplinkTypeProp.m_MapSourceId);
                m_frmMaplinkTypeProp.m_Result = DialogResult.Cancel;
            }
            cEXWB1.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
           /* if (m_frmAddMapType.ShowDialog(this) == DialogResult.OK)
            {
                if (DataBase.addMapType(
                    m_frmAddMapType.mapTypeName.Text,
                    m_frmAddMapType.mapTypePic.Text,
                    m_frmAddMapType.mapTypeCode.Text,
                    m_frmAddMapType.mapTypeDesc.Text
                    ) == false)
                    MessageBox.Show("添加失败：地图类型有重名！");
            }
            cEXWB1.Focus();*/
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            /*if (m_frmDelMapType.ShowDialog(this) == DialogResult.OK) 
            {
                if (DataBase.deleteMapType((string)m_frmDelMapType.comboBox.SelectedItem) == false)
                {
                    MessageBox.Show("删除失败！");
                }
            }
            cEXWB1.Focus();*/

            cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_TABLE);
        }

        private void tsMain_ItemClicked(object sender, EventArgs e)
        {
            cEXWB1.OleCommandExec(true, MSHTML_COMMAND_IDS.IDM_BOLD);
        }

        private void tsBtnLeftAlign_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            testForm f = new testForm();
            f.ShowDialog();
        }





        DemoApp.eq_controls.workEnvironment env = null;
        IfacesEnumsStructsClasses.IHTMLElement hitElement = null;
        private void initListViews()
        {
            env = new DemoApp.eq_controls.workEnvironment(this);
            env.init();
          

            try
            {
               // string rootpath = System.Windows.Forms.Application.StartupPath + "\\template.html";
              //  cEXWB1.Navigate(rootpath);
                cEXWB1.Focus();
                cEXWB1.deleteFunc = new csExWB.cEXWB.doDeleteObj(this.deleteObj);
                cEXWB1.canBeEditFunc = new csExWB.cEXWB.canBeEdit(this.canBeEdit);

               // cEXWB1.DocumentComplete += new csExWB.DocumentCompleteEventHandler(documentComplete);
            }
            catch (Exception exp)
            {
            }
        }
        public void changeCursor(IHTMLElement e)
        {
            
        }
        public void documentComplete(object sender, csExWB.DocumentCompleteEventArgs e)
        {
           // this.cEXWB1.SetDesignMode("on");
        }
        public bool canBeEdit()
        {
            if (this.hitElement == null)
                return true;
            IHTMLElement p = getCtrlParent(hitElement);
            if (p != null)
                return false;
            IHTMLElement e = cEXWB1.GetElementByCussor(false, true);
            if (e != null)
            {
                p = getCtrlParent(e);
                if (p != null)
                    return false;
            }
            return true;
        }
        public bool isControlPartFunc(IHTMLElement e)
        {
            if (e == null)
                return false;

            IHTMLElement p = getCtrlParent(e);

            if (p==null || p == e)
                return false;
            return true;
        }
        public void resizeFunc(IHTMLElement e)
        {
            if (e == null)
                return;
            env.resizeObj(e);
        }
        public void doubleClickFunc(IHTMLElement e)
        {
            if (e == null)
                return;
            IHTMLElement p = getCtrlParent(e);
            env.doDoubleClick(p);

          //  cEXWB1.Update();
            //this.cEXWB1.LoadHtmlIntoBrowser(htmledit.getHtmlSource());
        }
        public void targetOnOutFunc(IHTMLElement e , int flag)
        {

            if (e == null)
                return;

            if(flag == 1 )
                 this.cEXWB1.Cursor = System.Windows.Forms.Cursors.Hand;

            else this.cEXWB1.Cursor = System.Windows.Forms.Cursors.Default;

            IHTMLElement p = getCtrlParent(e);
            if(p == null )
                this.cEXWB1.Cursor = System.Windows.Forms.Cursors.Default;
        }
        public bool deleteObj()
        {
            if (hitElement != null)
            {
                IHTMLElement p = getCtrlParent(hitElement);
                if (p == null)
                    p = getLayParent(hitElement);
                    
                if(p!= null )
                   env.deletRelations(p);
                if (cEXWB1.deleteElement(p))
                {
                    
                    hitElement = null;
                    return true;
                }
            }
            return false;
           
        }

        private void controlListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.controlListView.SelectedItems.Count <= 0)
                return;

            ListViewItem item = controlListView.SelectedItems[0];

            env.appendControl(item);
        }

        public void reLoadEvent()
        {
            cEXWB1.RegisterAsBrowser = true; //using default webbrowser dragdrop
            int[] dispids = {  //(int)HTMLEventDispIds.ID_ONKEYUP,
                     (int)HTMLEventDispIds.ID_ONCLICK,
                    (int)HTMLEventDispIds.ID_ONCONTEXTMENU,
                    (int)HTMLEventDispIds.ID_ONDRAG,
                    (int)HTMLEventDispIds.ID_ONDRAGSTART,
                    //(int)HTMLEventDispIds.ID_ONDRAGENTER,
                    //(int)HTMLEventDispIds.ID_ONDRAGOVER,
                    //(int)HTMLEventDispIds.ID_ONDROP,
                    //(int)HTMLEventDispIds.ID_ONDRAGLEAVE,
                    (int)HTMLEventDispIds.ID_ONDRAGEND };
            m_elemEvents.InitHTMLEvents(this, dispids, Iid_Clsids.DIID_HTMLElementEvents2);
          
           
          
 
            m_elemEvents.canBeEditFunc = new csExWB.cHTMLElementEvents.canBeEdit(this.canBeEdit);
            m_elemEvents.isControlPartFunc = new csExWB.cHTMLElementEvents.isControlPart(this.isControlPartFunc);
            m_elemEvents.resizeFunc = new csExWB.cHTMLElementEvents.resizeWork(this.resizeFunc);
            m_elemEvents.doubleClickFunc = new csExWB.cHTMLElementEvents.doubleClickWork(this.doubleClickFunc);
            m_elemEvents.targetOnOutFunc = new csExWB.cHTMLElementEvents.targetOnOut(this.targetOnOutFunc);
            m_elemEvents.mouseUPFunc = new csExWB.cHTMLElementEvents.mouseUP(this.mouseUPFunc);
        }
        public void addControl(IHTMLElement e)
        {
            if (e == null)
                return;
            IHTMLDocument2 doc2 = null;
            IHTMLSelectionObject selobj = null;
            IHTMLTxtRange range = null;


            doc2 = cEXWB1.GetActiveDocument();
            if ((doc2 == null) || (doc2.selection == null))
                return;

            selobj = doc2.selection as IHTMLSelectionObject;
            if (selobj == null)
                return;

            if ((selobj.EventType == "none") || (selobj.EventType == "control"))
                return;

            try
            {
                range = selobj.createRange() as IHTMLTxtRange;

                if (range == null)
                    return;


                range.pasteHTML(e.outerHTML);

            }
            catch (Exception exp)
            {

            }

            return;
        }
        public void addControl(string html )
        {
           
            cEXWB1.Focus();
            IfacesEnumsStructsClasses.IHTMLElement targetEleemnt = hitElement;// cEXWB1.GetElementByCussor(false, true);
            if (targetEleemnt == null)
                return;
            string tagname = targetEleemnt.tagName;

            if (isControl(targetEleemnt))
            {
                MessageBox.Show("不能在控件中嵌套对象!");
                return;
            }

            
            IHTMLDocument2 doc2 = null;
            IHTMLSelectionObject selobj = null;
            IHTMLTxtRange range = null;


            doc2 = cEXWB1.GetActiveDocument();
            if ((doc2 == null) || (doc2.selection == null))
                return  ;

            selobj = doc2.selection as IHTMLSelectionObject;
            if (selobj == null)
                return  ;

            if ((selobj.EventType == "none") || (selobj.EventType == "control"))
                return  ;

            try
            {
                range = selobj.createRange() as IHTMLTxtRange;

                if (range == null)
                    return;


                range.pasteHTML(html);
                 
            }
            catch (Exception exp)
            {

            }
           
            return  ;


        }

        public IHTMLElement getCtrlParent(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null;
            if (e.getAttribute("cType", 0) != null && e.getAttribute("cType", 0).Equals("eq_ctrl"))
                return e;

            if (e.parentElement != null)
                return getCtrlParent(e.parentElement);

            return null;

        }
        public IHTMLElement getLayParent(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null;
            if (e.getAttribute("cType", 0) != null && e.getAttribute("cType", 0).Equals("eq_lay"))
                return e;

            if (e.parentElement != null)
                return getLayParent(e.parentElement);

            return null;

        }
        public IHTMLElement getLayChild(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return null;
            if (e.getAttribute("cType", 0) != null && e.getAttribute("cType", 0).Equals("eq_lay"))
                return e;
           
            IfacesEnumsStructsClasses.IHTMLDOMNode ed = (IfacesEnumsStructsClasses.IHTMLDOMNode)e;

            for (int i = 0; i < ed.childNodes.length; i++)
            {
                IfacesEnumsStructsClasses.IHTMLElement ec = (IfacesEnumsStructsClasses.IHTMLElement)(ed.childNodes.item(i));
                if (ec.tagName.Equals("table"))
                {
                    if (ec.getAttribute("cType", 0) != null && ec.getAttribute("cType", 0).Equals("eq_lay"))
                        return ec;
                }
                return getLayChild(ec);
            }
             

            return null;
        }
        public bool isControl(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return false;
            if (e.getAttribute("cType", 0) != null && e.getAttribute("cType", 0).Equals("eq_ctrl"))
                return true;

            if (e.parentElement != null)
                return isControl(e.parentElement);

            return false;

        }
        public void mouseUPFunc(IfacesEnumsStructsClasses.IHTMLElement e)
        {
            if (e == null)
                return;
            cEXWB1.closeHightLight();
            this.hitElement = e; 
        }
        private void controlListView_SelectedIndexChanged(object sender, MouseEventArgs e)
        {
            if (this.controlListView.SelectedItems.Count <= 0)
                return;

            ListViewItem item = controlListView.SelectedItems[0];

            env.appendControl(item);
        }

        private void cEXWB1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void cEXWB1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("a");
        }
        public void  hightLight(IHTMLElement p)
        {
            cEXWB1.closeHightLight();
            if (p == null)
                return;
            p = getCtrlParent(p);
            if (p != null)
            {
                cEXWB1.highlightElement(p, true);
                 
            }

        }
        private void checkObjectType(IHTMLEventObj pEvtObj)
        {
            IHTMLElement p = null;
          

            cEXWB1.closeHightLight();
            hitElement = pEvtObj.SrcElement;
            p = cEXWB1.GetElementByCussor(false, false);
            if (p != null)
            {
                p = getCtrlParent(p);
                if (p != null)
                {
                    cEXWB1.highlightElement(p, true);
                   // cEXWB1.focusToElement(p);
                }
                else
                {
                    IHTMLDocument2 doc2 = hitElement.document as  IHTMLDocument2 ;
                    string u1 = cEXWB1.GetActiveDocument().url ;
                    string u2 = doc2.url ;
                    if (!u1.Equals(u2))
                    {
                        IfacesEnumsStructsClasses.IHTMLElement2 e2 = (IfacesEnumsStructsClasses.IHTMLElement2)(cEXWB1.GetActiveDocument());
                        IfacesEnumsStructsClasses.IHTMLElementCollection c = (e2.getElementsByTagName("iframe")) as IfacesEnumsStructsClasses.IHTMLElementCollection;
                        foreach (IHTMLElement2 ce in c)
                        {
                            IHTMLElement cee = (IHTMLElement)ce;
                            object obj = cee.getAttribute("src", 1);
                            if (obj != null)
                            {
                                string url = obj.ToString();
                                if (url.StartsWith(u2))
                                {
                                   // cEXWB1.highlightElement(cee, true);
                                   // cEXWB1.focusToElement(cee);
                                  //  break; 
                                }
                            }
                         }
                    }
                }
            }
            
        }
        private void resizeLayout(IHTMLEventObj pEvtObj)
        {
            if (pEvtObj.SrcElement == null)
                return;
            IHTMLElement div = pEvtObj.SrcElement;
            if (!div.tagName.Equals("DIV"))
                return;
            
            
        }
        

        private void layoutView_Click(object sender, EventArgs e)
        {
            if (this.layoutView.SelectedItems.Count <= 0)
                return;

            ListViewItem item = layoutView.SelectedItems[0];

            env.appendLayout(item);
        }

        private void setTitle(string title)
        {
            resetData();
            if (title == null || title.Equals("剪切板"))
                frmHTMLeditor.templateName = string.Empty;
            string prefix = "表单模板编辑器--------";
            string nullTitle = prefix + "未命名";
            string templateTitle = prefix + title;
            if (title == null)
            {
                this.Text = nullTitle;
            }
            else
                this.Text = templateTitle;
        }

        private void resetData()
        {
            frmHTMLeditor.checkBoxNames.Clear();
            frmHTMLeditor.radioNames.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
             
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           

            
        }
        private void saveToDB()
        {

            env.m_template.saveToDB();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = env.m_template.name;
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                 

                string path = saveFileDialog1.FileName;
                env.m_template.saveAsFile(path);
            }
        }

        public void loadfromFile()
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;


                env.m_template.loadfromFile(path);

            }
        }

        public void  loadfromDB()
        {

            env.loadFromDB();
        }

        public void loadDefault()
        {
            this.cEXWB1.SetDesignMode("off");
            
            env.m_template.openDefault();
            
            this.cEXWB1.SetDesignMode("on");
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            env.m_template.setTemplate();
        }
         
    }

}