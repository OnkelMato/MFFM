namespace Mffm.Samples.Ui.Protocol
{
    partial class ProtocolForm
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
            Log = new TextBox();
            CloseWindow = new Button();
            SuspendLayout();
            // 
            // Log
            // 
            Log.Location = new Point(14, 16);
            Log.Margin = new Padding(3, 4, 3, 4);
            Log.Multiline = true;
            Log.Name = "Log";
            Log.Size = new Size(458, 289);
            Log.TabIndex = 1;
            // 
            // CloseWindow
            // 
            CloseWindow.Location = new Point(386, 315);
            CloseWindow.Margin = new Padding(3, 4, 3, 4);
            CloseWindow.Name = "CloseWindow";
            CloseWindow.Size = new Size(86, 31);
            CloseWindow.TabIndex = 2;
            CloseWindow.Text = "button1";
            CloseWindow.UseVisualStyleBackColor = true;
            // 
            // ProtocolForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 363);
            Controls.Add(CloseWindow);
            Controls.Add(Log);
            Margin = new Padding(3, 4, 3, 4);
            Name = "ProtocolForm";
            Text = "ProtocollForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Log;
        private Button CloseWindow;
    }
}