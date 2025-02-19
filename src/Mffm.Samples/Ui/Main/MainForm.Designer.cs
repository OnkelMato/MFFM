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
            MenuEditProtocol = new ToolStripMenuItem();
            SendLogMessageMenu = new ToolStripMenuItem();
            LogMessages = new Label();
            SendLogMessage = new Button();
            LogMessageToSend = new TextBox();
            People = new ListBox();
            PeopleSelected = new TextBox();
            sendMessagesGroup = new GroupBox();
            statusStrip1 = new StatusStrip();
            LastLogMessage = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            sendMessagesGroup.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1053, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuFileClose });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // MenuFileClose
            // 
            MenuFileClose.Name = "MenuFileClose";
            MenuFileClose.Size = new Size(103, 22);
            MenuFileClose.Text = "&Close";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MenuEditPerson, MenuEditProtocol, SendLogMessageMenu });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "Edit";
            // 
            // MenuEditPerson
            // 
            MenuEditPerson.Name = "MenuEditPerson";
            MenuEditPerson.Size = new Size(221, 22);
            MenuEditPerson.Text = "Person";
            // 
            // MenuEditProtocol
            // 
            MenuEditProtocol.Name = "MenuEditProtocol";
            MenuEditProtocol.Size = new Size(221, 22);
            MenuEditProtocol.Text = "Protocol";
            // 
            // SendLogMessageMenu
            // 
            SendLogMessageMenu.Name = "SendLogMessageMenu";
            SendLogMessageMenu.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            SendLogMessageMenu.Size = new Size(221, 22);
            SendLogMessageMenu.Text = "Send Message";
            // 
            // LogMessages
            // 
            LogMessages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LogMessages.Location = new Point(5, 98);
            LogMessages.Name = "LogMessages";
            LogMessages.Size = new Size(359, 232);
            LogMessages.TabIndex = 1;
            LogMessages.Text = "label1";
            // 
            // SendLogMessage
            // 
            SendLogMessage.Location = new Point(5, 62);
            SendLogMessage.Margin = new Padding(3, 2, 3, 2);
            SendLogMessage.Name = "SendLogMessage";
            SendLogMessage.Size = new Size(82, 22);
            SendLogMessage.TabIndex = 2;
            SendLogMessage.Text = "Send Message";
            SendLogMessage.UseVisualStyleBackColor = true;
            // 
            // LogMessageToSend
            // 
            LogMessageToSend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LogMessageToSend.Location = new Point(5, 38);
            LogMessageToSend.Margin = new Padding(3, 2, 3, 2);
            LogMessageToSend.Name = "LogMessageToSend";
            LogMessageToSend.Size = new Size(359, 23);
            LogMessageToSend.TabIndex = 3;
            // 
            // People
            // 
            People.FormattingEnabled = true;
            People.Location = new Point(16, 84);
            People.Margin = new Padding(3, 2, 3, 2);
            People.Name = "People";
            People.Size = new Size(334, 349);
            People.TabIndex = 4;
            // 
            // PeopleSelected
            // 
            PeopleSelected.Location = new Point(16, 436);
            PeopleSelected.Margin = new Padding(3, 2, 3, 2);
            PeopleSelected.Name = "PeopleSelected";
            PeopleSelected.Size = new Size(334, 23);
            PeopleSelected.TabIndex = 5;
            // 
            // sendMessagesGroup
            // 
            sendMessagesGroup.Controls.Add(LogMessageToSend);
            sendMessagesGroup.Controls.Add(SendLogMessage);
            sendMessagesGroup.Controls.Add(LogMessages);
            sendMessagesGroup.Location = new Point(371, 84);
            sendMessagesGroup.Margin = new Padding(3, 2, 3, 2);
            sendMessagesGroup.Name = "sendMessagesGroup";
            sendMessagesGroup.Padding = new Padding(3, 2, 3, 2);
            sendMessagesGroup.Size = new Size(369, 332);
            sendMessagesGroup.TabIndex = 6;
            sendMessagesGroup.TabStop = false;
            sendMessagesGroup.Text = "Send Log Messages";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { LastLogMessage });
            statusStrip1.Location = new Point(0, 500);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 12, 0);
            statusStrip1.Size = new Size(1053, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";
            // 
            // LastLogMessage
            // 
            LastLogMessage.Name = "LastLogMessage";
            LastLogMessage.Size = new Size(94, 17);
            LastLogMessage.Text = "LastLogMessage";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1053, 522);
            Controls.Add(statusStrip1);
            Controls.Add(sendMessagesGroup);
            Controls.Add(PeopleSelected);
            Controls.Add(People);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            sendMessagesGroup.ResumeLayout(false);
            sendMessagesGroup.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
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
        private ListBox People;
        private TextBox PeopleSelected;
        private GroupBox sendMessagesGroup;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel LastLogMessage;
        private ToolStripMenuItem SendLogMessageMenu;
    }
}
