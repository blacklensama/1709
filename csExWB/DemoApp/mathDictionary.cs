using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DemoApp
{
    public static class mathDictionary
    {
        public static Dictionary<string, string> dict = new Dictionary<string, string>();
        public static Dictionary<string, int> mathDict = new Dictionary<string, int>();
        public static Dictionary<string, string> Ddict = new Dictionary<string, string>();
        public static void init()
        {
            string str;
            string[] stringArray;
            StreamReader sr = new StreamReader("dictionary.txt", System.Text.Encoding.Default);
            str = sr.ReadLine();
            while (str != null)
            {
                stringArray = str.Split(' ');
                dict.Add(stringArray[0], stringArray[1]);
                Ddict.Add(stringArray[1], stringArray[0]);
                str = sr.ReadLine();
            }

            /*StreamReader sr1 = new StreamReader("dictionary1.txt", System.Text.Encoding.Default);
            str = sr1.ReadLine();
            while (str != null)
            {
                stringArray = str.Split(' ');
                mathDict.Add(stringArray[0], int.Parse(stringArray[1]));
                str = sr.ReadLine();
            }*/

        }
    }
}
