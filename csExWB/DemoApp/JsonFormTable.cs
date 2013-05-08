using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class JsonFormTable : Form
    {
        public string tableNameString;
        public string propertyNameString;
        public string html;
        public JsonFormTable()
        {
            InitializeComponent();
        }

        private void JsonFormTable_Load(object sender, EventArgs e)
        {
            foreach (var key in wordDictionary.dict.Keys)
            {
                tableName.Items.Add(mathDictionary.dict[key.ToString()]);
            }
        }

        private void tableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var key in wordDictionary.dict.Keys)
            {
                if (mathDictionary.dict[key.ToString()] == tableName.Text)
                {
                    tableNameString = key.ToString();
                    break;
                }
            }
            index.Items.Clear();
            property.Items.Clear();
            property.Text = "";
            int flag = 0;
            try
            {
                foreach (Dictionary<string, int> key in wordDictionary.dict[tableNameString])
                {
                    foreach (var t in key.Keys)
                    {
                        if (flag == 0)
                        {
                            flag++; continue;
                        }
                        property.Items.Add(mathDictionary.dict[t.ToString()]);
                        index.Items.Add(mathDictionary.dict[t.ToString()]);
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
            try
            {
                foreach (Dictionary<string, int> key in wordDictionary.dict[tableNameString])
                {
                    foreach (var t in key.Keys)
                    {
                        if (mathDictionary.dict[t.ToString()] == property.Text)
                        {
                            propertyNameString = t.ToString();
                            break;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {

            }

        }

        private void property_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in index.CheckedItems)
            {
                html = html + item.ToString() + " ";
            } 
        }
    }
}
