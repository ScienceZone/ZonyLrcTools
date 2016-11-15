using LibNet;
using LibPlug;
using LibPlug.Interface;
using System.Text;
using System.Text.RegularExpressions;

namespace LibLyricBaiDu
{
    [Plugins("百度音乐歌词下载插件", "Zony", "从百度音乐搜索歌曲歌词并下载.", 1100, PluginTypesEnum.LrcSource)]
    public class LibLyricBaiDuMusic : IPlug_Lrc
    {
        public PluginsAttribute PlugInfo { get; set; }
        private NetUtils m_netUtils = new NetUtils();

        public bool DownLoad(string artist, string songName, out byte[] lrcData)
        {
            // 请求URL构建
            const string Request_Url = "http://music.baidu.com/search/lrc?key=";
            const string ResponseUrl = "http://music.baidu.com";

            string _key = artist + "+" + songName;
            string _result = m_netUtils.HttpGet(Request_Url + _key, Encoding.UTF8);

            if (!string.IsNullOrWhiteSpace(_result))
            {
                Regex _reg = new Regex("/data2/lrc/\\d*/\\d*.lrc");
                string _lrcURL = _reg.Match(_result).ToString();

                string _lrcData = m_netUtils.HttpGet(ResponseUrl + _lrcURL,Encoding.UTF8);
                lrcData = Encoding.UTF8.GetBytes(_lrcData);
                return true;
            }
            else
            {
                lrcData = null;
                return false;
            }
        }
    }
}
