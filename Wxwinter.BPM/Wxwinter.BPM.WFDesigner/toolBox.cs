using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities.Presentation.Metadata;
using System.Reflection;
using System.Activities.Presentation.Toolbox;
using System.Data;
using System.Data.Odbc;
using MySQLDriverCS;
using Wxwinter.BPM.WFDesigner.model;

namespace Wxwinter.BPM.WFDesigner
{
   public  class toolBox
    {
       private static MainWindow parentWindow = null;
        public static void loadSystemIcon()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();

            string str = System.Environment.CurrentDirectory + @"\Microsoft.VisualStudio.Activities.dll";
            Assembly sourceAssembly = Assembly.LoadFile(str);


            System.Resources.ResourceReader resourceReader = new System.Resources.ResourceReader(sourceAssembly.GetManifestResourceStream("Microsoft.VisualStudio.Activities.Resources.resources"));
            foreach (Type type in typeof(System.Activities.Activity).Assembly.GetTypes())
            {
                if (type.Namespace == "System.Activities.Statements")
                {
                    createImageToActivity(builder, resourceReader, type);
                }
            }
            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private static void createImageToActivity(AttributeTableBuilder builder, System.Resources.ResourceReader resourceReader, Type builtInActivityType)
        {
            System.Drawing.Bitmap bitmap = getImageFromResource(resourceReader, builtInActivityType.IsGenericType ? builtInActivityType.Name.Split('`')[0] : builtInActivityType.Name);
            if (bitmap != null)
            {
                Type tbaType = typeof(System.Drawing.ToolboxBitmapAttribute);
                Type imageType = typeof(System.Drawing.Image);
                ConstructorInfo constructor = tbaType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { imageType, imageType }, null);
                System.Drawing.ToolboxBitmapAttribute tba = constructor.Invoke(new object[] { bitmap, bitmap }) as System.Drawing.ToolboxBitmapAttribute;
                builder.AddCustomAttributes(builtInActivityType, tba);
            }
        }

        private static System.Drawing.Bitmap getImageFromResource(System.Resources.ResourceReader resourceReader, string bitmapName)
        {
            System.Collections.IDictionaryEnumerator dictEnum = resourceReader.GetEnumerator();
            System.Drawing.Bitmap bitmap = null;
            while (dictEnum.MoveNext())
            {
                if (String.Equals(dictEnum.Key, bitmapName))
                {
                    bitmap = dictEnum.Value as System.Drawing.Bitmap;
                    System.Drawing.Color pixel = bitmap.GetPixel(bitmap.Width - 1, 0);
                    bitmap.MakeTransparent(pixel);
                    break;
                }
            }
            return bitmap;
        }

        public static ToolboxCategoryItems loadTemplatebox()
        {
            MySQLConnection DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "template", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            if (DBConn != null)
            {
                try
                {
                    DBConn.Open();
                    string sql = "select * from template";
                    MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);
                    MySQLCommand mcd = new MySQLCommand(sql, DBConn);
                    mcd.ExecuteNonQuery();
                    DataTable TemplateDataTable = new DataTable();
                    mda.Fill(TemplateDataTable);
                    DBConn.Close();
                    loadSystemIcon();

                    ToolboxCategoryItems toolboxCategoryItems = new ToolboxCategoryItems();
                    ToolboxCategory templates = new System.Activities.Presentation.Toolbox.ToolboxCategory("表单模板");

                    foreach (DataRow dr in TemplateDataTable.Rows)
                    {
                        ToolboxItemWrapper Template = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.Template), dr["NAME"].ToString());
                        templates.Add(Template);
                    }
                    toolboxCategoryItems.Add(templates);
                    parentWindow.statusInfo.Text = "";
                    return toolboxCategoryItems;
                }
                catch (Exception e)
                {
                    parentWindow.statusInfo.Text = "数据库连接失败，请检查网络设置和数据库连接配置";
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }
        public static ToolboxCategoryItems loadUserbox()
        {
            MySQLConnection DBConn = new MySQLConnection(new MySQLConnectionString(Configuration.getDBIp(), "workflow", Configuration.getDBUsername(), Configuration.getDBPassword()).AsString);
            if (DBConn != null)
            {
                try
                {
                    DBConn.Open();
                    string sql1 = "set names gb2312";
                    MySQLCommand DBComm = new MySQLCommand(sql1, DBConn); //設定下達 command
                    DBComm.ExecuteNonQuery();
                    DBComm.Dispose();
                    string sql = "select * from tb_user";
                    MySQLDataAdapter mda = new MySQLDataAdapter(sql, DBConn);
                    DataTable UserDataTable = new DataTable();
                    mda.Fill(UserDataTable);
                    DBConn.Close();
                    loadSystemIcon();

                    ToolboxCategoryItems toolboxCategoryItems = new ToolboxCategoryItems();
                    ToolboxCategory users = new System.Activities.Presentation.Toolbox.ToolboxCategory("系统工作人员");

                    foreach (DataRow dr in UserDataTable.Rows)
                    {
                        //byte[] temp = Encoding.Default.GetBytes(dr["User_Name"].ToString());
                        //temp = System.Text.Encoding.Convert(Encoding.GetEncoding("utf8"), Encoding.GetEncoding("gb2312"), temp);
                        //string username = Encoding.Default.GetString(temp);
                        //ToolboxItemWrapper User = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.User), username);
                        ToolboxItemWrapper User = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.User), dr["User_Name"].ToString());
                        users.Add(User);
                    }
                    toolboxCategoryItems.Add(users);
                    parentWindow.statusInfo.Text = "";
                    return toolboxCategoryItems;
                }
                catch (Exception e)
                {
                    parentWindow.statusInfo.Text = "数据库连接失败，请检查网络设置和数据库连接配置";
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        public static void bindWindow(MainWindow window)
        {
            parentWindow = window;
        }
        public static ToolboxCategoryItems loadToolbox()
        {
            loadSystemIcon();

            ToolboxCategoryItems toolboxCategoryItems = new ToolboxCategoryItems();

            //流程图
   /*         ToolboxItemWrapper flowchar = new ToolboxItemWrapper(typeof(System.Activities.Statements.Flowchart), "Flowchart");
            ToolboxItemWrapper flowDecision = new ToolboxItemWrapper(typeof(System.Activities.Statements.FlowDecision), "FlowDecision");
            ToolboxItemWrapper flowSwitch = new ToolboxItemWrapper(typeof(System.Activities.Statements.FlowSwitch<string>), "FlowSwitch");
            
            ToolboxCategory wf4Flowchar = new System.Activities.Presentation.Toolbox.ToolboxCategory("流程图");

            wf4Flowchar.Add(flowchar);
            wf4Flowchar.Add(flowDecision);
            wf4Flowchar.Add(flowSwitch);

            toolboxCategoryItems.Add(wf4Flowchar);

            //状态机

            ToolboxItemWrapper stateMachineWithInitialStateFactory = new ToolboxItemWrapper(typeof(Wxwinter.BPM.Machine.Design.ToolboxItems.StateMachineWithInitialStateFactory), "状态机流程");
            ToolboxItemWrapper state = new ToolboxItemWrapper(typeof(Wxwinter.BPM.Machine.State), "节点");
            ToolboxCategory stateMachineActivity = new System.Activities.Presentation.Toolbox.ToolboxCategory("状态机");

            stateMachineActivity.Add(stateMachineWithInitialStateFactory);
            stateMachineActivity.Add(state);

            */



            //WF4.0 Activity
            
            ToolboxItemWrapper writeLine = new ToolboxItemWrapper(typeof(System.Activities.Statements.WriteLine), "控制台输出");
            ToolboxItemWrapper sequence = new ToolboxItemWrapper(typeof(System.Activities.Statements.Sequence), "Sequence");
            ToolboxItemWrapper Assign = new ToolboxItemWrapper(typeof(System.Activities.Statements.Assign), "Assign");
            ToolboxItemWrapper Delay = new ToolboxItemWrapper(typeof(System.Activities.Statements.Delay), "Delay");
            ToolboxItemWrapper If = new ToolboxItemWrapper(typeof(System.Activities.Statements.If), "If");
            ToolboxItemWrapper ForEach = new ToolboxItemWrapper(typeof(System.Activities.Statements.ForEach<string>), "ForEach");
            ToolboxItemWrapper Switch = new ToolboxItemWrapper(typeof(System.Activities.Statements.Switch<string>), "Switch");
            ToolboxItemWrapper While = new ToolboxItemWrapper(typeof(System.Activities.Statements.While), "While");
            ToolboxItemWrapper DoWhile = new ToolboxItemWrapper(typeof(System.Activities.Statements.DoWhile), "DoWhile");
            ToolboxItemWrapper Parallel = new ToolboxItemWrapper(typeof(System.Activities.Statements.Parallel), "Parallel");
            ToolboxItemWrapper Pick = new ToolboxItemWrapper(typeof(System.Activities.Statements.Pick), "Pick");
            ToolboxItemWrapper PickBranch = new ToolboxItemWrapper(typeof(System.Activities.Statements.PickBranch), "PickBranch");


            ToolboxCategory wf4Activity = new System.Activities.Presentation.Toolbox.ToolboxCategory("Activity");
            
            wf4Activity.Add(writeLine);
            wf4Activity.Add(sequence);
            wf4Activity.Add(Assign);
            wf4Activity.Add(Delay);
            wf4Activity.Add(If);
            wf4Activity.Add(ForEach);
            wf4Activity.Add(Switch);
            wf4Activity.Add(While);
            wf4Activity.Add(DoWhile);
            wf4Activity.Add(Parallel);
            wf4Activity.Add(Pick);
            wf4Activity.Add(PickBranch);

            //toolboxCategoryItems.Add(wf4Activity);
           
            ToolboxItemWrapper CustomParallel = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.ParallelActivity), "并行活动");
            ToolboxItemWrapper CustomSequence = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.SequenceActivity), "串行活动");
            ToolboxItemWrapper CustomIf = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.IfActivity), "If活动");
            ToolboxItemWrapper CustomWhile = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.WhileActivity), "While循环活动");
            ToolboxItemWrapper CustomException = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.ExceptionActivity), "异常处理");
            ToolboxItemWrapper CustomEquivalent = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.CustomActivities.Equivalent), "等价替换");
            
            ToolboxCategory CustomActivities = new System.Activities.Presentation.Toolbox.ToolboxCategory("自定义设计器");
            
            CustomActivities.Add(CustomParallel);
            CustomActivities.Add(CustomSequence);
           //CustomActivities.Add(CustomWhile);
           CustomActivities.Add(CustomIf);
           //CustomActivities.Add(CustomException);
           //CustomActivities.Add(CustomEquivalent);

       

            ToolboxItemWrapper 发起审核活动 = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.发起审核活动), "表单流转活动");
         
            ToolboxItemWrapper Template = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.Template), "流转表单模板");
            ToolboxItemWrapper Participant = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.User), "参与者");
            ToolboxItemWrapper EndNode = new ToolboxItemWrapper(typeof(Wxwinter.BPM.WFDesigner.EndNode), "结束节点");
            

            CustomActivities.Add(发起审核活动);
            CustomActivities.Add(Template);
            CustomActivities.Add(Participant);
            CustomActivities.Add(EndNode);
            toolboxCategoryItems.Add(CustomActivities);


            return toolboxCategoryItems;

        }
    }
}
