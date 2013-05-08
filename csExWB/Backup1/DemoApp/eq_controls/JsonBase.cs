using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections;

namespace DemoApp.eq_controls
{
    public class jsonParseTemplate
    {
        private string key;
        private string value;
        public jsonParseTemplate(string k, string v)
        {
            key = k;
            value = v;
        }
        public string VALUE
        {
            get
            {
                return key;
            }
        }
        public string KEY
        {
            get
            {
                return value;
            }
        }
    }
    public class jsonBase
    {
        private ArrayList list;
        private void read(string str)
        {
            StreamReader reader = new StreamReader(str);
            JsonTextReader r = new JsonTextReader(reader);
            while (r.Read())
            {
                if (r.Value != null)
                {
                    list.Add(new jsonParseTemplate(r.TokenType.ToString(), r.Value.ToString()));
                }
            }
        }

        public jsonBase(string str)
        {
            list = new ArrayList();
            read(str);
        }

        public ArrayList ARRAY
        {
            get
            {
                return list;
            }
        }

        public string getHtmlTable()
        {
            string html = "";
            html = "";
            html += "<table width='100%' height='auto' cellpadding=0 cellspacing=0 style='border:1px;border-color:#aaaaaa;border-style:solid'>";
            for (int i = 0; i < list.Count; i++)
            {
                jsonParseTemplate temp = list[i] as jsonParseTemplate;
                if (temp.KEY == "PropertyName" && temp.VALUE[0] != '_')
                {
                    html += "<tr>" + "<td>" + temp.VALUE + "</td>" + "</tr>" + "\n";
                }
                else if (temp.KEY == "PropertyName" && temp.VALUE[0] == '_')
                {
                    html += "<tr>" + "<td>" + temp.VALUE + "</td>";
                }
                else if (temp.KEY != "PropertyName")
                {
                    html += "<td>" + temp.VALUE + "</td>" + "</tr>" + "\n";
                }
            }
            html += "</tabel>";
            return html;
        }
    }
}
