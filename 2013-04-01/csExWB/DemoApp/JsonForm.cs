using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class JsonForm : Form
    {
        public string tableNameString;
        public string propertyNameString;
        public JsonForm()
        {
            InitializeComponent();
        }

        private void JsonForm_Load(object sender, EventArgs e)
        {
            foreach (var key in wordDictionary.dict.Keys)
            {
                tablename.Items.Add(mathDictionary.dict[key.ToString()]);
            }
           
        }

        private void tablename_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var key in wordDictionary.dict.Keys)
            {
                if (mathDictionary.dict[key.ToString()] == tablename.Text)
                {
                    tableNameString = key.ToString();
                    break;
                }
            }
            property.Items.Clear();
            property.Text = "";
            index.Text = "";
            try
            {
                foreach (Dictionary<string, int> key in wordDictionary.dict[tableNameString])
                {
                    foreach (var t in key.Keys)
                    {
                        property.Items.Add(mathDictionary.dict[t.ToString()]);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
        }

        private void property_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            index.Items.Clear();
            index.Text = "";

            foreach (Dictionary<string, int> key in wordDictionary.dict[tableNameString])
            {
                try
                {
                    int i = key[propertyNameString];
                    if (i == 0)
                    {
                        index.Items.Add(i.ToString());
                    }
                    else
                    {
                        for (int t = 1; t <= i; t++)
                        {
                            index.Items.Add(t.ToString());
                        }
                    }
                }
                catch (System.Exception ex)
                {

                }
            }              
        }
    }
}
