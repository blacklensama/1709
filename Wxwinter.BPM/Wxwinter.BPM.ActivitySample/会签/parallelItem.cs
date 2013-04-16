using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Wxwinter.BPM.ActivitySample.会签
{
    [System.ComponentModel.Designer(typeof(parallelItemDesigner))]
    public class parallelItem : CodeActivity
    {

        /*public InArgument<User> 参与人员 { get; set; }
        public InArgument<Template> 表单模板 { get; set; }
        */
        public User 参与人员 { get; set; }
        public Template 表单模板 { get; set; }

        protected override void Execute(CodeActivityContext context)
        {

           // string text = context.GetValue(this.参与人员.姓名);

            //System.Console.WriteLine(text);
        }
    }
}
