namespace ZonyLrcTools.UI
{
    partial class UI_PluginSelect
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
            this.listBox_LrcSourcePluginsList = new System.Windows.Forms.ListBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox_LrcSourcePluginsList
            // 
            this.listBox_LrcSourcePluginsList.FormattingEnabled = true;
            this.listBox_LrcSourcePluginsList.ItemHeight = 12;
            this.listBox_LrcSourcePluginsList.Location = new System.Drawing.Point(12, 12);
            this.listBox_LrcSourcePluginsList.Name = "listBox_LrcSourcePluginsList";
            this.listBox_LrcSourcePluginsList.Size = new System.Drawing.Size(169, 148);
            this.listBox_LrcSourcePluginsList.TabIndex = 0;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(57, 166);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // UI_PluginSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 198);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.listBox_LrcSourcePluginsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UI_PluginSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下载插件选择";
            this.Load += new System.EventHandler(this.UI_PluginSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_LrcSourcePluginsList;
        private System.Windows.Forms.Button button_OK;
    }
}