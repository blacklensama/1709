using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Wxwinter.BPM.WFDesigner.CustomActivities
{
    [Designer(typeof(EquivalentDesigner))]
    public sealed class Equivalent : Activity{
        Activity activity;
        public Equivalent()
            : base()
        {
            this.DisplayName = "等价替换活动";
        }
        public Activity Activity
        {
            get
            {
                return this.activity;
            }
        }
    }
}
