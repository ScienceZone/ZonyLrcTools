using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LibNet;
using Newtonsoft.Json.Linq;

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
            const string _referer = @"http://music.163.com";

            _artist = m_netUtils.URL_Encoding(textBox_Artist.Text, Encoding.UTF8);
            _songName = m_netUtils.URL_Encoding(textBox_SongName.Text, Encoding.UTF8);

            string _buildKey = string.Format("{0}+{1}", _artist, _songName);
            string _requestData = "&s=" + _buildKey + "&type=1&offset=0&total=true&limit=5";
            string _result = m_netUtils.HttpPost(_requestUrl, Encoding.UTF8, _requestData, _referer);

            var _list = decodeSongList(_result);
            renderListView(_list);
        }

        private void ToolStripMenuItem_DownLoad_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 解析歌词列表
        /// </summary>
        /// <returns></returns>
        private List<SongListItem> decodeSongList(string jsonList)
        {
            List<SongListItem> _resultList = new List<SongListItem>();

            JObject _result = JObject.Parse(jsonList);
            JArray _jArray = (JArray)_result["result"]["songs"];
            foreach (var item in _jArray)
            {
                _resultList.Add(new SongListItem()
                {
                    SongName = item.Value<string>("name"),
                    SongID = item.Value<string>("id"),
                    Artist = ((JArray)item["artists"])[0].Value<string>("name")
                });
            }
            return _resultList;
        }

        /// <summary>
        /// 渲染列表
        /// </summary>
        /// <param name="list">歌词列表</param>
        private void renderListView(List<SongListItem> list)
        {
            foreach (var item in list)
            {
                listView_LyricList.Items.Add(new ListViewItem(new string[] { item.SongName, item.Artist }));
            }
        }

        /// <summary>
        /// 获得指定SID的Json歌词数据
        /// </summary>
        /// <param name="sid"></param>
        private string getLyricJson(string sid)
        {
            string _lrcUrl = "http://music.163.com/api/song/lyric?os=osx&id=" + sid + "&lv=-1&kv=-1&tv=-1";
            return m_netUtils.HttpGet(_lrcUrl, Encoding.UTF8, @"http://music.163.com");
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
