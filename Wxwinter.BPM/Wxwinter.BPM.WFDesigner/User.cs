using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Wxwinter.BPM.WFDesigner
{
    [Designer(typeof(UserDesigner))]
    public class User : NativeActivity
    {
        public User() : base()
        {
            this.DisplayName = "参与者";

        }

        public enum Gender { Male, Female }


        #region Public Properties
        [Category("基本信息")]
        [DisplayName("工作证号")]
        public string ID { get; set; }

        [Category("基本信息")]
        [DisplayName("姓名")]
        public string Name { get; set; }

        [Category("基本信息")]
        [DisplayName("部门")]
        public string Department { get; set; }

        [Category("基本信息")]
        [DisplayName("邮箱")]
        public string Email { get; set; }

        // The following are autoimplemented properties (C# 3.0 and up)
        [Category("详细信息")]
        [DisplayName("性别")]
        public Gender PersonGender { get; set; }

        [Category("详细信息")]
        [DisplayName("职务")]
        public String PersonPosition { get; set; }

        [Category("详细信息")]
        [DisplayName("电话")]
        public String Telephone { get; set; }

        [Category("详细信息")]
        [DisplayName("发送分组")]
        public String Group { get; set; }

        [Category("详细信息")]
        [DisplayName("工作职责")]
        public String PersonDuty { get; set; }
        #endregion

        protected override void Execute(NativeActivityContext context)
        {


        }
    }
}
