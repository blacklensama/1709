namespace DemoApp
{
    partial class frmSaveToDBProp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.templateNameTxt = new System.Windows.Forms.TextBox();
            this.templateDBSourceTxt = new System.Windows.Forms.TextBox();
            this.templateDescriptionTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.templateDBUserNameTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.templateDBUserPasswordTxt = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.concel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "模板名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "数据源：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "模板描述：";
            // 
            // templateNameTxt
            // 
            this.templateNameTxt.Location = new System.Drawing.Point(126, 34);
            this.templateNameTxt.Name = "templateNameTxt";
            this.templateNameTxt.Size = new System.Drawing.Size(133, 21);
            this.templateNameTxt.TabIndex = 4;
            // 
            // templateDBSourceTxt
            // 
            this.templateDBSourceTxt.Location = new System.Drawing.Point(126, 75);
            this.templateDBSourceTxt.Name = "templateDBSourceTxt";
            this.templateDBSourceTxt.Size = new System.Drawing.Size(133, 21);
            this.templateDBSourceTxt.TabIndex = 4;
            // 
            // templateDescriptionTxt
            // 
            this.templateDescriptionTxt.Location = new System.Drawing.Point(126, 195);
            this.templateDescriptionTxt.Name = "templateDescriptionTxt";
            this.templateDescriptionTxt.Size = new System.Drawing.Size(133, 21);
            this.templateDescriptionTxt.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "用户名：";
            // 
            // templateDBUserNameTxt
            // 
            this.templateDBUserNameTxt.Location = new System.Drawing.Point(126, 114);
            this.templateDBUserNameTxt.Name = "templateDBUserNameTxt";
            this.templateDBUserNameTxt.Size = new System.Drawing.Size(133, 21);
            this.templateDBUserNameTxt.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "密码：";
            // 
            // templateDBUserPasswordTxt
            // 
            this.templateDBUserPasswordTxt.Location = new System.Drawing.Point(126, 154);
            this.templateDBUserPasswordTxt.Name = "templateDBUserPasswordTxt";
            this.templateDBUserPasswordTxt.Size = new System.Drawing.Size(133, 21);
            this.templateDBUserPasswordTxt.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(32, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // concel
            // 
            this.concel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.concel.Location = new System.Drawing.Point(157, 222);
            this.concel.Name = "concel";
            this.concel.Size = new System.Drawing.Size(76, 28);
            this.concel.TabIndex = 5;
            this.concel.Text = "取消";
            this.concel.UseVisualStyleBackColor = true;
            // 
            // frmSaveToDBProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.concel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.templateDescriptionTxt);
            this.Controls.Add(this.templateDBUserPasswordTxt);
            this.Controls.Add(this.templateDBUserNameTxt);
            this.Controls.Add(this.templateDBSourceTxt);
            this.Controls.Add(this.templateNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSaveToDBProp";
            this.Text = "数据库存储设置";
            this.Load += new System.EventHandler(this.frmSaveToDBProp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox templateNameTxt;
        public System.Windows.Forms.TextBox templateDBSourceTxt;
        public System.Windows.Forms.TextBox templateDescriptionTxt;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox templateDBUserNameTxt;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox templateDBUserPasswordTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button concel;
    }
}