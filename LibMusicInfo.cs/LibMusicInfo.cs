using System.IO;
using LibPlug;
using LibPlug.Interface;
using LibPlug.Model;
using ID3;

namespace LibMusicInfo.cs
{
    [Plugins("音乐信息提取插件", "Zony", "可以从常规的音乐文件当中提取音乐信息。", 1000, PluginTypesEnum.Mp3Tag)]
    public class LibMusicInfo : IPlug_MusicTag
    {
        public PluginsAttribute PlugInfo { get; set; }

        public Stream LoadAlbumImg(string path)
        {
            ID3Info _info = new ID3Info(path, true);
            if (_info.ID3v2Info.AttachedPictureFrames.Items.Length == 0) return null;
            MemoryStream _imgStream = _info.ID3v2Info.AttachedPictureFrames.Items[0].Data;
            return _imgStream;
        }

        public void LoadTag(string path, MusicInfoModel info)
        {
            ID3Info _info = new ID3Info(path, true);
            string _fileName = Path.GetFileNameWithoutExtension(path);
            string _songName = _info.ID3v1Info.Title != null ? _info.ID3v1Info.Title : _info.ID3v2Info.GetTextFrame("TIT2").Replace("�", string.Empty);
            string _artist = _info.ID3v1Info.Artist != null ? _info.ID3v1Info.Artist : _info.ID3v2Info.GetTextFrame("TPE1").Replace("�", string.Empty);
            string _album = _info.ID3v1Info.Album != null ? _info.ID3v1Info.Album : _info.ID3v2Info.GetTextFrame("TALB").Replace("�", string.Empty);

            if (string.IsNullOrEmpty(_songName)) _songName = _fileName;
            if (string.IsNullOrEmpty(_artist)) _artist = _fileName;

            info.TagType = _info.ID3v1Info.HaveTag ? "ID3v1" : "ID3v2";
            info.Artist = _artist;
            info.SongName = _songName;
            info.Album = _album;
            info.IsAlbumImg = _info.ID3v2Info.AttachedPictureFrames.Count > 0 ? true : false;
        }
    }
}
