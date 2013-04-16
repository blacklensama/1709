namespace DemoApp
{
    partial class frmAddMapType
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
            this.mapTypePic = new System.Windows.Forms.TextBox();
            this.mapTypeName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mapTypeCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mapTypeDesc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "类型名称：";
            // 
            // mapTypePic
            // 
            this.mapTypePic.Location = new System.Drawing.Point(136, 73);
            this.mapTypePic.Name = "mapTypePic";
            this.mapTypePic.Size = new System.Drawing.Size(100, 21);
            this.mapTypePic.TabIndex = 2;
            // 
            // mapTypeName
            // 
            this.mapTypeName.Location = new System.Drawing.Point(136, 24);
            this.mapTypeName.Name = "mapTypeName";
            this.mapTypeName.Size = new System.Drawing.Size(100, 21);
            this.mapTypeName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "类型图片：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "类型图层代码：";
            // 
            // mapTypeCode
            // 
            this.mapTypeCode.Location = new System.Drawing.Point(136, 122);
            this.mapTypeCode.Name = "mapTypeCode";
            this.mapTypeCode.Size = new System.Drawing.Size(100, 21);
            this.mapTypeCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "类型描述：";
            // 
            // mapTypeDesc
            // 
            this.mapTypeDesc.Location = new System.Drawing.Point(136, 172);
            this.mapTypeDesc.Name = "mapTypeDesc";
            this.mapTypeDesc.Size = new System.Drawing.Size(100, 21);
            this.mapTypeDesc.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(147, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(38, 221);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "确定";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mapTypeDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mapTypeCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mapTypeName);
            this.Controls.Add(this.mapTypePic);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "添加地图类型";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox mapTypePic;
        public System.Windows.Forms.TextBox mapTypeName;
        public System.Windows.Forms.TextBox mapTypeCode;
        public System.Windows.Forms.TextBox mapTypeDesc;
    }
}