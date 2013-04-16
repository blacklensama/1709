using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();
        }

        private void testForm_Load(object sender, EventArgs e)
        {
            init();

        }
        private void init()
        {
            cEXWB1.RegisterAsBrowser = true; //using default webbrowser dragdrop
            cEXWB1.NavToBlank();
            //Enter design mode
            this.cEXWB1.SetDesignMode("on");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "";
            cEXWB1.Focus();

         /*   Control c = cEXWB1.GetChildAtPoint(Cursor.Position);
          IfacesEnumsStructsClasses. IHTMLElementCollection collections =   cEXWB1.GetAnchors(true);
          for (int i = 0; i < collections.length; i++)
          {
              
          }*/

           
             
           string  ss = cEXWB1.GetSelectedText(true, true);
           IfacesEnumsStructsClasses.IHTMLElement ele =  cEXWB1.ElementFromPoint2(false, 10, 10);

           
           // cEXWB1.GetChildAtPoint(e.x
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cEXWB1.Focus();
            cEXWB1.testadddiv();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cEXWB1.Focus();
            IfacesEnumsStructsClasses.IHTMLElement targetEleemnt = cEXWB1.GetElementByCussor(false, true);
            if (targetEleemnt!=null)
                cEXWB1.testadddiv2(targetEleemnt);
        }
    }
}
