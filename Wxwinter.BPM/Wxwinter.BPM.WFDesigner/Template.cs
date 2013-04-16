using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;
namespace Wxwinter.BPM.WFDesigner
{
    [Designer(typeof(TemplateDesigner))]
    public class Template : NativeActivity
    {
        public Template()
            : base()
        {
            this.DisplayName = "表单模板";
            
        }

        #region Public Properties
        [Category("基本信息")]
        [DisplayName("名称")]
        public string Name { get; set; }


        [Category("详细信息")]
        [DisplayName("描述")]
        public String PersonPosition { get; set; }

       

       // [Category("详细信息")]
      //  [DisplayName("输出")]
      //  public String Output { get; set; }

      //  [Category("详细信息")]
      //  [DisplayName("输入")]
      //  public String Input { get; set; }
        #endregion

        protected override void Execute(NativeActivityContext context)
        {


        }
    }
}
