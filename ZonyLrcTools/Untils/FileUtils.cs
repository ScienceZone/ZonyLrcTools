using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return false;
            }
            return false;
        }
    }
}
