using System;
using System.IO;

namespace ZonyLrcTools.Untils
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public static class LogManager
    {
        private static FileStream m_logFile;
        private static string m_logName;
        private static string m_logPath = Environment.CurrentDirectory + @"\LogFiles\";
        private static object m_lockObj = new object();

        static LogManager()
        {
            if (!Directory.Exists(m_logPath)) Directory.CreateDirectory(m_logPath);
            m_logName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            m_logFile = new FileStream(Path.Combine(m_logPath,m_logName), FileMode.OpenOrCreate);
        }

        /// <summary>
        /// 写入日志记录
        /// </summary>
        /// <param name="status">状态</param>
        /// <param name="text">具体信息</param>
        /// <param name="e">错误堆栈</param>
        public static void WriteLogRecord(string status,string text,Exception e = null)
        {
            lock(m_lockObj)
            {
                if (m_logFile != null)
                {
                    StreamWriter _sw = new StreamWriter(m_logFile);
                    _sw.Write(buildWriteString(status, text, e));
                    _sw.Flush();
                    m_logFile.Flush();
                }
            }
        }

        /// <summary>
        /// 构建写入文本
        /// </summary>
        private static string buildWriteString(string status,string text,Exception e)
        {
            string _writeString = "状态:" + status + "\r\n" +
                                  "信息:" + status + "\r\n" +
                                  "错误信息：" + (e == null ? "无" : e.Message != null ? e.Message : e.InnerException.Message) + "\r\n" +
                                  "错误堆栈：" + (e == null ? "无" : e.StackTrace != null ? e.StackTrace : e.InnerException.StackTrace);
            return _writeString;
        }
    }
}
