namespace DemoApp
{
    partial class JsonForm
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
            this.tablename = new System.Windows.Forms.ComboBox();
            this.property = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.index = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "属性";
            // 
            // tablename
            // 
            this.tablename.FormattingEnabled = true;
            this.tablename.Location = new System.Drawing.Point(123, 33);
            this.tablename.Name = "tablename";
            this.tablename.Size = new System.Drawing.Size(121, 20);
            this.tablename.TabIndex = 2;
            this.tablename.UseWaitCursor = true;
            this.tablename.SelectedIndexChanged += new System.EventHandler(this.tablename_SelectedIndexChanged);
            // 
            // property
            // 
            this.property.FormattingEnabled = true;
            this.property.Location = new System.Drawing.Point(123, 68);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(121, 20);
            this.property.TabIndex = 3;
            this.property.SelectedIndexChanged += new System.EventHandler(this.property_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "选项";
            // 
            // index
            // 
            this.index.FormattingEnabled = true;
            this.index.Location = new System.Drawing.Point(123, 99);
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(121, 20);
            this.index.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(169, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // JsonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 218);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.index);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.property);
            this.Controls.Add(this.tablename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "JsonForm";
            this.Text = "JsonForm";
            this.Load += new System.EventHandler(this.JsonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox tablename;
        public System.Windows.Forms.ComboBox property;
        public System.Windows.Forms.ComboBox index;
    }
}