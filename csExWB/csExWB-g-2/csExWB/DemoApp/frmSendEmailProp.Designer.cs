namespace DemoApp
{
    partial class frmSendEmailProp
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
            this.concel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chaosongTxt = new System.Windows.Forms.TextBox();
            this.themeTxt = new System.Windows.Forms.TextBox();
            this.toNameTxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // concel
            // 
            this.concel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.concel.Location = new System.Drawing.Point(160, 179);
            this.concel.Name = "concel";
            this.concel.Size = new System.Drawing.Size(80, 28);
            this.concel.TabIndex = 12;
            this.concel.Text = "取消";
            this.concel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(30, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 28);
            this.button1.TabIndex = 13;
            this.button1.Text = "确认";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // chaosongTxt
            // 
            this.chaosongTxt.Location = new System.Drawing.Point(124, 143);
            this.chaosongTxt.Name = "chaosongTxt";
            this.chaosongTxt.Size = new System.Drawing.Size(133, 21);
            this.chaosongTxt.TabIndex = 10;
            // 
            // themeTxt
            // 
            this.themeTxt.Location = new System.Drawing.Point(124, 97);
            this.themeTxt.Name = "themeTxt";
            this.themeTxt.Size = new System.Drawing.Size(133, 21);
            this.themeTxt.TabIndex = 11;
            // 
            // toNameTxt
            // 
            this.toNameTxt.Location = new System.Drawing.Point(124, 56);
            this.toNameTxt.Name = "toNameTxt";
            this.toNameTxt.Size = new System.Drawing.Size(133, 21);
            this.toNameTxt.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "抄送：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "主题：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "收件人：";
            // 
            // frmSendEmailProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.concel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chaosongTxt);
            this.Controls.Add(this.themeTxt);
            this.Controls.Add(this.toNameTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmSendEmailProp";
            this.Text = "邮件发送设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button concel;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox chaosongTxt;
        public System.Windows.Forms.TextBox themeTxt;
        public System.Windows.Forms.TextBox toNameTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}