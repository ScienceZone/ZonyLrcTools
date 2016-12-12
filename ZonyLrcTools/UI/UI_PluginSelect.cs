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
    public partial class UI_PluginSelect : Form
    {
        /// <summary>
        /// 已选择的插件名称
        /// </summary>
        public string SelectPluginName { get; set; }

        public UI_PluginSelect()
        {
            InitializeComponent();
        }

        private void UI_PluginSelect_Load(object sender, EventArgs e)
        {
            // 获得可用的歌词源插件
            var _plugins = GlobalMember.LrcPluginsManager.BaseOnTypeGetPlugins(LibPlug.PluginTypesEnum.LrcSource);
            foreach(var item in _plugins)
            {
                listBox_LrcSourcePluginsList.Items.Add(item.PlugInfo.PlugName);
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if(listBox_LrcSourcePluginsList.SelectedIndex != -1)
            {
                SelectPluginName = listBox_LrcSourcePluginsList.Items[listBox_LrcSourcePluginsList.SelectedIndex].ToString();
            }
            Close();
        }
    }
}
