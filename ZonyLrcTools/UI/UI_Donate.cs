using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibNet;

namespace ZonyLrcTools.UI
{
    public partial class UI_Donate : Form
    {
        public UI_Donate()
        {
            InitializeComponent();
        }

        private void UI_Donate_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.App;
            getDonateInfo();
        }

        private async void getDonateInfo()
        {
            if (await getDonateInfoTask())
            {
                label_LoadingText.Text = "加载完成.";
                label_LoadingText.Visible = false;
            }
            else label_LoadingText.Text = "网络发生异常，无法获取列表...";
        }

        private Task<bool> getDonateInfoTask()
        {
            return Task.Run(() => 
            {
                string _result = new NetUtils().HttpGet("http://www.myzony.com/donatelist.txt", Encoding.UTF8);
                if (string.IsNullOrEmpty(_result)) return false;
                string[] _items = _result.Split('|');
                foreach(var item in _items)
                {
                    string[] _listItem = item.Split(',');
                    listView1.Items.Add(new ListViewItem(_listItem));
                }
                return true;
            });
        }
    }
}
