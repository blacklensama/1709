namespace DemoApp
{
    partial class frmDatalinkProp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.targetKeyNameTxt = new System.Windows.Forms.TextBox();
            this.targetValueTxt = new System.Windows.Forms.TextBox();
            this.linkKeyNameTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(348, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // targetKeyNameTxt
            // 
            this.targetKeyNameTxt.Location = new System.Drawing.Point(109, 13);
            this.targetKeyNameTxt.Name = "targetKeyNameTxt";
            this.targetKeyNameTxt.Size = new System.Drawing.Size(133, 21);
            this.targetKeyNameTxt.TabIndex = 1;
            // 
            // targetValueTxt
            // 
            this.targetValueTxt.Location = new System.Drawing.Point(109, 52);
            this.targetValueTxt.Name = "targetValueTxt";
            this.targetValueTxt.Size = new System.Drawing.Size(133, 21);
            this.targetValueTxt.TabIndex = 1;
            // 
            // linkKeyNameTxt
            // 
            this.linkKeyNameTxt.Location = new System.Drawing.Point(109, 93);
            this.linkKeyNameTxt.Name = "linkKeyNameTxt";
            this.linkKeyNameTxt.Size = new System.Drawing.Size(133, 21);
            this.linkKeyNameTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "连接项名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "连接项类型取值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "取值项名称";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 33);
            this.button2.TabIndex = 0;
            this.button2.Text = "查询已有链接定义";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(348, 96);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 33);
            this.button3.TabIndex = 0;
            this.button3.Text = "存储当前链接定义";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // frmDatalinkProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 148);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkKeyNameTxt);
            this.Controls.Add(this.targetValueTxt);
            this.Controls.Add(this.targetKeyNameTxt);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDatalinkProp";
            this.Text = "数据链接定义对话框";
            this.Load += new System.EventHandler(this.datalinkDefineDia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.TextBox targetKeyNameTxt;
        public System.Windows.Forms.TextBox targetValueTxt;
        public System.Windows.Forms.TextBox linkKeyNameTxt;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}