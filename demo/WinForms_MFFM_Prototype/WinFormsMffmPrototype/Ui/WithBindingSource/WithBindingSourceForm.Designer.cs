namespace WinFormsMffmPrototype.Ui.WithBindingSource
{
    partial class WithBindingSourceForm
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
            components = new System.ComponentModel.Container();
            bindingSource1 = new BindingSource(components);
            button1 = new Button();
            extendedButton1 = new ExtendedButton();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(WithBindingSourceFormModel);
            // 
            // button1
            // 
            button1.Location = new Point(37, 47);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // extendedButton1
            // 
            extendedButton1.DataBindings.Add(new Binding("ImageBinding", bindingSource1, "ButtonImage", true));
            extendedButton1.ImageBinding = null;
            extendedButton1.Location = new Point(41, 115);
            extendedButton1.Name = "extendedButton1";
            extendedButton1.Size = new Size(94, 29);
            extendedButton1.TabIndex = 1;
            extendedButton1.Text = "extendedButton1";
            extendedButton1.UseVisualStyleBackColor = true;
            // 
            // WithBindingSourceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(extendedButton1);
            Controls.Add(button1);
            Name = "WithBindingSourceForm";
            Text = "WithBindingSourceForm";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BindingSource bindingSource1;
        private Button button1;
        private ExtendedButton extendedButton1;
    }
}