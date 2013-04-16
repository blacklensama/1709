namespace DemoApp
{
    partial class JsonFormTable
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
            this.name = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tableName = new System.Windows.Forms.ComboBox();
            this.property = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.index = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(21, 28);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(29, 12);
            this.name.TabIndex = 0;
            this.name.Text = "名称";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(185, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableName
            // 
            this.tableName.FormattingEnabled = true;
            this.tableName.Location = new System.Drawing.Point(56, 23);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(121, 20);
            this.tableName.TabIndex = 3;
            this.tableName.SelectedIndexChanged += new System.EventHandler(this.tableName_SelectedIndexChanged);
            // 
            // property
            // 
            this.property.FormattingEnabled = true;
            this.property.Location = new System.Drawing.Point(56, 55);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(121, 20);
            this.property.TabIndex = 5;
            this.property.SelectedIndexChanged += new System.EventHandler(this.property_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "属性";
            // 
            // index
            // 
            this.index.FormattingEnabled = true;
            this.index.Location = new System.Drawing.Point(70, 103);
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(120, 212);
            this.index.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "选项";
            // 
            // JsonFormTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 341);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.index);
            this.Controls.Add(this.property);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.name);
            this.Name = "JsonFormTable";
            this.Text = "JsonFormTable";
            this.Load += new System.EventHandler(this.JsonFormTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox tableName;
        public System.Windows.Forms.ComboBox property;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox index;
        private System.Windows.Forms.Label label1;
    }
}