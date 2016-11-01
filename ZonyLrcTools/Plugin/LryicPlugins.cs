using LibPlug.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
