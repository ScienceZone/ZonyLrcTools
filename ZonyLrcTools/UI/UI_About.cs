using System;
using System.Windows.Forms;
using ZonyLrcTools.Untils;

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
    }
}
