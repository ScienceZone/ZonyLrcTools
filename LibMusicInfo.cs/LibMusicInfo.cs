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
            try
            {
                ID3Info _info = new ID3Info(path, true);
                if (_info.ID3v2Info.AttachedPictureFrames.Items.Length == 0) return null;
                MemoryStream _imgStream = _info.ID3v2Info.AttachedPictureFrames.Items[0].Data;
                return _imgStream;
            }
            catch
            {
                return null;
            }
        }

        public string LoadLyricText(string path)
        {
            ID3Info _info = new ID3Info(path, true);
            string _lyric = _info.ID3v2Info.GetTextFrame("TEXT");
            return !string.IsNullOrEmpty(_lyric) ? _lyric : "没有内置歌词";
        }

        public void LoadTag(string path, MusicInfoModel info)
        {
            string _songName = null;
            string _artist = null;
            string _fileName = Path.GetFileNameWithoutExtension(path);
            try
            {
                ID3Info _info = new ID3Info(path, true);
                _songName = _info.ID3v1Info.Title != null ? _info.ID3v1Info.Title : _info.ID3v2Info.GetTextFrame("TIT2").Replace("�", string.Empty);
                _artist = _info.ID3v1Info.Artist != null ? _info.ID3v1Info.Artist : _info.ID3v2Info.GetTextFrame("TPE1").Replace("�", string.Empty);
                string _album = _info.ID3v1Info.Album != null ? _info.ID3v1Info.Album : _info.ID3v2Info.GetTextFrame("TALB").Replace("�", string.Empty);

                if (string.IsNullOrEmpty(_songName)) _songName = _fileName;
                if (string.IsNullOrEmpty(_artist)) _artist = _fileName;

                info.TagType = _info.ID3v1Info.HaveTag ? "ID3v1" : "ID3v2";
                info.IsAlbumImg = _info.ID3v2Info.AttachedPictureFrames.Count > 0 ? true : false;
                info.IsBuildInLyric = string.IsNullOrEmpty(_info.ID3v2Info.GetTextFrame("TEXT"));
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
            ID3Info _info = null;
            try
            {
                _info = new ID3Info(info.Path, true);
                _info.ID3v2Info.SetTextFrame("TIT2", info.SongName);
                _info.ID3v2Info.SetTextFrame("TPE1", info.Artist);
                _info.ID3v2Info.SetTextFrame("TALB", info.Album);

                MemoryStream _ms = null;
                if (imgBytes != null)
                {
                    // 将专辑图像数据添加进Mp3文件当中
                    _ms = new MemoryStream(imgBytes);
                    _info.ID3v2Info.AttachedPictureFrames.Add(new ID3.ID3v2Frames.BinaryFrames.AttachedPictureFrame(0, "ZonyLrc", TextEncodings.Ascii, "image/jpeg", ID3.ID3v2Frames.BinaryFrames.AttachedPictureFrame.PictureTypes.Media, _ms));
                }

                if (!string.IsNullOrEmpty(lyric)) _info.ID3v2Info.SetTextFrame("TEXT", lyric);

                _info.Save();
                if (_ms != null) _ms.Close();
            }
            catch{ }
        }
    }
}
