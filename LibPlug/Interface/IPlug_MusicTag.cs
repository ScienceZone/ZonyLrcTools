using System.IO;
using LibPlug.Model;

namespace LibPlug.Interface
{
    public interface IPlug_MusicTag
    {
        PluginsAttribute PlugInfo { get; set; }
        void LoadTag(string path, MusicInfoModel info);
        Stream LoadAlbumImg(string path);
    }
}
