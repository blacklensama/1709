using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace DemoApp.eq_controls
{
    public  class templateBase
    {
        public string name = "";
        public string description = "";
        public string ID ="temp_" +  new Random().Next().ToString();
        public DemoApp.frmHTMLeditor pform = null;
        public templateBase(DemoApp.frmHTMLeditor p)
        {
            pform = p;
            initMe();
        }
        public void deleteDB()
        {
            if (pform == null)
            {
                return;
            }
            dbTemplateForm dbt = new dbTemplateForm();
            dbt.ShowDialog();
            if (dbt.sel_template != null)
            {

            }
        }
        public void loadFromDB()
        {
            if (pform == null)
                return;

            dbTemplateForm dbt = new dbTemplateForm();
            dbt.ShowDialog();
            if (dbt.sel_template != null)
            {
                this.name = dbt.sel_template[0];
                string html = dbt.sel_template[1];
                this.description = dbt.sel_template[2];


                IfacesEnumsStructsClasses.IHTMLDocument2  doc = pform.cEXWB1.GetActiveDocument();

                mshtml.HTMLDocument docc = (mshtml.HTMLDocument)doc;

               // Encoding ec = Encoding.GetEncoding("gb2312");
               // html = ec.GetString(System.Text.Encoding.Default.GetBytes(html));

                 

                string tempfilepath = Application.StartupPath;
                if (!tempfilepath.EndsWith("\\"))
                    tempfilepath = tempfilepath + "\\";

                tempfilepath = tempfilepath + "tempFile.html";

                System.IO.File.WriteAllText(tempfilepath, html, Encoding.GetEncoding("gb2312"));
              ///  docc.documentElement.innerHTML = html;
              //  docc.body.outerHTML = "<body><label>a</label></body>";
                this.setDocNameDesp();
                pform.cEXWB1.Navigate(tempfilepath); 
            }
        }
        public void initMe()
        {
            templateForm tf = new templateForm();
            tf.ShowDialog() ;

            if (tf.type == 0)
            {
                name = tf.tname;
                description = tf.tdescription;
                setDocNameDesp();
                loadDefault();
                pform.Text = "模版:" + name; 
                //pform.cEXWB1.DocumentComplete += new csExWB.DocumentCompleteEventHandler(DocumentComplete);
            }
            if (tf.type == 1)
            {

                if (pform != null)
                {
                    string path = tf.connectString;
                    loadfromFile(path);
                    pform.Text = "模版:" + name; 
                    
                }
                else System.Windows.Forms.MessageBox.Show("无法读取模版文档");
            }
            if (tf.type == 2)
            {

                if (pform != null)
                {
                    loadFromDB();
                    pform.Text = "模版:" + name; 
                }
                else System.Windows.Forms.MessageBox.Show("无法读取模版文档");
            }
        }
        public void defaultDocumentComplete(object sender, csExWB.DocumentCompleteEventArgs e)
        {
            setDocNameDesp();
        }
        public void DocumentComplete(object sender, csExWB.DocumentCompleteEventArgs e)
        {
            getDocNameDesp();
        }
        public void saveAsFile(string path)
        {

           /* SaveForm save = new SaveForm();
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }*/
            templateCreateForm tcf = new templateCreateForm();

            IfacesEnumsStructsClasses.IHTMLDocument2 doc2 = pform.cEXWB1.GetActiveDocument();
            string tempname = name;
            int idx = path.LastIndexOf("\\");
            tempname = path.Substring(idx + 1);
           
            idx = tempname.IndexOf(".");
            tempname = tempname.Substring(0, idx);
            name = tempname;

            this.setDocNameDesp();


            string s = "<html>" + doc2.body.outerHTML;
            /*if (save.time.Text == "")
            {
                s = s + "<endtime> template </endtime>";
            }
            else
            {
                s = s + save.time.Text;
            }
            if (save.Expert.Text == "")
            {
                s = s + "<expert> template </expert>";
            }
            else
            {
                s = s + save.Expert.Text;
            }*/
            s = s + "</html>";

            System.IO.File.WriteAllText(path, s, Encoding.GetEncoding("gb2312"));

            idx = path.LastIndexOf("\\");
            
            path = path.Substring(0, idx);
           

            System.IO.Directory.CreateDirectory(path + "\\icon");

            string rootpath = System.Windows.Forms.Application.StartupPath + "\\icon\\";

            string[] files = System.IO.Directory.GetFiles(rootpath);

            for (int i = 0; i < files.Length; i++)
            {
                int sidex = files[i].LastIndexOf("\\");
                string f = files[i].Substring(sidex + 1);
                string fname = rootpath + f;
                string fname2 = path + "\\icon\\" + f;
                if (fname != fname2)
                {
                    System.IO.File.Copy(fname, fname2, true);
                }      
            }
        }
        public void loadDefault()
        {
            string rootpath = System.Windows.Forms.Application.StartupPath + "\\template.html";
            pform.cEXWB1.Navigate(rootpath);
            pform.cEXWB1.DocumentComplete += new csExWB.DocumentCompleteEventHandler(defaultDocumentComplete);

        }

        public void openDefault()
        {
            templateCreateForm cf = new templateCreateForm();
            if (cf.ShowDialog() == DialogResult.OK)
            {
                name = cf.textBox1.Text;
                description = cf.textBox2.Text;
                this.setDocNameDesp();
                loadDefault();
            }
        }
        public void saveToDB()
        {
           /* SaveForm save = new SaveForm();
            if (save.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }*/
            templateCreateForm tcf = new templateCreateForm();
            tcf.textBox1.Text = this.name;
            tcf.textBox2.Text = this.description;
            if (tcf.ShowDialog() != DialogResult.OK)
                return;


            IfacesEnumsStructsClasses.IHTMLDocument2 doc = pform.cEXWB1.GetActiveDocument();
            IfacesEnumsStructsClasses.IHTMLElement bd = (IfacesEnumsStructsClasses.IHTMLElement)doc.body;
            this.name = tcf.textBox1.Text;
            this.description = tcf.textBox2.Text;

            pform.Text = "模版:" + name;
          /*  object o = bd.getAttribute("tname", 1);
            if (o != null && !o.ToString().Equals(""))
                name = o.ToString();
            

            o = bd.getAttribute("tdescription", 1);
            if (o != null && !o.ToString().Equals(""))
                description = o.ToString();
            

            o = bd.getAttribute("ID", 1);
            if (o != null && !o.ToString().Equals(""))
                ID = o.ToString();*/

            this.setDocNameDesp();


            string s =    doc.body.outerHTML  ;

            //Encoding ec = Encoding.GetEncoding("gb2312");
           // s ="<html>"  + ec.GetString(System.Text.Encoding.Default.GetBytes(s)) + "</html>";
            s = "<html>" + s;
            /*if (save.time.Text == "")
            {
                s = s + "<endtime> template </endtime>";
            }
            else
            {
                s = s + save.time.Text;
            }
            if (save.Expert.Text == "")
            {
                s = s + "<expert> template </expert>";
            }else
            {
                s = s + save.Expert.Text;
            }*/
            /*foreach (var key in s)
            {

            }*/
            s = s +"</html>";

            if (dbTools.dbTool.saveTemplate(name, s, description, ""))
                MessageBox.Show("数据存储成功");
            else MessageBox.Show("数据存储失败");
            
        }
        public void loadfromFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                int idx = path.LastIndexOf("\\");
                this.name = path.Substring(idx + 1);
                description = name;
                pform.cEXWB1.Navigate(path);
                pform.Text = "模版:" + name;

               
            }
            else
            {

            }
        }
        public void setTemplate()
        {
            templateCreateForm tcf = new templateCreateForm();
            tcf.textBox1.Text = this.name;
            tcf.textBox2.Text = this.description;
            if (tcf.ShowDialog() != DialogResult.OK)
                return;


            
            this.name = tcf.textBox1.Text;
            this.description = tcf.textBox2.Text;

           

            this.setDocNameDesp();
        }
        public void setDocNameDesp()
        {
            if (pform == null)
                return;
            IfacesEnumsStructsClasses.IHTMLDocument2 doc = pform.cEXWB1.GetActiveDocument();
            IfacesEnumsStructsClasses.IHTMLElement bd = (IfacesEnumsStructsClasses.IHTMLElement)doc.body;
            bd.setAttribute("tname", name, 1);
            bd.setAttribute("tdescription", description, 1);
            bd.setAttribute("ID", ID, 1);
            pform.Text = "模版:" + name;
        }
        public void getDocNameDesp()
        {
            if (pform == null)
                return;
            IfacesEnumsStructsClasses.IHTMLDocument2 doc = pform.cEXWB1.GetActiveDocument();
            IfacesEnumsStructsClasses.IHTMLElement bd = (IfacesEnumsStructsClasses.IHTMLElement)doc.body;
            object o = bd.getAttribute("tname", 1);
            if (o != null && ! o.ToString().Equals(""))
                name = o.ToString();
            else bd.setAttribute("tname", name, 1);

             o = bd.getAttribute("tdescription", 1);
             if (o != null && !o.ToString().Equals(""))
                description = o.ToString();
            else bd.setAttribute("tdescription", description, 1);

            o = bd.getAttribute("ID", 1);
            if (o != null && !o.ToString().Equals(""))
                ID = o.ToString();
            else bd.setAttribute("ID" , ID , 1);

           
        }
    }
}
