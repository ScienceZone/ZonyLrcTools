using System.Collections.Generic;
namespace LibPlug.Model
{
    /// <summary>
    /// 插件与主程序共享资源模型
    /// </summary>
    public class ResourceModel
    {
        /// <summary>
        /// 歌曲信息集合
        /// </summary>
        public Dictionary<int,MusicInfoModel> MusicInfos { get; set; }
    }
}
