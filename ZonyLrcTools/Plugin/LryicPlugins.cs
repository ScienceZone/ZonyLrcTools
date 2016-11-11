using LibPlug;
using LibPlug.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ZonyLrcTools.Plugin
{
    public class LryicPlugins : BasePlugins<IPlug_Lrc>
    {
        protected override void CallBack()
        {
            for (int i = 0; i < PluginInfos.Count; i++)
            {
                Plugins[i].PlugInfo = PluginInfos[i];
            }
        }

        /// <summary>
        /// 获得指定类型的插件集
        /// </summary>
        /// <returns></returns>
        public List<IPlug_Lrc> BaseOnTypeGetPlugins(PluginTypesEnum pType)
        {
            return Plugins.Where(x => x.PlugInfo.TypeEnum == pType).ToList();
        }
    }
}
