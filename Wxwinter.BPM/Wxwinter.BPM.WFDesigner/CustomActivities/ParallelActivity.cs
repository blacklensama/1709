using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Wxwinter.BPM.WFDesigner.CustomActivities
{

    [Designer(typeof(ParallelDesigner))]
    public sealed class ParallelActivity : NativeActivity
    {
        Collection<Activity> children;
        Collection<Variable> variables;
        Variable<int> currentIndex;
        CompletionCallback onChildComplete;

        [Category("其他信息")]
        [DisplayName("等价过程")]
        public Collection<Activity> equivalentActivity { get; set; }

        [Category("其他信息")]
        [DisplayName("异常处理")]
        public Collection<Activity> exceptionActivity { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（小时）")]
        public String hour { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（分钟）")]
        public String hour1 { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（小时）")]
        public String minute { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（分钟）")]
        public String minute1 { get; set; }
        public ParallelActivity()
            : base()
        {
            this.DisplayName = "并行活动";
            this.children = new Collection<Activity>();
            this.equivalentActivity = new Collection<Activity>();
            this.exceptionActivity = new Collection<Activity>();
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

