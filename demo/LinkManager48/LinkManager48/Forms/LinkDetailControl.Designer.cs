namespace LinkManager48.Forms
{
    partial class LinkDetailControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.LinkTitle = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LinkUrl = new System.Windows.Forms.TextBox();
            this.LinkCategory = new System.Windows.Forms.ComboBox();
            this.SaveCommand = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // LinkTitle
            // 
            this.LinkTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinkTitle.Location = new System.Drawing.Point(118, 3);
            this.LinkTitle.Name = "LinkTitle";
            this.LinkTitle.Size = new System.Drawing.Size(436, 22);
            this.LinkTitle.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.80378F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.19622F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LinkTitle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LinkUrl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LinkCategory, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.SaveCommand, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(557, 121);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Link";
            // 
            // LinkUrl
            // 
            this.LinkUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinkUrl.Location = new System.Drawing.Point(118, 33);
            this.LinkUrl.Name = "LinkUrl";
            this.LinkUrl.Size = new System.Drawing.Size(436, 22);
            this.LinkUrl.TabIndex = 2;
            // 
            // LinkCategory
            // 
            this.LinkCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinkCategory.FormattingEnabled = true;
            this.LinkCategory.Location = new System.Drawing.Point(118, 63);
            this.LinkCategory.Name = "LinkCategory";
            this.LinkCategory.Size = new System.Drawing.Size(436, 24);
            this.LinkCategory.TabIndex = 3;
            // 
            // SaveCommand
            // 
            this.SaveCommand.Location = new System.Drawing.Point(118, 93);
            this.SaveCommand.Name = "SaveCommand";
            this.SaveCommand.Size = new System.Drawing.Size(75, 23);
            this.SaveCommand.TabIndex = 6;
            this.SaveCommand.Text = "&Save";
            this.SaveCommand.UseVisualStyleBackColor = true;
            // 
            // LinkDetailControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LinkDetailControl";
            this.Size = new System.Drawing.Size(557, 121);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LinkTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox LinkUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox LinkCategory;
        private System.Windows.Forms.Button SaveCommand;
    }
}
