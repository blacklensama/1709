using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Activities.Presentation.Services;
using System.Activities.Core.Presentation;
using System.Activities.Presentation.Toolbox;
using System.Activities;
using System.Activities.Debugger;
using System.Activities.Statements;
using System.Activities.Presentation.Metadata;
using System.Reflection;

using Wxwinter.BPM.WFDesigner.dialog;
using Wxwinter.BPM.WFDesigner.model;
using MySQLDriverCS;

using System.Windows.Controls.Primitives;



namespace Wxwinter.BPM.WFDesigner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        WorkflowDesigner designer;//提供设计器画布，该设计器画布呈现工作流模型正在设计时的可视表示形式。

        ModelEditingScope modelEditingScope;

        ModelItem rootModelItem;

        UndoEngine undoEngine;

        DesignerView designerView;

        ModelService modelService;

        ToolboxControl nodeToolboxControl;
        ToolboxControl templateListBoxcontrol;
        ToolboxControl userListBoxcontrol;

        public static WFModel wfmodel;

       // private readonly MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            (new DesignerMetadata()).Register();


            (new Wxwinter.BPM.Machine.Design.DesignerMetadata()).Register();



            this.DataContext = this;
            toolBox.bindWindow(this);
            nodeToolboxControl = new ToolboxControl() { Categories = toolBox.loadToolbox() };
                     
            nodePanel.Content = nodeToolboxControl;
            templateListBoxcontrol = new ToolboxControl() { Categories = toolBox.loadTemplatebox() };
            templateList.Content = templateListBoxcontrol;
            userListBoxcontrol = new ToolboxControl() { Categories = toolBox.loadUserbox() };
            userList.Content = userListBoxcontrol;
            
          /*  EditConfig ec = new EditConfig();
            String dbServerIp = ec.ConfigGetValue("dbServerIp");
            String dbUser = ec.ConfigGetValue("dbPassword");
            String dbPassword = ec.ConfigGetValue("dbPassword");*/


        }
        string workflowFilePathName = "tempRun.xaml";


       

        designerDebugTracking tracker;

   
        //加载流程
        void loadWorkflowFromFile(string workflowFilePathName)
        {

            desienerPanel.Content = null;
           // propertyPanel.Content = null;
           

            designer = new WorkflowDesigner();
            
           
            try
            {
                designer.Load(workflowFilePathName);

                modelService = designer.Context.Services.GetService<ModelService>();

                rootModelItem = modelService.Root;

                undoEngine = designer.Context.Services.GetService<UndoEngine>();
         
                undoEngine.UndoUnitAdded += delegate(object ss, UndoUnitEventArgs ee)
                                           {
                                               designer.Flush(); //调用Flush使designer.Text得到数据
                                               desigeerActionList.Items.Add(string.Format("{0}  ,   {1}", DateTime.Now.ToString(), ee.UndoUnit.Description));
                                           };

                designerView = designer.Context.Services.GetService<DesignerView>();



                designerView.WorkflowShellBarItemVisibility = ShellBarItemVisibility.Arguments    //如果不使用Activity做根,无法出现参数选项
                                                              | ShellBarItemVisibility.Imports
                                                              | ShellBarItemVisibility.MiniMap
                                                              | ShellBarItemVisibility.Variables
                                                               | ShellBarItemVisibility.Zoom
                                                              ;

                desienerPanel.Content = designer.View;

                //propertyPanel.Content = designer.PropertyInspectorView;

                propertyGrid.setWfDesigner(designer);

                //this.xamlTextBox.Text = designer.Text;
   
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  //end

        //保存流程
        Boolean saveWorkflowToFile()
        {
            MySQLConnection DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            if (DBConn != null)
            {
                try
                {
                    DBConn.Open();
                    DateTime now = DateTime.Now;
                    String content = designer.Text;//.Replace("\"","\\\"");
                  //  content=Encoding.UTF8.GetString(Encoding.Default.GetBytes(content));// ((Byte[])dr["model_content"]);
                  //  String name = Encoding.UTF8.GetString(Encoding.Default.GetBytes(wfmodel.WFModel_Name));
                    String name = wfmodel.WFModel_Name;
                    string sql = "insert into wf_model (model_name,model_type,owner,lastedit_time,model_content,create_time) values ('"
                      + name + "','" + wfmodel.WFModel_Type+ "','" + wfmodel.WFModel_Owner + "','" + now.ToLocalTime().ToString() + "','"
                      + content + "','" + now.ToLocalTime().ToString() + "' ) on duplicate key update lastedit_time='"
                      + now.ToLocalTime().ToString() + "',model_content='" + content + "'";
                    //  MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);
                    MySQLCommand setformat = new MySQLCommand("set names gb2312", DBConn);
                    setformat.ExecuteNonQuery();
                    setformat.Dispose();
                    MySQLCommand mcd = new MySQLCommand(sql, DBConn);
                    mcd.ExecuteNonQuery();
                }
               
                catch (Exception e)
                {
                    // parentWindow.statusInfo.Text = "数据库连接失败，请检查网络设置和数据库连接配置";
                    return false;
                }
            }
            else { return false; }
            return true;
           
        }




    }//end class


    //UI响应
    public partial class MainWindow
    {

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            if (menuItem == null)
            {
                return;
            }

            string menuItemValue = menuItem.Header.ToString();
            //MessageBox.Show(menuItemValue);
            switch (menuItemValue)
            {
                //---------[工作流 ]---------------------

                case "新建":
                    {
                        dialog.CreateWFModelWindow createWFModelWindow = new dialog.CreateWFModelWindow();
                        createWFModelWindow.ShowDialog();
                        if (createWFModelWindow.WFModelInst != null)
                        {
                            loadWorkflowFromFile("template\\流程图.xaml");
                        }
                        createWFModelWindow.Close();
                        if ((wfmodel = createWFModelWindow.WFModelInst) != null)
                        {
                            workflowFilePathName = createWFModelWindow.WFModelInst.WFModel_Name + ".xaml";
                            this.Title = "流程设计器: " + createWFModelWindow.WFModelInst.WFModel_Name;
                        }
                       
                         //1   workflowFilePathName = "create.xaml";
                        //1 this.Title = "流程设计器: " +"create";
                       /* dialog.createWorkflowWindow createWorkflowWindow = new dialog.createWorkflowWindow();

                       //createWorkflowWindow.ShowDialog();

                        //if (!string.IsNullOrEmpty(createWorkflowWindow.templateName))
                       // {
                       //     loadWorkflowFromFile(createWorkflowWindow.templateName);
                       // }
                       // createWorkflowWindow.Close();


                      //  workflowFilePathName = "tempRun.xaml";

                       // this.Title = "Wxwinter.BPM 流程设计器: " + workflowFilePathName;*/
                    }
                    break;

                case "从数据库打开":
                    dialog.OpenWorkflowWindow openWin = new dialog.OpenWorkflowWindow();
                    openWin.ShowDialog();
                    if (!String.IsNullOrEmpty(openWin.SelectName() ))
                    {
                        if (!String.IsNullOrEmpty(openWin.SelectPath()))
                        {
                            loadWorkflowFromFile(openWin.SelectPath());
                        }
                        else
                        {
                            loadWorkflowFromFile("template\\流程图.xaml");
                        }
                        
                        workflowFilePathName = openWin.SelectName();
                        wfmodel = openWin.SelectModel();
                        this.Title = "表单流转流程设计器:" + workflowFilePathName;
                    } 
             /*       Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
                    if (openDialog.ShowDialog(this).Value)
                    {
                        loadWorkflowFromFile(openDialog.FileName);
                        workflowFilePathName = openDialog.FileName;
                        this.Title = "表单流转流程设计器" + workflowFilePathName;
                    }*/
                    openWin.Close();
                    break;

                case "从文件打开":
                     Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
                    if (openDialog.ShowDialog(this).Value)
                    {
                        loadWorkflowFromFile(openDialog.FileName);
                        workflowFilePathName = openDialog.FileName;
                        this.Title = "表单流转流程设计器" + workflowFilePathName;
                    }
                    break;
                case "人员配置":
                    dialog.peopleManager p = new dialog.peopleManager();
                    p.ShowDialog();
                    break;
                case "保存至数据库":
                    if(saveWorkflowToFile())
                        MessageBox.Show("保存成功");
                    else
                        MessageBox.Show("保存失败");
                    break;
                case "数据库配置":
                    dialog.configuration config = new dialog.configuration();
                        config.ShowDialog();
                     templateListBoxcontrol = new ToolboxControl() { Categories = toolBox.loadTemplatebox() };
            templateList.Content = templateListBoxcontrol;
                    break;
                case "撤销":
                    undoEngine.Undo();
                    break;

                case "重做":
                    undoEngine.Redo();
                    break;

                case "清空流程设计跟踪":
                    desigeerActionList.Items.Clear();
                    break;





                //--------- [调试] -----------------
                case "运行":

                    saveWorkflowToFile();
                   // runWorkflow();

                    break;


                case "清除跟踪信息":
                   // trackingList.ItemsSource = null;
                    tracker.clearTrackInfo();
                    break;

                //--------- [查看] -----------------




                case "XAML":

                    (new Window() { Content = new TextBox() { Text = designer.Text, AcceptsReturn = true, HorizontalScrollBarVisibility = ScrollBarVisibility.Visible, VerticalScrollBarVisibility = ScrollBarVisibility.Visible } }).Show();
                    break;

                
                
                //--------- [工具栏] -----------------

                case "Auto ExpandCollapse":
                    nodeToolboxControl.CategoryItemStyle = new System.Windows.Style(typeof(TreeViewItem))
                                    {
                                        Setters = { new Setter(TreeViewItem.IsExpandedProperty, false)}
                                    };
                    break;
            } // end switch


        }//end

        //
        private void trackingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          /*  if (trackingList.SelectedItem != null)
            {
                designerDebugTrackingData td = trackingList.SelectedItem as designerDebugTrackingData;
                this.designer.DebugManagerView.CurrentLocation = td.sourceLocation;
            }*/
        } //end



    

    }//end class

    //流程启动部分
    public partial class MainWindow
    {
        WorkflowApplication instance = null;

        //-----------------------------------------------------

        public void runWorkflow()
        {
           
        } //end

        //
        private void submit_Click(object sender, RoutedEventArgs e)
        {
            /*
            string bookName = bookmarkNameTextbox.Text;
            string inputValue =submitTextbox.Text;

            if (instance != null)
            {
                if (instance.GetBookmarks().Count(p => p.BookmarkName == bookName) == 1)
                {
                    instance.ResumeBookmark(bookName, inputValue);
                }
                else
                {
                    foreach (var v in instance.GetBookmarks())
                    {
                        System.Console.WriteLine("--------请从下面选项中选择一BookmarkName---------------------------");
                        System.Console.WriteLine("BookmarkName:{0}:,OwnerDisplayName:{1}", v.BookmarkName, v.OwnerDisplayName);
                        System.Console.WriteLine("================================");
                    }
                }
            }
            else
            {
                MessageBox.Show("没有创建实例");
            }
            */
        }//end
    }

    //测试部分
    public partial class MainWindow
    {
        //
        private void find_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
            {
                return;
            }

            string buttonValue = button.Content.ToString();

            switch (buttonValue)
            {
                case "查找根容器中所有WriteLine并赋值":
                    {
                        IEnumerable<ModelItem> list = modelService.Find(rootModelItem, typeof(WriteLine));

                        //读取属性
                        foreach (ModelItem item in list)
                        {
                            MessageBox.Show(item.Properties["DisplayName"].Value.ToString());
                        }

                        //属性赋值
                        foreach (ModelItem item in list)
                        {
                            InArgument<String> value = new InArgument<string>("wxwinter");

                            item.Properties["Text"].SetValue(value);
                        }

                    }
                    break;

                
            }
        }//end

        //
        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button == null)
            {
                return;
            }

            string buttonValue = button.Content.ToString();

            switch (buttonValue)
            {
                case "createStaticWorkflow":
                    loadWorkflowFromFile(@"template\sequence.xaml");
                    break;


                case "add":
                    {
                        modelEditingScope = rootModelItem.BeginEdit();

                        //如果容器是FlowChar,要添加的对象要用 FlowStep 包装,添加到 modelItem.Properties["Nodes"]中
                        // FlowStep flowCharNode = new FlowStep();
                        // flowCharNode.Action = new WriteLine { Text = "这是新增加的部分?" };
                        // rootModelItem.Properties["Nodes"].Collection.Add(flowCharNode);

                        //如果容器是Sequence,直接向  modelItem.Properties["Activities"] 中添加
                        rootModelItem.Properties["Activities"].Collection.Add(new Flowchart());

                    }
                    break;

                case "complete":
                    {
                        modelEditingScope.Complete();
                    }
                    break;

                case "revert":
                    {
                        modelEditingScope.Revert();
                    }
                    break;
            }
        }//end

        private void TabItemGotFocusRefreshXamlBox(object sender, RoutedEventArgs e)
        {
            //this.mainViewModel.NotifyChanged("XAML");
            if (this.designer != null)
            {
                this.xamlTextBox.Text = designer.Text;
            }
            
        }
    }
}
