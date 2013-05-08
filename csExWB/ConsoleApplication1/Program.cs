using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Dictionary<string, string> dict = new Dictionary<string, string>();
            string str;
            string[] stringArray;
            StreamReader sr = new StreamReader("dictionary.txt", System.Text.Encoding.Default);
            str = sr.ReadLine();
            while (str != null)
            {
                stringArray = str.Split(' ');
                dict.Add(stringArray[0], stringArray[1]);
                str = sr.ReadLine();
            }*/
            StreamReader reader = new StreamReader("test1.json");
            string str = reader.ReadToEnd();
            JObject jObj = JObject.Parse(str);
            JToken name = jObj.SelectToken("circles");
            foreach (var key in name)
            {
                Console.WriteLine(key.SelectToken("longAxis").ToString());
            }

            /*JObject o = JObject.Parse(@"{
'CPU': 'Intel',
 'Drives': [
  'DVD read/writer',
  '500 gigabyte hard drive'
]
}");*/
            /*string cpu = (string)o["CPU"];

            string firstDrive = (string)o["Drives"][0];

            IList<string> allDrives = o["Drives"].Select(t => (string)t).ToList();
            foreach (var key in allDrives)
            {
                Console.WriteLine(key.ToString());
            }*/
        }
    }
}
