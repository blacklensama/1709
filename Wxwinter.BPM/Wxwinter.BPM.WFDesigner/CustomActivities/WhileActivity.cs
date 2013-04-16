using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Wxwinter.BPM.WFDesigner.CustomActivities
{
    [Designer(typeof(WhileDesigner))]
    public sealed class WhileActivity : CustomActivity
    {
        Collection<Activity> bodyActivity;
        Collection<Variable> variables;

        public WhileActivity()
            : base()
        {
            this.DisplayName = "While循环活动";
            this.bodyActivity = new Collection<Activity>();
            this.variables = new Collection<Variable>();
        }

        public Collection<Activity> BodyActivities
        {
            get
            {
                return this.bodyActivity;
            }
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
                System.Console.WriteLine("variable:" + item.Name);
            }
            InternalExecute(context, null);
        }

        void InternalExecute(NativeActivityContext context, ActivityInstance instance)
        {
           
        }
    }
}
