namespace FastSearch
{
    partial class MainForm
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
            this.searchBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dropDownPan = new System.Windows.Forms.Panel();
            this.dropDownBtn = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // searchBtn
            // 
            this.searchBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBtn.Location = new System.Drawing.Point(657, 12);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(23, 23);
            this.searchBtn.TabIndex = 0;
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(386, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(236, 20);
            this.textBox1.TabIndex = 1;
            // 
            // dropDownPan
            // 
            this.dropDownPan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDownPan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dropDownPan.Location = new System.Drawing.Point(386, 34);
            this.dropDownPan.Name = "dropDownPan";
            this.dropDownPan.Size = new System.Drawing.Size(265, 126);
            this.dropDownPan.TabIndex = 2;
            this.dropDownPan.Visible = false;
            // 
            // dropDownBtn
            // 
            this.dropDownBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDownBtn.Image = global::FastSearch.Properties.Resources.expand;
            this.dropDownBtn.Location = new System.Drawing.Point(628, 12);
            this.dropDownBtn.Name = "dropDownBtn";
            this.dropDownBtn.Size = new System.Drawing.Size(23, 23);
            this.dropDownBtn.TabIndex = 3;
            this.dropDownBtn.UseVisualStyleBackColor = true;
            this.dropDownBtn.Click += new System.EventHandler(this.dropDownBtn_Click);
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(12, 41);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(668, 363);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 416);
            this.Controls.Add(this.dropDownPan);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.dropDownBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.searchBtn);
            this.Name = "MainForm";
            this.Text = "FastSearch";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel dropDownPan;
        private System.Windows.Forms.Button dropDownBtn;
        private System.Windows.Forms.ListView listView;
    }
}

