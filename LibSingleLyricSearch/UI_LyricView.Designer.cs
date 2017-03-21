namespace LibSingleLyricSearch
{
    partial class UI_LyricView
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
            this.textBox_Lyric = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_Lyric
            // 
            this.textBox_Lyric.Location = new System.Drawing.Point(12, 12);
            this.textBox_Lyric.Multiline = true;
            this.textBox_Lyric.Name = "textBox_Lyric";
            this.textBox_Lyric.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Lyric.Size = new System.Drawing.Size(318, 438);
            this.textBox_Lyric.TabIndex = 0;
            // 
            // UI_LyricView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 462);
            this.Controls.Add(this.textBox_Lyric);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UI_LyricView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "歌词数据";
            this.Load += new System.EventHandler(this.UI_LyricView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Lyric;
    }
}