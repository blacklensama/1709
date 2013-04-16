using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Wxwinter.BPM.WFDesigner.CustomActivities
{


    [Designer(typeof(IfDesigner))]
    public sealed class IfActivity : CustomActivity{
        Collection<Activity> thenActivity;
        Collection<Activity> elseActivity;
        Collection<Variable> variables;

        [Category("其他信息")]
        [DisplayName("条件名称")]
        public String conName { get; set; }
        [Category("其他信息")]
        [DisplayName("条件符号")]
        public String conOp { get; set; }
        [Category("其他信息")]
        [DisplayName("条件值")]
        public String conValue { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（小时）")]
        public String deadHour { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（分钟）")]
        public String deadMinute { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（天）")]
        public String deadDay { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（小时）")]
        public String distHour { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（分钟）")]
        public String distMinute { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（天）")]
        public String distDay { get; set; }

        public IfActivity()
            : base()
        {
            this.DisplayName = "If条件活动";
            this.thenActivity = new Collection<Activity>();
            this.elseActivity = new Collection<Activity>();
            this.variables = new Collection<Variable>();
        }

        public Collection<Activity> ThenActivities
        {
            get
            {
                return this.thenActivity;
            }
        }

        public Collection<Activity> ElseActivities
        {
            get
            {
                return this.elseActivity;
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
                //  System.Console.WriteLine(item.Get(context).ToString());
                System.Console.WriteLine("variable:" + item.Name);

            }

            InternalExecute(context, null);
        }

        void InternalExecute(NativeActivityContext context, ActivityInstance instance)
        {
            //grab the index of the current Activity
            /*int currentActivityIndex = this.currentIndex.Get(context);
            int count= ThenActivities.Count+ElseActivities.Count;
            if (currentActivityIndex == count)
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
            Activity nextChild = ThenActivities[currentActivityIndex];
            context.ScheduleActivity(nextChild, this.onChildComplete);

            //increment the currentIndex
            this.currentIndex.Set(context, ++currentActivityIndex);*/
        }
    }
}
