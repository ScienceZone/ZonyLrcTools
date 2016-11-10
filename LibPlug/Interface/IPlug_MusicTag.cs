using System.IO;
using LibPlug.Model;

namespace LibPlug.Interface
{
    public interface IPlug_MusicTag
    {
        PluginsAttribute PlugInfo { get; set; }
        /// <summary>
        /// 加载ID3v2信息到信息模型 
        /// </summary>
        /// <param name="path">歌曲文件路径</param>
        /// <param name="info">信息模型</param>
        void LoadTag(string path, MusicInfoModel info);
        /// <summary>
        /// 加载专辑图像
        /// </summary>
        /// <param name="path">歌曲文件路径</param>
        /// <returns></returns>
        Stream LoadAlbumImg(string path);
        /// <summary>
        /// 将信息模型保存到歌曲文件的ID3v2
        /// </summary>
        /// <param name="info">信息模型</param>
        /// <param name="imgBytes">专辑图像数据</param>
        void SaveTag(MusicInfoModel info, byte[] imgBytes);
    }
}
