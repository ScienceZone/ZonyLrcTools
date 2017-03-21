using LibPlug.Interface;
using LibPlug.Model;
using ZonyLrcTools.Untils;

namespace ZonyLrcTools.Plugin
{
    public class DIYPlugins : BasePlugins<IPlug_DIY>
    {
        private ResourceModel _resource = null;
        public int LoadPlugins(ResourceModel res)
        {
            _resource = res;
            return base.LoadPlugins();
        }

        public void InitPlugins()
        {
            foreach (var item in Plugins)
            {
                if (SettingManager.SetValue.PluginsStatus.Find(x => x.PluginName == item.PlugInfo.PlugName).IsOpen)
                {
                    item.Init(_resource);
                }
            }
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
