using LibPlug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ZonyLrcTools.Plugin
{
    public class BasePlugins<T>
    {
        /// <summary>
        /// 已加载的插件列表
        /// </summary>
        public List<T> Plugins { get; }
        /// <summary>
        /// 插件信息列表
        /// </summary>
        public List<PluginsAttribute> PluginInfos{ get; }
        public Assembly DllAssembly { get; set; }
        public Type Plug { get; set; }

        private Type m_infaceType;

        public BasePlugins()
        {
            m_infaceType = typeof(T);
            PluginInfos = new List<PluginsAttribute>();
            Plugins = new List<T>();
        }

        /// <summary>
        /// 加载插件
        /// </summary>
        /// <returns></returns>
        public virtual int LoadPlugins()
        {
            Plugins.Clear();
            PluginInfos.Clear();

            if (!Directory.Exists(Environment.CurrentDirectory + @"\Plugins")) return 0;
            string[] _files = Directory.GetFiles(Environment.CurrentDirectory + @"\Plugins");
            PluginsAttribute _info = new PluginsAttribute();

            foreach(var item in _files)
            {
                string _ext = Path.GetExtension(item);
                if (!_ext.Equals(".dll")) continue;

                try
                {
                    Assembly _asm = DllAssembly = Assembly.UnsafeLoadFrom(item);
                    Type[] _types = _asm.GetTypes();
                    foreach(var t in _types)
                    {
                        if(t.GetInterface(m_infaceType.Name) != null)
                        {
                            T _plug = (T)_asm.CreateInstance(t.FullName); Plug = t;
                            Plugins.Add(_plug);
                            // 获得插件信息
                            _info = t.GetCustomAttribute(typeof(PluginsAttribute), false) as PluginsAttribute;
                            PluginInfos.Add(_info);

                            CallBack();
                        }
                    }
                }
                catch
                {
                    return 0;
                }
            }

            return PluginInfos.Count;
        }

        protected virtual void CallBack() { }
    }
}