namespace LibPlug.Interface
{
    public interface IPlug_Lrc
    {
        bool DownLoad(string artist,string songName,out byte[] lrcData);
    }
}
