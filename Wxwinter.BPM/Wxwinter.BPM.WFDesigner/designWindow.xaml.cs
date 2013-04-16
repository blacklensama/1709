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
using System.Windows.Navigation;
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

namespace Wxwinter.BPM.WFDesigner
{

    public partial class designWindow : Window
    {
        string workflowFilePathName = "tempRun.xaml";

        WorkflowDesigner designer;

        ModelEditingScope modelEditingScope;

        ModelItem rootModelItem;

        UndoEngine undoEngine;

        DesignerView designerView;

        ModelService modelService;

        ToolboxControl nodeToolboxControl;

       



        //构造函数
        public designWindow()
        {
            InitializeComponent();

            (new DesignerMetadata()).Register();


            (new Wxwinter.BPM.Machine.Design.DesignerMetadata()).Register();



            this.DataContext = this;

            nodeToolboxControl = new ToolboxControl() { Categories = toolBox.loadToolbox() };
          
            nodePanel.Content = nodeToolboxControl;

           
      
        } //end

   
        //加载流程
        void loadWorkflowFromFile(string workflowFilePathName)
        {

            desienerPanel.Content = null;
            propertyPanel.Content = null;


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

                propertyPanel.Content = designer.PropertyInspectorView;

   
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  //end

        //保存流程
        void saveWorkflowToFile()
        {
            {
                if (workflowFilePathName == "tempRun.xaml")
                {
                    Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    if (saveFileDialog.ShowDialog(this).Value)
                    {
                        designer.Save(saveFileDialog.FileName);
                        workflowFilePathName = saveFileDialog.FileName;
                        this.Title = "Wxwinter.BPM 流程设计器  -   " + workflowFilePathName;
                    }
                }
                else
                {
                    designer.Save(workflowFilePathName);
                }

                loadWorkflowFromFile(workflowFilePathName);
            }
        }




    }//end class


    //UI响应
    public partial class designWindow
    {

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            if (menuItem == null)
            {
                return;
            }

            string menuItemValue = menuItem.Header.ToString();

            switch (menuItemValue)
            {
                //---------[工作流 ]---------------------

                case "新建":
                    {
                        dialog.createWorkflowWindow createWorkflowWindow = new dialog.createWorkflowWindow();

                        createWorkflowWindow.ShowDialog();

                        if (!string.IsNullOrEmpty(createWorkflowWindow.templateName))
                        {
                            loadWorkflowFromFile(createWorkflowWindow.templateName);
                        }
                        createWorkflowWindow.Close();


                        workflowFilePathName = "tempRun.xaml";

                        this.Title = "Wxwinter.BPM 流程设计器: " + workflowFilePathName;
                    }
                    break;

                case "打开":
                    Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
                    if (openDialog.ShowDialog(this).Value)
                    {
                        loadWorkflowFromFile(openDialog.FileName);
                        workflowFilePathName = openDialog.FileName;
                        this.Title = "表单流转流程设计器" + workflowFilePathName;
                    }

                    break;

                case "保存":
                    saveWorkflowToFile();
                    MessageBox.Show("保存成功");
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


                case "XAML":

                    (new Window() { Content = new TextBox() { Text = designer.Text, AcceptsReturn = true, HorizontalScrollBarVisibility = ScrollBarVisibility.Visible, VerticalScrollBarVisibility = ScrollBarVisibility.Visible } }).Show();
                    break;

               
            } // end switch


        }//end

        //
        private void trackingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (trackingList.SelectedItem != null)
            {
                designerDebugTrackingData td = trackingList.SelectedItem as designerDebugTrackingData;
                this.designer.DebugManagerView.CurrentLocation = td.sourceLocation;
            }
        } //end





    }//end class

    //流程启动部分
    public partial class designWindow
    {

       
    }

    //测试部分
    public partial class designWindow
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

                case "按名称查找并赋值":
                    {
                        MessageBox.Show("该功能没完成");
                        //   ModelItem item=  modelService.FromName(rootModelItem, findNameTextbox.Text);

                        //   MessageBox.Show(item.Properties["DisplayName"].Value.ToString());

                        //   item.Properties["DisplayName"].SetValue("wxd");
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
      
    }



  
}
