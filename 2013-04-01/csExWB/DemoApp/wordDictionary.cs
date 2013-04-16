using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemoApp
{
    static class wordDictionary
    {
        public static Dictionary<string, List<Dictionary<string, int>>>
            dict = new Dictionary<string, List<Dictionary<string, int>>>();

        public static void init()
        {
            string str;
            string[] array;
            StreamReader st = new StreamReader("dict1.txt", System.Text.Encoding.Default);
            string name = st.ReadLine();
            str = st.ReadLine();
            while (str != null)
            {
                List<Dictionary<string, int>> temp = new List<Dictionary<string, int>>();
                while (str != "#")
                {
                    array = str.Split(' ');
                    if (array.Length == 1)
                    {
                        Dictionary<string, int> temp1 = new Dictionary<string, int>();
                        temp1.Add(array[0], 0);
                        temp.Add(temp1);
                    }
                    else
                    {
                        Dictionary<string, int> temp1 = new Dictionary<string, int>();
                        temp1.Add(array[0], int.Parse(array[1]));
                        temp.Add(temp1);
                    }
                    str = st.ReadLine();
                }
                dict.Add(name, temp);
                name = st.ReadLine();
                str = st.ReadLine();
            }
        }
    }
}
