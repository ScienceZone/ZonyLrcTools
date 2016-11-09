using LibPlug;
using LibPlug.Interface;
using LibNet;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;

namespace LibAlbumImg
{
    [Plugins("专辑图像下载插接件","Zony","从网易云音乐下载专辑图像写入到歌曲文件。",1000,PluginTypesEnum.AlbumImg)]
    public class LibAlbumImg : IPlug_Lrc
    {
        private NetUtils m_netUtils = new NetUtils();
        public PluginsAttribute PlugInfo { get; set; }

        public bool DownLoad(string artist, string songName, out byte[] lrcData)
        {
            lrcData = null;
            string _songName = m_netUtils.URL_Encoding(songName, Encoding.UTF8);
            string _artist = m_netUtils.URL_Encoding(artist, Encoding.UTF8);
            string _key = string.Format("{0}+{1}", _artist, _songName);

            string _requestUrl = @"http://music.163.com/api/search/get/web?csrf_token=";
            string _postData = "&s=" + _key + "&type=1&offset=0&total=true&limit=5";
            string _result = m_netUtils.HttpPost(_requestUrl, Encoding.UTF8, _postData, "http://music.163.com");

            string _sid = getSID(_result);
            if (_sid == null) return false;

            //获得专辑图像
            string songURL = "http://music.163.com/api/song/detail/?id=" + _sid + "&ids=%5B" + _sid + "%5D";
            _result = m_netUtils.HttpGet(songURL, Encoding.UTF8);

            string _imgUrl = getImage(_result);
            lrcData = new WebClient().DownloadData(_imgUrl);
            return true;
        }

        /// <summary>
        /// 获得歌曲的SID
        /// </summary>
        /// <returns></returns>
        private string getSID(string json)
        {
            JObject _jsonSid = JObject.Parse(json);
            if (!json.Contains("result")) return null;
            if (_jsonSid["result"]["songCount"].Value<int>() == 0)
            {
                return null;
            }

            JArray _jarraySID = (JArray)_jsonSid["result"]["songs"];
            return _jarraySID[0]["id"].ToString();
        }

        /// <summary>
        /// 获得专辑图像地址
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private string getImage(string json)
        {
            JArray _songs = (JArray)JObject.Parse(json)["songs"];
            return _songs[0]["album"]["picUrl"].ToString();
        }
    }
}
