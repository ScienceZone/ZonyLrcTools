using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZonyLrcTools.Plugin
{
    public class BasePlugins<T> where T : class
    {
        public List<T> Plugins { get; }
        

    }
}
