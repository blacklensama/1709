using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Presentation.Model;
using System.Activities.Presentation;
using System.Activities.Presentation.View;

namespace Wxwinter.BPM.WFDesigner
{
   public  class designerTool
    {
        //Activity getDebugActivity()
        //{
        //    ModelService modelService = designer.Context.Services.GetService<ModelService>();

        //    IDebuggableWorkflowTree debugTree = modelService.Root.GetCurrentValue() as IDebuggableWorkflowTree;

        //    if (debugTree != null)
        //    {
        //        return debugTree.GetWorkflowRoot();
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //} //end 


       public static IEnumerable<ModelItem> getSelectActivityList(WorkflowDesigner designer)
       {
           foreach (var v in designer.Context.Items)
           {
               Selection selection = v as Selection;
               if (selection != null)
               {
                   return selection.SelectedObjects;
               }
           }
            return null;
       } //edm

       public static string getXamlFilePath(WorkflowDesigner designer)
       {
           System.Activities.Presentation.WorkflowFileItem fileItem = designer.Context.Items.GetValue(typeof(System.Activities.Presentation.WorkflowFileItem)) as System.Activities.Presentation.WorkflowFileItem;
           return fileItem.LoadedFile;
       }

    }
}
