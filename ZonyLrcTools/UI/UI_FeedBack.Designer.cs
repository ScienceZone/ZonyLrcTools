namespace ZonyLrcTools.UI
{
    partial class UI_FeedBack
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_SurveyContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_SurveyType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Contact = new System.Windows.Forms.TextBox();
            this.button_Submit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "反馈内容:";
            // 
            // textBox_SurveyContent
            // 
            this.textBox_SurveyContent.Location = new System.Drawing.Point(12, 24);
            this.textBox_SurveyContent.Multiline = true;
            this.textBox_SurveyContent.Name = "textBox_SurveyContent";
            this.textBox_SurveyContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_SurveyContent.Size = new System.Drawing.Size(536, 198);
            this.textBox_SurveyContent.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 231);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "反馈类型:";
            // 
            // comboBox_SurveyType
            // 
            this.comboBox_SurveyType.FormattingEnabled = true;
            this.comboBox_SurveyType.Items.AddRange(new object[] {
            "Bug信息反馈",
            "改进建议"});
            this.comboBox_SurveyType.Location = new System.Drawing.Point(75, 228);
            this.comboBox_SurveyType.Name = "comboBox_SurveyType";
            this.comboBox_SurveyType.Size = new System.Drawing.Size(114, 20);
            this.comboBox_SurveyType.TabIndex = 3;
            this.comboBox_SurveyType.Text = "Bug信息反馈";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "联系方式(QQ/Email):";
            // 
            // textBox_Contact
            // 
            this.textBox_Contact.Location = new System.Drawing.Point(135, 254);
            this.textBox_Contact.Name = "textBox_Contact";
            this.textBox_Contact.Size = new System.Drawing.Size(201, 21);
            this.textBox_Contact.TabIndex = 4;
            // 
            // button_Submit
            // 
            this.button_Submit.Location = new System.Drawing.Point(457, 231);
            this.button_Submit.Name = "button_Submit";
            this.button_Submit.Size = new System.Drawing.Size(91, 47);
            this.button_Submit.TabIndex = 5;
            this.button_Submit.Text = "提交反馈";
            this.button_Submit.UseVisualStyleBackColor = true;
            this.button_Submit.Click += new System.EventHandler(this.button_Submit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(448, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "QQ群:";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(489, 287);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(59, 12);
            this.linkLabel3.TabIndex = 6;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "337656932";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // UI_FeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 308);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.button_Submit);
            this.Controls.Add(this.textBox_Contact);
            this.Controls.Add(this.comboBox_SurveyType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_SurveyContent);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UI_FeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "反馈信息";
            this.Load += new System.EventHandler(this.UI_FeedBack_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_SurveyContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_SurveyType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Contact;
        private System.Windows.Forms.Button button_Submit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}