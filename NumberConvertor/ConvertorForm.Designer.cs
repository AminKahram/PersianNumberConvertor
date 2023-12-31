namespace NumberConvertor
{
    partial class ConvertorForm
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
            label = new Label();
            Number = new TextBox();
            Result = new RichTextBox();
            Converter = new Button();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(608, 83);
            label.Name = "label";
            label.Size = new Size(34, 20);
            label.TabIndex = 0;
            label.Text = "عدد";
            // 
            // Number
            // 
            Number.Location = new Point(187, 76);
            Number.Name = "Number";
            Number.Size = new Size(378, 27);
            Number.TabIndex = 1;
            Number.KeyPress += Number_KeyPress;
            // 
            // Result
            // 
            Result.Dock = DockStyle.Bottom;
            Result.Location = new Point(0, 215);
            Result.Name = "Result";
            Result.ReadOnly = true;
            Result.RightToLeft = RightToLeft.Yes;
            Result.Size = new Size(800, 236);
            Result.TabIndex = 2;
            Result.Text = "";
            // 
            // Converter
            // 
            Converter.Location = new Point(341, 139);
            Converter.Name = "Converter";
            Converter.Size = new Size(94, 29);
            Converter.TabIndex = 3;
            Converter.Text = "تبدیل";
            Converter.UseVisualStyleBackColor = true;
            Converter.Click += Converter_Click;
            // 
            // ConvertorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(Converter);
            Controls.Add(Result);
            Controls.Add(Number);
            Controls.Add(label);
            Name = "ConvertorForm";
            Text = "مبدل";
            Load += ConvertorForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private TextBox Number;
        private RichTextBox Result;
        private Button Converter;
    }
}