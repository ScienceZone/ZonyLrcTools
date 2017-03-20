using LibNet;
using LibPlug;
using LibPlug.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibLyricQQMusic
{
    [Plugins("QQ音乐歌词下载", "Zony", "从QQ音乐下载歌词", 1000, PluginTypesEnum.LrcSource)]
    public class LibLyricQQMusic : IPlug_Lrc
    {
        private NetUtils m_netUtils = new NetUtils();

        private const string QQMUSIC_SERVIER_URL = "http://qqmusic.qq.com/fcgi-bin/qm_getLyricId.fcg?";
        //private const string

        public PluginsAttribute PlugInfo { get; set; }

        public bool DownLoad(string artist, string songName, out byte[] lrcData, bool isOpenTrans)
        {
            lrcData = null;
            string _queryString = buildQQSearchString(artist, songName);
            string _result = m_netUtils.HttpGet(_queryString, Encoding.Default);
            return false;
        }

        /// <summary>
        /// 构建查询字符串
        /// </summary>
        /// <param name="artist">歌手</param>
        /// <param name="songName">歌曲名称</param>
        /// <returns>构建完成的查询字符串</returns>
        private string buildQQSearchString(string artist, string songName)
        {
            string _encArtist = m_netUtils.URL_Encoding(artist, Encoding.Default);
            string _encSongName = m_netUtils.URL_Encoding(songName, Encoding.Default);

            return string.Format("{0}name={1}&singer={2}&from=qqplayer", QQMUSIC_SERVIER_URL, _encSongName, _encArtist);
        }
    }
}
