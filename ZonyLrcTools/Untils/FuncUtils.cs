using System.Collections.Generic;
namespace ZonyLrcTools.Untils
{
    public static class FuncUtils
    {
        public static void AddRange<T,V>(this Dictionary<T,V> value,Dictionary<T,V> container)
        {
            foreach(var item in container)
            {
                value.Add(item.Key, item.Value);
            }
        }
    }
}
