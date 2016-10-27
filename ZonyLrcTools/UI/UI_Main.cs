using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonyLrcTools.Enum;

namespace ZonyLrcTools.UI
{
    public partial class UI_Main : Form
    {
        public UI_Main()
        {
            InitializeComponent();
        }

        private void button_SetWorkDirectory_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog _folderDlg = new FolderBrowserDialog();
            _folderDlg.Description = "请选择程序的工作目录:";
            _folderDlg.ShowDialog();
            if(!string.IsNullOrEmpty(_folderDlg.SelectedPath))
            {
                setBottomStatusText(StatusHeadEnum.NORMAL, "开始扫描目录...");
            }
        }

        private void setBottomStatusText(string head,string content)
        {
            statusLabel_StateText.Text = string.Format("{0}:{1}", head, content);
        }

        private void UI_Main_Load(object sender, EventArgs e)
        {
            setBottomStatusText(StatusHeadEnum.WAIT, "等待操作...");
        }
    }
}
