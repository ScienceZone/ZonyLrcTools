using System.Collections.Generic;
using LibPlug.Model;
using ZonyLrcTools.Plugin;

namespace ZonyLrcTools.Untils
{
    public static class GlobalMember
    {
        /// <summary>
        /// 工作路径
        /// </summary>
        public static string WorkPath = null;
        /// <summary>
        /// 音乐标签插件集合
        /// </summary>
        public static MusicTagPlugins MusicTagPluginsManager = new MusicTagPlugins();
        /// <summary>
        /// 歌词源插件集合
        /// </summary>
        public static LryicPlugins LrcPluginsManager = new LryicPlugins();
        /// <summary>
        /// 自定义插件集合
        /// </summary>
        public static DIYPlugins DIYPluginsManager = new DIYPlugins();

        private static Dictionary<int, MusicInfoModel> m_allMusics = null;
        /// <summary>
        /// 歌曲信息集合
        /// </summary>
        public static Dictionary<int, MusicInfoModel> AllMusics
        {
            get
            {
                if (m_allMusics == null) m_allMusics = new Dictionary<int, MusicInfoModel>();
                return m_allMusics;
            }
            set { m_allMusics = value; }
        }
    }
}
