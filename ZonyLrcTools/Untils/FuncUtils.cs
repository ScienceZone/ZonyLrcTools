using System.Collections.Generic;
namespace ZonyLrcTools.Untils
{
    public static class FuncUtils
    {
        /// <summary>
        /// 将某个Dictionary集合添加到另外一个集合的末尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="value"></param>
        /// <param name="container"></param>
        public static void AddRange<T,V>(this Dictionary<T,V> value,Dictionary<T,V> container)
        {
            foreach(var item in container)
            {
                value.Add(item.Key, item.Value);
            }
        }
    }
}
