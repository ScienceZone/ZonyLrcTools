using System;
using LibPlug;
using LibPlug.Interface;
namespace LibAlbumImg
{
    [Plugins("专辑图像下载插接件","Zony","从网易云音乐下载专辑图像写入到歌曲文件。",1000,PluginTypesEnum.AlbumImg)]
    public class LibAlbumImg : IPlug_Lrc
    {
        public PluginsAttribute PlugInfo { get; set; }

        public bool DownLoad(string artist, string songName, out byte[] lrcData)
        {
            throw new NotImplementedException();
        }
    }
}
