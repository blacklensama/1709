﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ComponentModel;

namespace Wxwinter.BPM.WFDesigner
{
    [Designer(typeof(resultBookmarkDesigner))]
       public sealed class resultBookmark : NativeActivity<string>
    {
        public InArgument<string> bookmarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get
            { return true; }
        }
        protected override void Execute(NativeActivityContext context)
        {
            string bookmark = context.GetValue(bookmarkName);
            context.CreateBookmark(bookmark, new BookmarkCallback(bookmarkCallback));
            
        }
        void bookmarkCallback(NativeActivityContext context, Bookmark bookmark, object obj)
        {
            this.Result.Set(context, (string)obj);
        }
    }

}
