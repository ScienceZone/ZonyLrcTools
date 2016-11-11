namespace ZonyLrcTools.UI
{
    partial class UI_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Settings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox_IsIgnoreExitsFile = new System.Windows.Forms.CheckBox();
            this.textBox_SearchSuffixs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_LrcOutput = new System.Windows.Forms.ComboBox();
            this.comboBox_Encoding = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox_IsCheckUpdate = new System.Windows.Forms.CheckBox();
            this.textBox_DownLoadThreadNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_SaveSetting = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox_IsIgnoreExitsFile);
            this.tabPage1.Controls.Add(this.textBox_SearchSuffixs);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.comboBox_LrcOutput);
            this.tabPage1.Controls.Add(this.comboBox_Encoding);
            this.tabPage1.Controls.Add(this.label2);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsIgnoreExitsFile
            // 
            resources.ApplyResources(this.checkBox_IsIgnoreExitsFile, "checkBox_IsIgnoreExitsFile");
            this.checkBox_IsIgnoreExitsFile.Name = "checkBox_IsIgnoreExitsFile";
            this.checkBox_IsIgnoreExitsFile.UseVisualStyleBackColor = true;
            // 
            // textBox_SearchSuffixs
            // 
            resources.ApplyResources(this.textBox_SearchSuffixs, "textBox_SearchSuffixs");
            this.textBox_SearchSuffixs.Name = "textBox_SearchSuffixs";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // comboBox_LrcOutput
            // 
            this.comboBox_LrcOutput.FormattingEnabled = true;
            this.comboBox_LrcOutput.Items.AddRange(new object[] {
            resources.GetString("comboBox_LrcOutput.Items"),
            resources.GetString("comboBox_LrcOutput.Items1")});
            resources.ApplyResources(this.comboBox_LrcOutput, "comboBox_LrcOutput");
            this.comboBox_LrcOutput.Name = "comboBox_LrcOutput";
            this.comboBox_LrcOutput.SelectionChangeCommitted += new System.EventHandler(this.comboBox_LrcOutput_SelectionChangeCommitted);
            // 
            // comboBox_Encoding
            // 
            this.comboBox_Encoding.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_Encoding, "comboBox_Encoding");
            this.comboBox_Encoding.Name = "comboBox_Encoding";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox_IsCheckUpdate);
            this.tabPage2.Controls.Add(this.textBox_DownLoadThreadNum);
            this.tabPage2.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_IsCheckUpdate
            // 
            resources.ApplyResources(this.checkBox_IsCheckUpdate, "checkBox_IsCheckUpdate");
            this.checkBox_IsCheckUpdate.Name = "checkBox_IsCheckUpdate";
            this.checkBox_IsCheckUpdate.UseVisualStyleBackColor = true;
            // 
            // textBox_DownLoadThreadNum
            // 
            resources.ApplyResources(this.textBox_DownLoadThreadNum, "textBox_DownLoadThreadNum");
            this.textBox_DownLoadThreadNum.Name = "textBox_DownLoadThreadNum";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button_SaveSetting
            // 
            resources.ApplyResources(this.button_SaveSetting, "button_SaveSetting");
            this.button_SaveSetting.Name = "button_SaveSetting";
            this.button_SaveSetting.UseVisualStyleBackColor = true;
            this.button_SaveSetting.Click += new System.EventHandler(this.button_SaveSetting_Click);
            // 
            // UI_Settings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_SaveSetting);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UI_Settings";
            this.Load += new System.EventHandler(this.UI_Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button_SaveSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_DownLoadThreadNum;
        private System.Windows.Forms.CheckBox checkBox_IsCheckUpdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_Encoding;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_LrcOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_SearchSuffixs;
        private System.Windows.Forms.CheckBox checkBox_IsIgnoreExitsFile;
    }
}