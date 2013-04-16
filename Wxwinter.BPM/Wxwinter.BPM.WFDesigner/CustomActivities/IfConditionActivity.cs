using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wxwinter.BPM.WFDesigner.CustomActivities
{

    [Designer(typeof(IfConditionDesigner))]
    public sealed class IfConditionActivity : NativeActivity
    {

        Collection<Variable> variables;


        public IfConditionActivity()
            : base()
        {
            this.DisplayName = "If条件活动";
            this.variables = new Collection<Variable>();
        }


        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            /*base.CacheMetadata(metadata);
            metadata.AddImplementationVariable(this.currentIndex);*/
        }

        protected override void Execute(NativeActivityContext context)
        {
            foreach (var item in this.variables)
            {
                //  System.Console.WriteLine(item.Get(context).ToString());
                System.Console.WriteLine("variable:" + item.Name);

            }

            InternalExecute(context, null);
        }

        void InternalExecute(NativeActivityContext context, ActivityInstance instance)
        {
        }
    }
}
