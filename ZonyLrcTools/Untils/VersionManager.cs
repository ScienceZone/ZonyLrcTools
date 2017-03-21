using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibNet;
using ZonyLrcTools.EnumDefine;

namespace ZonyLrcTools.Untils
{
    /// <summary>
    /// 版本管理器
    /// </summary>
    public static class VersionManager
    {
        /// <summary>
        /// 当前版本
        /// </summary>
        public static Version CurrentVersion { get; private set; }
        /// <summary>
        /// 新版本信息
        /// </summary>
        public static NewVersionInfo Info { get; private set; }

        static VersionManager()
        {
            CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            Info = null;
        }

        /// <summary>
        /// 检测程序更新
        /// </summary>
        public static bool CheckUpdate()
        {
            return Task.Run(() =>
            {
                try
                {
                    string _resultStr = new NetUtils().HttpGet("http://www.myzony.com/update.ver", Encoding.Default);
                    string[] _result = _resultStr.TrimEnd(',').Split(',');

                    Version _newVersion = new Version(_result[0]);
                    if (_newVersion > CurrentVersion)
                    {
                        // 对更新信息进行换行操作
                        var _sb = new StringBuilder();
                        foreach (var item in _result[2].Split('|'))
                        {
                            _sb.Append(item + "\r\n");
                        }

                        Info = new NewVersionInfo() { DownLoadUrl = _result[1], UpdateInfo = _sb.ToString(), NewVersion = _newVersion.ToString() };
                        return true;
                    }
                    else return false;
                }
                catch (Exception E)
                {
                    LogManager.WriteLogRecord(StatusHeadEnum.EXP, "网络异常，请关闭代理软件或者检查网络再次重试!", E);
                    return false;
                }
            }).Result;
        }
    }

    /// <summary>
    /// 版本信息
    /// </summary>
    public class NewVersionInfo
    {
        /// <summary>
        /// 新版本下载地址
        /// </summary>
        public string DownLoadUrl { get; set; }
        /// <summary>
        /// 更新信息
        /// </summary>
        public string UpdateInfo { get; set; }
        /// <summary>
        /// 新版本版本号
        /// </summary>
        public string NewVersion { get; set; }
    }
}
