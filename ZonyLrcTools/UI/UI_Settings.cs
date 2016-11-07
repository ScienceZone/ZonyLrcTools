using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonyLrcTools.Untils;

namespace ZonyLrcTools.UI
{
    public partial class UI_Settings : Form
    {
        public UI_Settings()
        {
            InitializeComponent();
        }

        private void button_SaveSetting_Click(object sender, EventArgs e)
        {
            saveSetting();
            Close();
        }

        private void UI_Settings_Load(object sender, EventArgs e)
        {
            loadEncodings();
            laodSetting();
        }

        /// <summary>
        /// 加载电脑支持的编码集
        /// </summary>
        private async void loadEncodings()
        {
            await Task.Run(() => 
            {
                var _encodings = Encoding.GetEncodings();
                foreach (var item in _encodings)
                {
                    comboBox_Encoding.Items.Add(item.Name);
                }
            });
        }
        /// <summary>
        /// 加载设置
        /// </summary>
        private async void laodSetting()
        {
            await Task.Run(() => 
            {
                SettingManager.Load();
                comboBox_Encoding.Text = SettingManager.SetValue.EncodingName;
                textBox_SearchSuffixs.Text = SettingManager.SetValue.FileSuffixs;
                checkBox_IsIgnoreExitsFile.Checked = SettingManager.SetValue.IsIgnoreExitsFile;
                textBox_DownLoadThreadNum.Text = SettingManager.SetValue.DownloadThreadNum.ToString();
                checkBox_IsCheckUpdate.Checked = SettingManager.SetValue.IsCheckUpdate;
                if (SettingManager.SetValue.UserDirectory == string.Empty)
                {
                    comboBox_LrcOutput.SelectedIndex = 0;
                }
                else if (SettingManager.SetValue.UserDirectory.Equals("ID3v2"))
                {
                    comboBox_LrcOutput.SelectedIndex = 2;
                }
                else
                {
                    comboBox_LrcOutput.SelectedIndex = 1;
                    comboBox_LrcOutput.Text = SettingManager.SetValue.UserDirectory;
                }
            });
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        private void saveSetting()
        {
            SettingManager.SetValue.EncodingName = comboBox_Encoding.Text;
            SettingManager.SetValue.FileSuffixs = textBox_SearchSuffixs.Text;
            SettingManager.SetValue.IsIgnoreExitsFile = checkBox_IsIgnoreExitsFile.Checked;
            SettingManager.SetValue.IsCheckUpdate = checkBox_IsCheckUpdate.Checked;
            SettingManager.SetValue.DownloadThreadNum = int.Parse(textBox_DownLoadThreadNum.Text);
            if (comboBox_LrcOutput.SelectedIndex == 2) SettingManager.SetValue.UserDirectory = "ID3v2";
            else if(comboBox_LrcOutput.SelectedIndex == 0) SettingManager.SetValue.UserDirectory = string.Empty;
            SettingManager.Save();
        }

        private void comboBox_LrcOutput_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(comboBox_LrcOutput.SelectedIndex == 1)
            {
                FolderBrowserDialog _folderDlg = new FolderBrowserDialog();
                _folderDlg.Description = "请选择歌词的输出目录：";
                _folderDlg.ShowDialog();
                if(!string.IsNullOrEmpty(_folderDlg.SelectedPath))
                {
                    SettingManager.SetValue.UserDirectory = _folderDlg.SelectedPath;
                }
            }
        }
    }
}
