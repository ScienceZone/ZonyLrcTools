using System;
using System.Windows.Forms;

namespace LibSingleLyricSearch
{
    public partial class UI_LyricView : Form
    {
        public string LyricText { get; set; }
        public UI_LyricView()
        {
            InitializeComponent();
        }

        private void UI_LyricView_Load(object sender, EventArgs e)
        {
            textBox_Lyric.Text = LyricText.Replace("\n", "\r\n");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
            }
            return true;
        }
    }
}
