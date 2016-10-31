using System.Collections.Generic;

namespace ZonyLrcTools.Plugin
{
    public class BasePlugins<T> where T : class
    {
        public List<T> Plugins { get; }
        

    }
}
