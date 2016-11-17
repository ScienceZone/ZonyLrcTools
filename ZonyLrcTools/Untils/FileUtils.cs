using LibPlug.Model;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using ZonyLrcTools.EnumDefine;

namespace ZonyLrcTools.Untils
{
    /// <summary>
    /// 文件操作实用类
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 将数据写入到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">要写入的byte数据</param>
        /// <returns></returns>
        public static bool WriteFile(string filePath,byte[] data)
        {
            try
            {
                using (FileStream _fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    _fs.Write(data, 0, data.Length);
                    return true;
                }
            }
            catch(Exception E)
            {
                LogManager.WriteLogRecord(StatusHeadEnum.EXP, "在方法WriteFile发生异常!", E);
                return false;
            }
        }

        /// <summary>
        /// 将数据写入到文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="data">要写入的字符串数据</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static bool WriteFile(string filePath,string data,Encoding encoding)
        {
            byte[] _dataBytes = encoding.GetBytes(data);
            try
            {
                using (FileStream _fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    _fs.Write(_dataBytes, 0, _dataBytes.Length);
                }
            }
            catch(Exception E)
            {
                LogManager.WriteLogRecord(StatusHeadEnum.EXP, "在方法WriteFile发生异常!", E);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 搜索文件，并将文件存入全局音乐文件字典
        /// </summary>
        /// <param name="dirPath">要搜索的目录</param>
        /// <param name="exts">音乐文件后缀名集合</param>
        /// <returns></returns>
        public static bool SearchFiles(string dirPath,string[] exts)
        {
            if (!Directory.Exists(dirPath)) return false;
            int _invaildCount = 0; // 未搜索到的后缀名文件计数
            try
            {
                foreach(var ext in exts)
                {
                    string[] _files = Directory.GetFiles(dirPath, ext, SearchOption.AllDirectories);
                    if (_files.Length != 0)
                    {
                        int _count = 0;
                        foreach (var fileName in _files)
                        {
                            GlobalMember.AllMusics.Add(_count, new MusicInfoModel() { Path = fileName });
                            _count++;
                        }
                    }
                    else _invaildCount++;
                }

                if (_invaildCount == exts.Length) return false;
                else return true;
            }
            catch(Exception E)
            {
                LogManager.WriteLogRecord(StatusHeadEnum.EXP, "在方法SearchFiles发生异常！", E);
                return false;
            }
        }

        /// <summary>
        /// 在资源管理器当中定位文件的位置
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void OpenFilePos(string filePath)
        {
            ProcessStartInfo _start = new ProcessStartInfo("Explorer.exe");
            _start.Arguments = "/e,/select," + filePath;
            Process.Start(_start);
        }
    }
}
