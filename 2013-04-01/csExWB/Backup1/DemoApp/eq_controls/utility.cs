using System;
using System.Collections.Generic;
using System.Text;

namespace DemoApp.eq_controls
{
    public class utility
    {
        public static List<string> parseStrings(string s, string seperator)
        {
            List<string> res = new List<string>();
            if (s == null || s.Equals(""))
                return res;
            int step = 0;
            while (true)
            {
                step++;
                if (step >= 100) break;
                if (s.Equals(""))
                    break;
                if (s.Length == 1 && !s.Equals(seperator))
                {
                    res.Add(s);
                    break;
                }
                int index = s.IndexOf(seperator);
                if (index < 0 || index >= s.Length)
                {
                    res.Add(s);
                    break;
                }
                if (index == 0)
                {
                    s = s.Substring(1);
                    continue;
                }

                string s1 = s.Substring(0, index);
                if (!s1.Equals("") && s1.IndexOf(seperator) < 0)
                    res.Add(s1);
                string s2 = s.Substring(index + 1);
                s = s2;
                if (s.Equals(""))
                    break;
            }
            return res;
        }
    }
}
