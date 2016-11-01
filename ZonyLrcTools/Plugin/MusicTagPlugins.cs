using LibPlug.Interface;

namespace ZonyLrcTools.Plugin
{
    public class MusicTagPlugins : BasePlugins<IPlug_MusicTag>
    {
        protected override void CallBack()
        {
            for(int i =0;i<PluginInfos.Count;i++)
            {
                Plugins[i].PlugInfo = PluginInfos[i];
            }
        }
    }
}
