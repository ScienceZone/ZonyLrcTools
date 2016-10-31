namespace LibPlug.Model
{
    /// <summary>
    /// 音乐文件信息对象
    /// </summary>
    public class MusicInfoModel
    {
        /// <summary>
        /// 歌手/艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string SongName { get; set; }
        /// <summary>
        /// 歌曲文件路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 是否有专辑图像
        /// </summary>
        public bool IsAlbumImg { get; set; }
        /// <summary>
        /// 是否有内置歌词
        /// </summary>
        public bool IsBuildInLyric { get; set; }
        /// <summary>
        /// 歌曲文件后缀名
        /// </summary>
        public string FileExt { get; set; }
    }
}
