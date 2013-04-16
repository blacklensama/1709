using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.ActivitySample.会签
{
    [System.ComponentModel.Designer(typeof(发起审核活动Designer))]
    public sealed class 发起审核活动 : NativeActivity
    {
        public 发起审核活动()
            : base()
        {
            this.DisplayName = "发起审核活动";
            this.Actors = new Collection<User>();
           // this.children.Add(new parallelItem());
          //  this.children.Add(new parallelItem());
            this.Templates = new Collection<Template>();
        }

        #region Public Properties
        [Category("基本信息")]
        [DisplayName("参与人员")]
        public InArgument<Collection<User>> Actors { get; set; }

        [Category("基本信息")]
        [DisplayName("流转表单")]
        public InArgument<Collection<Template>> Templates { get; set; }

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
