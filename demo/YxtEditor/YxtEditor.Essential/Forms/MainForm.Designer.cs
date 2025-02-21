namespace YxtEditor.Essential
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            NewDocument = new ToolStripMenuItem();
            OpenDocument = new ToolStripMenuItem();
            SaveDocument = new ToolStripMenuItem();
            SaveAsDocument = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            CloseApplication = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            LastLogEvent = new ToolStripStatusLabel();
            Contents = new TextBox();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { NewDocument, OpenDocument, SaveDocument, SaveAsDocument, toolStripMenuItem1, CloseApplication });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // NewDocument
            // 
            NewDocument.Name = "NewDocument";
            NewDocument.Size = new Size(152, 26);
            NewDocument.Text = "New";
            // 
            // OpenDocument
            // 
            OpenDocument.Name = "OpenDocument";
            OpenDocument.Size = new Size(152, 26);
            OpenDocument.Text = "Open...";
            // 
            // SaveDocument
            // 
            SaveDocument.Name = "SaveDocument";
            SaveDocument.Size = new Size(152, 26);
            SaveDocument.Text = "Save";
            // 
            // SaveAsDocument
            // 
            SaveAsDocument.Name = "SaveAsDocument";
            SaveAsDocument.Size = new Size(152, 26);
            SaveAsDocument.Text = "Save As...";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(149, 6);
            // 
            // CloseApplication
            // 
            CloseApplication.Name = "CloseApplication";
            CloseApplication.Size = new Size(152, 26);
            CloseApplication.Text = "Quit";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { LastLogEvent });
            statusStrip.Location = new Point(0, 424);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(800, 26);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // LastLogEvent
            // 
            LastLogEvent.Name = "LastLogEvent";
            LastLogEvent.Size = new Size(18, 20);
            LastLogEvent.Text = "...";
            // 
            // Contents
            // 
            Contents.Dock = DockStyle.Fill;
            Contents.Location = new Point(0, 28);
            Contents.Multiline = true;
            Contents.Name = "Contents";
            Contents.Size = new Size(800, 396);
            Contents.TabIndex = 2;
            Contents.WordWrap = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Contents);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "MainForm";
            Text = "Form1";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem NewDocument;
        private ToolStripMenuItem OpenDocument;
        private ToolStripMenuItem SaveDocument;
        private ToolStripMenuItem SaveAsDocument;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem CloseApplication;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel LastLogEvent;
        private TextBox Contents;
    }
}
