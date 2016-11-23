using LibPlug.Model;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using ZonyLrcTools.EnumDefine;
using System.Collections.Generic;

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
        public static string[] SearchFiles(string dirPath,string[] exts)
        {
            List<string> _result = new List<string>();
            if (!Directory.Exists(dirPath)) return _result.ToArray();

            foreach (var ext in exts)
            {
                try
                {
                    string[] _files = Directory.GetFiles(dirPath, ext, SearchOption.TopDirectoryOnly);
                    foreach (var fileName in _files)
                    {
                        _result.Add(fileName); 
                    }
                }
                catch (Exception E)
                {
                    LogManager.WriteLogRecord(StatusHeadEnum.EXP, "在函数SearchFiles发生异常。", E);
                    continue;
                }
            }
            return _result.ToArray();
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

        private static string[] getFiles(string dir,string ext)
        {
            List<string> _result = new List<string>();
            if(!string.IsNullOrEmpty(dir))
            {
                
            }return _result.ToArray();
        }
    }
}
