using LibPlug.Interface;
using LibPlug.Model;

namespace ZonyLrcTools.Plugin
{
    public class DIYPlugins : BasePlugins<IPlug_DIY>
    {
        public int LoadPlugins(ResourceModel res)
        {
            int _loadCount = base.LoadPlugins();
            if (_loadCount != 0)
            {
                // 初始化高级插件资源
                foreach (var item in Plugins)
                {
                    item.Init(res);
                }
            }

            return _loadCount;
        }

        protected override void CallBack()
        {
            for (int i = 0; i < PluginInfos.Count; i++)
            {
                Plugins[i].PlugInfo = PluginInfos[i];
            }
        }
    }
}
