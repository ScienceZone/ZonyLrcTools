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
            Close();
        }

        private void button_Disagree_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_Agree_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
