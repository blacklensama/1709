using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls
{
    public class baseLayout
    {
        public enum layoutTypes
        {

            CUS_LAYL_TABLE = 1,
            CUS_LAYL_DIV = 2,

            CUS_LAYL_TIME_DIV = 3,

            CUS_LAYL_OTHER = 50,
            CUS_LAYL_NULL = 100

        }

        virtual public string createHtmlView()
        {

            return "";
        }

        public static baseLayout createLayout(layoutTypes t)
        {
            if(t == layoutTypes.CUS_LAYL_TABLE)
                return new layouts.table() ;

            if (t == layoutTypes.CUS_LAYL_DIV)
                return new layouts.div();

            return null ;
        }
        public static string getControTypeName(layoutTypes t)
        {
            if (t == layoutTypes.CUS_LAYL_TABLE)
                return "表格";

            if (t == layoutTypes.CUS_LAYL_DIV)
                return "DIV";

            return "其他";
        }
    }

   
}
