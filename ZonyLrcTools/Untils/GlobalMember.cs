using System.Collections.Generic;
using LibPlug.Model;
using ZonyLrcTools.Plugin;

namespace ZonyLrcTools.Untils
{
    public static class GlobalMember
    {
        public static string WorkPath = null;
        public static MusicTagPlugins MusicTagPluginsManager = new MusicTagPlugins();

        private static Dictionary<int, MusicInfoModel> m_allMusics = null;
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
