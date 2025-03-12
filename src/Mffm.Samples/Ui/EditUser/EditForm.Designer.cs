namespace Mffm.Samples.Ui.EditUser
{
    partial class EditForm
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
            Close = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            Firstname = new TextBox();
            Lastname = new TextBox();
            Address = new TextBox();
            City = new TextBox();
            Save = new Button();
            SaveAndClose = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Close
            // 
            Close.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Close.DialogResult = DialogResult.Cancel;
            Close.Location = new Point(482, 291);
            Close.Margin = new Padding(3, 4, 3, 4);
            Close.Name = "Close";
            Close.Size = new Size(86, 31);
            Close.TabIndex = 0;
            Close.Text = "Cancel";
            Close.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(Firstname, 1, 0);
            tableLayoutPanel1.Controls.Add(Lastname, 1, 1);
            tableLayoutPanel1.Controls.Add(Address, 1, 2);
            tableLayoutPanel1.Controls.Add(City, 1, 3);
            tableLayoutPanel1.Location = new Point(14, 16);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.3150673F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.6849327F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
            tableLayoutPanel1.Size = new Size(558, 267);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(273, 68);
            label1.TabIndex = 0;
            label1.Text = "Vorname";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(3, 68);
            label2.Name = "label2";
            label2.Size = new Size(273, 70);
            label2.TabIndex = 1;
            label2.Text = "Nachname";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.Location = new Point(3, 138);
            label3.Name = "label3";
            label3.Size = new Size(273, 64);
            label3.TabIndex = 2;
            label3.Text = "Adresse";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.Location = new Point(3, 202);
            label4.Name = "label4";
            label4.Size = new Size(273, 65);
            label4.TabIndex = 3;
            label4.Text = "Ort";
            // 
            // Firstname
            // 
            Firstname.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Firstname.Location = new Point(282, 4);
            Firstname.Margin = new Padding(3, 4, 3, 4);
            Firstname.Name = "Firstname";
            Firstname.Size = new Size(273, 27);
            Firstname.TabIndex = 4;
            // 
            // Lastname
            // 
            Lastname.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Lastname.Location = new Point(282, 72);
            Lastname.Margin = new Padding(3, 4, 3, 4);
            Lastname.Name = "Lastname";
            Lastname.Size = new Size(273, 27);
            Lastname.TabIndex = 5;
            // 
            // Address
            // 
            Address.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Address.Location = new Point(282, 142);
            Address.Margin = new Padding(3, 4, 3, 4);
            Address.Name = "Address";
            Address.Size = new Size(273, 27);
            Address.TabIndex = 6;
            // 
            // City
            // 
            City.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            City.Location = new Point(282, 206);
            City.Margin = new Padding(3, 4, 3, 4);
            City.Name = "City";
            City.Size = new Size(273, 27);
            City.TabIndex = 7;
            // 
            // Save
            // 
            Save.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Save.Location = new Point(298, 291);
            Save.Margin = new Padding(3, 4, 3, 4);
            Save.Name = "Save";
            Save.Size = new Size(86, 31);
            Save.TabIndex = 2;
            Save.Text = "Apply";
            Save.UseVisualStyleBackColor = true;
            // 
            // SaveAndClose
            // 
            SaveAndClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveAndClose.DialogResult = DialogResult.OK;
            SaveAndClose.Location = new Point(390, 291);
            SaveAndClose.Margin = new Padding(3, 4, 3, 4);
            SaveAndClose.Name = "SaveAndClose";
            SaveAndClose.Size = new Size(86, 31);
            SaveAndClose.TabIndex = 3;
            SaveAndClose.Text = "OK";
            SaveAndClose.UseVisualStyleBackColor = true;
            // 
            // EditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 337);
            Controls.Add(SaveAndClose);
            Controls.Add(Save);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(Close);
            Margin = new Padding(3, 4, 3, 4);
            Name = "EditForm";
            Text = "EditForm";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button Close;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox Firstname;
        private TextBox Lastname;
        private TextBox Address;
        private TextBox City;
        private Button Save;
        private Button SaveAndClose;
    }
}