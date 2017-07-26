namespace Clipboard_And_Snippet_Manager
{
    partial class ItemForm
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
            this.bodyTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.placeholderCheckBox = new System.Windows.Forms.CheckBox();
            this.helperComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bodyTextBox
            // 
            this.bodyTextBox.Location = new System.Drawing.Point(12, 62);
            this.bodyTextBox.Multiline = true;
            this.bodyTextBox.Name = "bodyTextBox";
            this.bodyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.bodyTextBox.Size = new System.Drawing.Size(544, 292);
            this.bodyTextBox.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(400, 360);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "追加";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(481, 360);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "キャンセル";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // modeComboBox
            // 
            this.modeComboBox.FormattingEnabled = true;
            this.modeComboBox.Location = new System.Drawing.Point(435, 5);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(121, 23);
            this.modeComboBox.TabIndex = 2;
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.modeComboBox_SelectedIndexChanged);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(55, 6);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(269, 22);
            this.nameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "名前:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(359, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "実行モード:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "登録文:";
            // 
            // placeholderCheckBox
            // 
            this.placeholderCheckBox.AutoSize = true;
            this.placeholderCheckBox.Location = new System.Drawing.Point(370, 42);
            this.placeholderCheckBox.Name = "placeholderCheckBox";
            this.placeholderCheckBox.Size = new System.Drawing.Size(186, 19);
            this.placeholderCheckBox.TabIndex = 3;
            this.placeholderCheckBox.Text = "プレースホルダを有効にする";
            this.placeholderCheckBox.UseVisualStyleBackColor = true;
            // 
            // helperComboBox
            // 
            this.helperComboBox.FormattingEnabled = true;
            this.helperComboBox.Location = new System.Drawing.Point(168, 361);
            this.helperComboBox.Name = "helperComboBox";
            this.helperComboBox.Size = new System.Drawing.Size(121, 23);
            this.helperComboBox.TabIndex = 99;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 364);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "プレースホルダ入力補助:";
            // 
            // ItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 395);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.helperComboBox);
            this.Controls.Add(this.placeholderCheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.bodyTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ItemForm";
            this.Text = "スニペット登録";
            this.Load += new System.EventHandler(this.ItemForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bodyTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox placeholderCheckBox;
        private System.Windows.Forms.ComboBox helperComboBox;
        private System.Windows.Forms.Label label4;
    }
}