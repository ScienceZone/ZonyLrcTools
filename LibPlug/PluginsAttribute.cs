using System;

namespace LibPlug
{
    public class PluginsAttribute : Attribute
    {
        /// <summary>
        /// 初始化插件信息
        /// </summary>
        /// <param name="PlugName">插件名称</param>
        /// <param name="Author">插件作者</param>
        /// <param name="Descript">插件描述</param>
        /// <param name="Version">插件版本</param>
        /// <param name="TypeEnum">插件类型</param>
        public PluginsAttribute(string PlugName,string Author,string Descript,int Version,PluginTypesEnum TypeEnum)
        {
            _PlugName = PlugName;
            _Author = Author;
            _Descript = Descript;
            _Version = Version;
            _TypeEnum = TypeEnum;
        }

        #region > 插件属性定义 <
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PlugName { get { return _PlugName; } }
        /// <summary>
        /// 插件作者
        /// </summary>
        public string Author { get { return _Author; } }
        /// <summary>
        /// 插件描述
        /// </summary>
        public string Descript { get { return _Descript; } }
        /// <summary>
        /// 插件版本
        /// </summary>
        public int Version { get { return _Version; } }
        /// <summary>
        /// 插件类型
        /// </summary>
        public PluginTypesEnum TypeEnum { get { return _TypeEnum; } }
        #endregion

        private string _PlugName;
        private string _Author;
        private string _Descript;
        private int _Version;
        private PluginTypesEnum _TypeEnum;
    }
}
