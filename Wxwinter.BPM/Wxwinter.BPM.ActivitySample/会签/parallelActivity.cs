using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.ActivitySample.会签
{
    [System.ComponentModel.Designer(typeof(parallelDesigner))]
    public sealed class parallelActivity : NativeActivity
    {
        Collection<Activity> children;
        Collection<Variable> variables;
        Variable<int> currentIndex;
        CompletionCallback onChildComplete;

        public parallelActivity()
            : base()
        {
            this.DisplayName = "会签";
            this.children = new Collection<Activity>();
           // this.children.Add(new parallelItem());
          //  this.children.Add(new parallelItem());
            this.variables = new Collection<Variable>();
            this.currentIndex = new Variable<int>();
        }

        public Collection<Activity> Activities
        {
            get
            {
                return this.children;
            }
            /*set
            {
                this.children = value;
            }*/
        }

        [System.Windows.Markup.DependsOn("Variables")]
        public Collection<Variable> Variables
        {
            get
            {
                return this.variables;
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
            //grab the index of the current Activity
            int currentActivityIndex = this.currentIndex.Get(context);
            if (currentActivityIndex == Activities.Count)
            {
                //if the currentActivityIndex is equal to the count of MySequence's Activities
                //MySequence is complete
                return;
            }

            if (this.onChildComplete == null)
            {
                //on completion of the current child, have the runtime call back on this method
                this.onChildComplete = new CompletionCallback(InternalExecute);
            }

            //grab the next Activity in MySequence.Activities and schedule it
            Activity nextChild = Activities[currentActivityIndex];
            context.ScheduleActivity(nextChild, this.onChildComplete);

            //increment the currentIndex
            this.currentIndex.Set(context, ++currentActivityIndex);
        }
    }
}
