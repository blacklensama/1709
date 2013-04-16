using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Wxwinter.BPM.ActivitySample.会签
{
    [Designer(typeof(UserDesigner))]
    public class User : NativeActivity
    {
        public User()
            : base()
        {
            this.DisplayName = "参与人";
            
        }

        public enum Gender { Male, Female }

        
     
        #region Public Properties
        [Category("基本信息")]
        [DisplayName("姓名")]
        public InArgument<string> Name { get; set; }

        [Category("基本信息")]
        [DisplayName("部门")]
        public InArgument<string> Department { get; set; }

        [Category("基本信息")]
        [DisplayName("邮箱")]
        public InArgument<string> Email { get; set; }


        // The following are autoimplemented properties (C# 3.0 and up)
        [Category("详细信息")]
        [DisplayName("性别")]
        public InArgument<Gender> PersonGender { get; set; }

        [Category("详细信息")]
        [DisplayName("职务")]
        public InArgument<string> PersonPosition { get; set; }

        [Category("详细信息")]
        [DisplayName("电话")]
        public InArgument<string> Telephone { get; set; }

        [Category("详细信息")]
        [DisplayName("工作职责")]
        public InArgument<string> PersonDuty { get; set; }
        #endregion

        protected override void Execute(NativeActivityContext context)
        {
            

        }
    }
}
