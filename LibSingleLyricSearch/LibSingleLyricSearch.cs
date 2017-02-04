using System;
using LibPlug.Interface;
using LibPlug;
using LibPlug.Model;

namespace LibSingleLyricSearch
{
    [Plugins("单歌词搜索插件", "Zony", "可以进行单首歌曲的歌词进行搜索。", 1000, PluginTypesEnum.DIY)]
    public class LibSingleLyricSearch : IPlug_DIY
    {
        public PluginsAttribute PlugInfo { get; set; }
        private ResourceModel m_shareResource;

        public void Init(ResourceModel shareResModel)
        {
            m_shareResource = shareResModel;
            var _buttonRef = m_shareResource.UI_Main_TopButtonMenu.Items.Add("歌词搜索");
            _buttonRef.Click += (object sender, EventArgs e) => { new UI_SearchWindow().Show(); };
        }
    }
}
