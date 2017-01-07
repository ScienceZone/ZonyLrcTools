using System.IO;
using LibPlug;
using LibPlug.Interface;
using LibPlug.Model;
using System.Collections.Generic;

namespace LibMusicInfo.cs
{
    [Plugins("音乐信息提取插件", "Zony", "可以从常规的音乐文件当中提取音乐信息。", 1000, PluginTypesEnum.Mp3Tag)]
    public class LibMusicInfo : IPlug_MusicTag
    {
        public PluginsAttribute PlugInfo { get; set; }

        public Stream LoadAlbumImg(string path)
        {
            try
            {
                TagLib.File _info = TagLib.File.Create(path);
                if (_info.Tag.Pictures.Length == 0) return null;
                MemoryStream _imgStream = new MemoryStream(_info.Tag.Pictures[0].Data.Data);
                return _imgStream;
            }
            catch
            {
                return null;
            }
        }

        public string LoadLyricText(string path)
        {
            TagLib.File _info = TagLib.File.Create(path);
            string _lyric = _info.Tag.Lyrics;
            return !string.IsNullOrEmpty(_lyric) ? _lyric : "没有内置歌词";
        }

        public void LoadTag(string path, MusicInfoModel info)
        {
            string _songName = null;
            string _artist = null;
            string _fileName = Path.GetFileNameWithoutExtension(path);
            try
            {
                TagLib.File _info = TagLib.File.Create(path);
                _songName = _info.Tag.Title;
                _artist = _info.Tag.FirstPerformer;
                string _album = _info.Tag.Album;

                if (string.IsNullOrEmpty(_songName)) _songName = _fileName;
                if (string.IsNullOrEmpty(_artist)) _artist = _fileName;

                info.TagType = string.Join(",", _info.TagTypes);
                info.IsAlbumImg = _info.Tag.Pictures.Length > 0 ? true : false;
                info.IsBuildInLyric = string.IsNullOrEmpty(_info.Tag.Lyrics);
                info.Album = _album;
            }
            catch
            {
                if (string.IsNullOrEmpty(_songName)) _songName = _fileName;
                if (string.IsNullOrEmpty(_artist)) _artist = string.Empty;
                info.TagType = "无";
                info.IsAlbumImg = false;
                info.IsBuildInLyric = false;
                info.Album = "无";
            }

            info.Artist = _artist;
            info.SongName = _songName;
        }

        public void SaveTag(MusicInfoModel info,byte[] imgBytes,string lyric)
        {
            try
            {
                TagLib.File _info = TagLib.File.Create(info.Path);
                _info.Tag.Title = info.SongName;
                _info.Tag.Performers[0] = info.Artist;
                _info.Tag.Album = info.Album;

                MemoryStream _ms = null;
                if (imgBytes != null)
                {
                    // 将专辑图像数据添加进Mp3文件当中
                    var _picList = new List<TagLib.Picture>();
                    _picList.Add(new TagLib.Picture(new TagLib.ByteVector(imgBytes)));
                    _info.Tag.Pictures = _picList.ToArray();
                }

                if (!string.IsNullOrEmpty(lyric)) _info.Tag.Lyrics = lyric;

                _info.Save();
                if (_ms != null) _ms.Close();
            }
            catch{ }
        }
    }
}
