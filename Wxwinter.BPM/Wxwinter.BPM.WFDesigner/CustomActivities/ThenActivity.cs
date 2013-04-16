using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Wxwinter.BPM.WFDesigner.CustomActivities
{

    [Designer(typeof(ThenDesigner))]
    public sealed class ThenActivity : NativeActivity
    {
        Collection<Activity> children;
        Collection<Variable> variables;
        Variable<int> currentIndex;

        public ThenActivity()
            : base()
        {
            this.DisplayName = "Then活动";
            this.children = new Collection<Activity>();
            this.variables = new Collection<Variable>();
            this.currentIndex = new Variable<int>();
        }

        public Collection<Activity> Activities
        {
            get
            {
                return this.children;
            }
        }

        protected override void CacheMetadata(NativeActivityMetadata metadata)
        {
            //call base.CacheMetadata to add the Activities and Variables to this activity's metadata
            base.CacheMetadata(metadata);
            //add the private implementation variable: currentIndex 
            metadata.AddImplementationVariable(this.currentIndex);
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
