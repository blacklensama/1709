using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Wxwinter.BPM.WFDesigner.CustomActivities
{
    [Designer(typeof(ExceptionDesigner))]
    public sealed class ExceptionActivity : Activity
    {
        private static int count = 0;
        public ExceptionActivity()
            : base()
        {
            this.DisplayName = "异常处理" + ++count;
            this.MessageUsers = new Collection<User>();
            this.EmailUsers = new Collection<User>();
        }
        #region Public Properties
        [Category("基本信息")]
        [DisplayName("发送手机短信人员")]
        public Collection<User> MessageUsers { get; set; }

        [Category("基本信息")]
        [DisplayName("发送电子邮件人员")]
        public Collection<User> EmailUsers { get; set; }

        [Category("基本信息")]
        [DisplayName("文本消息")]
        public String TextMassage { get; set; }
        #endregion
        
    }
}  
