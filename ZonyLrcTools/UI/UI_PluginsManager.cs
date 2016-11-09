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
            Close();
        }

        private void UI_PluginsManager_Load(object sender, EventArgs e)
        {
            fillPluginsInfo(GlobalMember.MusicTagPluginsManager);
            fillPluginsInfo(GlobalMember.LrcPluginsManager);
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
    }
}
