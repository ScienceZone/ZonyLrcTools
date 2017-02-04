namespace LibPlug
{
    /// <summary>
    /// 插件类型枚举
    /// </summary>
    public enum PluginTypesEnum
    {
        /// <summary>
        /// 歌词源下载插件
        /// </summary>
        LrcSource = 0x00000000,
        /// <summary>
        /// MP3标签读取插件
        /// </summary>
        Mp3Tag = LrcSource + 0x01,
        /// <summary>
        /// 专辑图像下载插件
        /// </summary>
        AlbumImg = LrcSource + 0x02,
        /// <summary>
        /// 高级插件
        /// </summary>
        DIY = LrcSource + 0x03
    }
}
