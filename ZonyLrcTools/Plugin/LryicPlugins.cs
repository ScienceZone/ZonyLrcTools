using LibPlug;
using LibPlug.Interface;
using System.Collections.Generic;
using System.Linq;
using ZonyLrcTools.Untils;

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
        /// 获得指定已经开启的类型的插件集
        /// </summary>
        /// <returns></returns>
        public List<IPlug_Lrc> BaseOnTypeGetPlugins(PluginTypesEnum pType)
        {
            return Plugins.Where(x => 
            {
                if (x.PlugInfo.TypeEnum == pType)
                    return SettingManager.SetValue.PluginsStatus.FirstOrDefault(y => y.PluginName.Equals(x.PlugInfo.PlugName)).IsOpen;
                else return false;
            }).ToList();
        }

        /// <summary>
        /// 根据插件名称获取插件
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns></returns>
        public IPlug_Lrc BaseOnNameGetPlugin(string pluginName)
        {
            return Plugins.Where(x => x.PlugInfo.PlugName.Equals(pluginName)).FirstOrDefault();
        }
    }
}
