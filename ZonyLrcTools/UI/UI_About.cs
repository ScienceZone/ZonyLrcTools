using System;
using System.Windows.Forms;
using ZonyLrcTools.Untils;
using System.Diagnostics;

namespace ZonyLrcTools.UI
{
    public partial class UI_About : Form
    {
        public UI_About()
        {
            InitializeComponent();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            SettingManager.SetValue.IsAgree = true;
            Close();
        }

        private void button_Disagree_Click(object sender, EventArgs e)
        {
            SettingManager.SetValue.IsAgree = false;
            Environment.Exit(0);
        }

        private void button_Agree_Click(object sender, EventArgs e)
        {
            SettingManager.SetValue.IsAgree = true;
            Close();
        }

        private void UI_About_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.App;
            if (SettingManager.SetValue.IsAgree) button_Agree.Visible = button_Disagree.Visible = false;
            else button_Exit.Visible = false;
        }

        private void UI_About_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingManager.Save();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openExplorer("https://jq.qq.com/?_wv=1027&k=41bejmu");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openExplorer("http://git.oschina.net/jokers/ZonyLrcTools");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openExplorer("http://www.myzony.com");
        }

        /// <summary>
        /// 通过默认浏览器打开指定网页
        /// </summary>
        /// <param name="url">网页的URL</param>
        private void openExplorer(string url)
        {
            Process.Start(url);
        }
    }
}
