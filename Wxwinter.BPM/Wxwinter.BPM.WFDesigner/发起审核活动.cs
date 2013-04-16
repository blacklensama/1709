using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Wxwinter.BPM.WFDesigner.CustomActivities;

namespace Wxwinter.BPM.WFDesigner
{
    [System.ComponentModel.Designer(typeof(发起审核活动Designer))]
    public sealed class 发起审核活动 : CustomActivity
    {
        private static int count = 0;
        public 发起审核活动()
            : base()
        {
            this.DisplayName = "发送表单" + ++count;
            this.RadioName = this.DisplayName;
            this.Actors = new Collection<User>();
            // this.children.Add(new parallelItem());
            //  this.children.Add(new parallelItem());
            this.equivalentActivity = new Collection<Activity>();
            this.exceptionActivity = new Collection<Activity>();
            this.Templates = new Collection<Template>();
        }

        #region Public Properties
        [Category("基本信息")]
        [DisplayName("参与人员")]
        public Collection<User> Actors { get; set; }

        [Category("基本信息")]
        [DisplayName("展示名称")]
        public string RadioName { get; set; }

        [Category("基本信息")]
        [DisplayName("流转表单")]
        public Collection<Template> Templates { get; set; }
         
        [Category("基本信息")]
        [DisplayName("文本消息")]
        public String TextMassage { get; set; }

        [Category("基本信息")]
        [DisplayName("通知表单")]
        public string needFeedback { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（天）")]
        public String deadDay { get; set; }

        [Category("其他信息")]
        [DisplayName("等价过程")]
        public Collection<Activity> equivalentActivity { get; set; }

        [Category("其他信息")]
        [DisplayName("异常处理")]
        public Collection<Activity> exceptionActivity { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（小时）")]
        public String deadHour { get; set; }

        [Category("其他信息")]
        [DisplayName("截止期限（分钟）")]
        public String deadMinute { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（天）")]
        public String distDay { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（小时）")]
        public String distHour { get; set; }

        [Category("其他信息")]
        [DisplayName("时间距离（分钟）")]
        public String distMinute { get; set; }

        #endregion
        protected override bool CanInduceIdle
        {
            get
            { return true; }
        }
        protected override void Execute(NativeActivityContext context)
        {


        }
    }
}
