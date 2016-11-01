using LibPlug.Interface;
using LibPlug;
using System;

namespace LibLyricNetEase
{
    [Plugins("网易云Lrc歌词下载插件", "Zony", "从网易云下载lrc格式的歌词。", 1300, PluginTypesEnum.Mp3Tag)]
    public class LibLyricNetEase : IPlug_Lrc
    {
        public PluginsAttribute PlugInfo { get; set; }

        public bool DownLoad(string artist, string songName, out byte[] lrcData)
        {
            const string _reqURL = "http://music.163.com";
            lrcData = null;
            return false;
        }
    }
}
