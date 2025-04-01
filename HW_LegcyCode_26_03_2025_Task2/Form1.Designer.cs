namespace HW_LegcyCode_26_03_2025_Task2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchTitleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EnterTitleTextBox = new System.Windows.Forms.TextBox();
            this.SelectComboBox = new System.Windows.Forms.ComboBox();
            this.executeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchTitleTextBox
            // 
            this.SearchTitleTextBox.Location = new System.Drawing.Point(230, 35);
            this.SearchTitleTextBox.Name = "SearchTitleTextBox";
            this.SearchTitleTextBox.Size = new System.Drawing.Size(232, 22);
            this.SearchTitleTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите искомое окно";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите новое название окна";
            // 
            // EnterTitleTextBox
            // 
            this.EnterTitleTextBox.Location = new System.Drawing.Point(230, 68);
            this.EnterTitleTextBox.Name = "EnterTitleTextBox";
            this.EnterTitleTextBox.Size = new System.Drawing.Size(232, 22);
            this.EnterTitleTextBox.TabIndex = 2;
            // 
            // SelectComboBox
            // 
            this.SelectComboBox.FormattingEnabled = true;
            this.SelectComboBox.Location = new System.Drawing.Point(230, 97);
            this.SelectComboBox.Name = "SelectComboBox";
            this.SelectComboBox.Size = new System.Drawing.Size(232, 24);
            this.SelectComboBox.TabIndex = 4;
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(230, 141);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(232, 23);
            this.executeButton.TabIndex = 5;
            this.executeButton.Text = "Выполнить";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.SelectComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EnterTitleTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchTitleTextBox);
            this.Name = "Form1";
            this.Text = "FormTest";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchTitleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EnterTitleTextBox;
        private System.Windows.Forms.ComboBox SelectComboBox;
        private System.Windows.Forms.Button executeButton;
    }
}

