namespace Mffm.Samples.Extensions.GeoComponent
{
    partial class GeolocationControl
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
            latitudeTextbox = new TextBox();
            longitudeTextbox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // latitudeTextbox
            // 
            latitudeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            latitudeTextbox.Location = new Point(91, 3);
            latitudeTextbox.Name = "latitudeTextbox";
            latitudeTextbox.Size = new Size(125, 27);
            latitudeTextbox.TabIndex = 0;
            latitudeTextbox.TextChanged += latitudeTextbox_TextChanged;
            // 
            // longitudeTextbox
            // 
            longitudeTextbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            longitudeTextbox.Location = new Point(91, 36);
            longitudeTextbox.Name = "longitudeTextbox";
            longitudeTextbox.Size = new Size(125, 27);
            longitudeTextbox.TabIndex = 1;
            longitudeTextbox.TextChanged += longitudeTextbox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 6);
            label1.Name = "label1";
            label1.Size = new Size(63, 20);
            label1.TabIndex = 2;
            label1.Text = "Latitude";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 39);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Longitude";
            // 
            // GeolocationControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(longitudeTextbox);
            Controls.Add(latitudeTextbox);
            MinimumSize = new Size(224, 71);
            Name = "GeolocationControl";
            Size = new Size(224, 71);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox latitudeTextbox;
        private TextBox longitudeTextbox;
        private Label label1;
        private Label label2;
    }
}
