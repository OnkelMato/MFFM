namespace Mffm.Samples.Ui.Main
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            MenuFileClose = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            MenuEditPerson = new ToolStripMenuItem();
            LogMessages = new Label();
            SendLogMessage = new Button();
            LogMessageToSend = new TextBox();
            MenuEditProtocol = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1203, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuFileClose });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "&File";
            // 
            // MenuFileClose
            // 
            MenuFileClose.Name = "MenuFileClose";
            MenuFileClose.Size = new Size(224, 26);
            MenuFileClose.Text = "&Close";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuEditPerson, MenuEditProtocol });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            // 
            // MenuEditPerson
            // 
            MenuEditPerson.Name = "MenuEditPerson";
            MenuEditPerson.Size = new Size(224, 26);
            MenuEditPerson.Text = "Person";
            // 
            // LogMessages
            // 
            LogMessages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            LogMessages.Location = new Point(872, 28);
            LogMessages.Name = "LogMessages";
            LogMessages.Size = new Size(319, 659);
            LogMessages.TabIndex = 1;
            LogMessages.Text = "label1";
            // 
            // SendLogMessage
            // 
            SendLogMessage.Location = new Point(405, 48);
            SendLogMessage.Name = "SendLogMessage";
            SendLogMessage.Size = new Size(94, 29);
            SendLogMessage.TabIndex = 2;
            SendLogMessage.Text = "Send Message";
            SendLogMessage.UseVisualStyleBackColor = true;
            // 
            // LogMessageToSend
            // 
            LogMessageToSend.Location = new Point(18, 50);
            LogMessageToSend.Name = "LogMessageToSend";
            LogMessageToSend.Size = new Size(381, 27);
            LogMessageToSend.TabIndex = 3;
            // 
            // MenuEditProtocol
            // 
            MenuEditProtocol.Name = "MenuEditProtocol";
            MenuEditProtocol.Size = new Size(224, 26);
            MenuEditProtocol.Text = "Protocol";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1203, 696);
            Controls.Add(LogMessageToSend);
            Controls.Add(SendLogMessage);
            Controls.Add(LogMessages);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "MFFM Sample Application";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem MenuFileClose;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem MenuEditPerson;
        private Label LogMessages;
        private Button SendLogMessage;
        private TextBox LogMessageToSend;
        private ToolStripMenuItem MenuEditProtocol;
    }
}
