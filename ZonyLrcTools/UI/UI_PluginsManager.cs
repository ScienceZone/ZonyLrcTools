using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZonyLrcTools.Plugin;
using ZonyLrcTools.Untils;

namespace ZonyLrcTools.UI
{
    public partial class UI_PluginsManager : Form
    {
        public UI_PluginsManager()
        {
            InitializeComponent();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView_Plugins.Items)
            {
                SettingManager.SetValue.PluginsStatus.FirstOrDefault(x => x.PluginName.Equals(item.SubItems[0].Text)).IsOpen = item.Checked;
            }
            Close();
        }

        private void UI_PluginsManager_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.App;
            fillPluginsInfo(GlobalMember.MusicTagPluginsManager);
            fillPluginsInfo(GlobalMember.LrcPluginsManager);
            Task.Run(() =>
            {
                foreach (ListViewItem item in listView_Plugins.Items)
                {
                    if (SettingManager.SetValue.PluginsStatus.Where(x => x.PluginName.Equals(item.SubItems[0].Text)).Count() <= 0)
                    {
                        item.Checked = true;
                        SettingManager.SetValue.PluginsStatus.Add(new PluginStatusModel() { IsOpen = true, PluginName = item.SubItems[0].Text });
                    }
                    item.Checked = SettingManager.SetValue.PluginsStatus.FirstOrDefault(x => x.PluginName.Equals(item.SubItems[0].Text)).IsOpen;
                }
            });
        }

        private void fillPluginsInfo<T>(BasePlugins<T> manager)
        {
            foreach (var item in manager.PluginInfos)
            {
                listView_Plugins.Items.Add(new ListViewItem(new string[]
                {
                    item.PlugName,
                    item.Descript,
                    item.Author,
                    item.TypeEnum.ToString(),
                    item.Version.ToString()
                }));
            }
        }

        private void UI_PluginsManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            SettingManager.Save();
        }
    }
}
