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

namespace LibSingleLyricSearch
{
    public partial class UI_SearchWindow : Form
    {
        private NetUtils m_netUtils;

        public UI_SearchWindow()
        {
            m_netUtils = new NetUtils();
            InitializeComponent();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            string _artist, _songName;
            const string _requestUrl = @"http://music.163.com/api/search/get/web?csrf_token=";
            _artist = m_netUtils.URL_Encoding(textBox_Artist.Text, Encoding.UTF8);
            _songName = m_netUtils.URL_Encoding(textBox_SongName.Text, Encoding.UTF8);

            string _buildKey = string.Format("{0}+{1}", _artist, _songName);
            string _requestData = "&s=" + _buildKey + "&type=1&offset=0&total=true&limit=5";
            string _result = m_netUtils.HttpPost(_requestUrl, Encoding.UTF8, _requestData, @"http://music.163.com");

            return;
        }

        /// <summary>
        /// 解析歌词列表
        /// </summary>
        /// <returns></returns>
        private List<SongListItem> decodeSongList()
        {
            return null;
        }

        /// <summary>
        /// 歌词结果模型
        /// </summary>
        private class SongListItem
        {
            /// <summary>
            /// 歌曲名称
            /// </summary>
            public string SongName { get; set; }
            /// <summary>
            /// 艺术家/歌手
            /// </summary>
            public string Artist { get; set; }
            /// <summary>
            /// 歌曲SID
            /// </summary>
            public string SongID { get; set; }
        }
    }
}
