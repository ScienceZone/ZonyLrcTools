using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private async void laodSetting()
        {
            await Task.Run(() => 
            {
                SettingManager.Load();
                comboBox_Encoding.Text = SettingManager.SetValue.EncodingName;
            });
        }
        private void saveSetting()
        {
            SettingManager.SetValue.EncodingName = comboBox_Encoding.Text;
            SettingManager.Save();
        }
    }
}
