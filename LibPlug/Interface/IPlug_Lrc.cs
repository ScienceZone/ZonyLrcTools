using LibPlug.Model;
namespace LibPlug.Interface
{
    public interface IPlug_Lrc
    {
        PluginsAttribute PlugInfo { get; set; }
        bool DownLoad(string artist,string songName,out byte[] lrcData);
    }
}
