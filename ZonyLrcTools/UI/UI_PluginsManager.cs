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
            foreach(var item in GlobalMember.MusicTagPluginsManager.Plugins)
            {
                listView_Plugins.Items.Add(new ListViewItem(new string[]
                {
                    item.PlugInfo.PlugName,
                    item.PlugInfo.Descript,
                    item.PlugInfo.Author,
                    item.PlugInfo.TypeEnum.ToString(),
                    item.PlugInfo.Version.ToString()
                }));
            }
        }
    }
}
