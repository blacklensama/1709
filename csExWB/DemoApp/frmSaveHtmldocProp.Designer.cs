namespace DemoApp
{
    partial class frmSaveHtmldocProp
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
            this.fileNameTxt = new System.Windows.Forms.TextBox();
            this.filePathTxt = new System.Windows.Forms.TextBox();
            this.fileDescriptionTxt = new System.Windows.Forms.TextBox();
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
            this.label1.Text = "文件名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "存储路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "模板描述：";
            // 
            // fileNameTxt
            // 
            this.fileNameTxt.Location = new System.Drawing.Point(126, 34);
            this.fileNameTxt.Name = "fileNameTxt";
            this.fileNameTxt.Size = new System.Drawing.Size(133, 21);
            this.fileNameTxt.TabIndex = 4;
            // 
            // filePathTxt
            // 
            this.filePathTxt.Location = new System.Drawing.Point(126, 75);
            this.filePathTxt.Name = "filePathTxt";
            this.filePathTxt.Size = new System.Drawing.Size(133, 21);
            this.filePathTxt.TabIndex = 4;
            // 
            // fileDescriptionTxt
            // 
            this.fileDescriptionTxt.Location = new System.Drawing.Point(126, 121);
            this.fileDescriptionTxt.Name = "fileDescriptionTxt";
            this.fileDescriptionTxt.Size = new System.Drawing.Size(133, 21);
            this.fileDescriptionTxt.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(32, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 28);
            this.button1.TabIndex = 5;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // concel
            // 
            this.concel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.concel.Location = new System.Drawing.Point(162, 157);
            this.concel.Name = "concel";
            this.concel.Size = new System.Drawing.Size(80, 28);
            this.concel.TabIndex = 5;
            this.concel.Text = "取消";
            this.concel.UseVisualStyleBackColor = true;
            // 
            // frmSaveHtmldocProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 210);
            this.Controls.Add(this.concel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fileDescriptionTxt);
            this.Controls.Add(this.filePathTxt);
            this.Controls.Add(this.fileNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSaveHtmldocProp";
            this.Text = "网页发布设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox fileNameTxt;
        public System.Windows.Forms.TextBox filePathTxt;
        public System.Windows.Forms.TextBox fileDescriptionTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button concel;
    }
}