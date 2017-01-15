using System;
using System.Text;
using System.Net;
using System.IO;

namespace LibNet
{
    /// <summary>
    /// 提供HTTP网络操作对象与常用方法
    /// </summary>
    public class NetUtils
    {
        /// <summary>
        /// 对目标URL进行Get操作
        /// </summary>
        /// <param name="url">目标URL</param>
        /// <param name="encoding">返回数据的编码方式</param>
        /// <param name="referer">要提供的来源站点地址</param>
        /// <returns>成功返回数据，否则返回null</returns>
        public string HttpGet(string url,Encoding encoding, string referer = null)
        {
            try
            {
                HttpWebRequest _req = WebRequest.Create(url) as HttpWebRequest;
                _req.Method = "get";
                _req.ContentType = "application/x-www-form-urlencoded";

                if (referer != null) _req.Referer = referer;

                using (HttpWebResponse _res = _req.GetResponse() as HttpWebResponse)
                {
                    if (_res.StatusCode == HttpStatusCode.OK)
                    {
                        StringBuilder _sb = new StringBuilder();
                        using (StreamReader _sr = new StreamReader(_res.GetResponseStream()))
                        {
                            string _data;
                            while((_data = _sr.ReadLine()) != null)
                            {
                                _sb.Append(_data);
                            }
                            return _sb.ToString();
                        }
                    }
                    else return null;
                }
            }
            catch(Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 对目标URL进行Post操作
        /// </summary>
        /// <param name="url">目标URL</param>
        /// <param name="encoding">提交数据的编码方式</param>
        /// <param name="data">要提交的数据</param>
        /// <param name="referer">要提供的来源站点地址</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="UriFormatException"/>
        /// <returns>成功返回数据，否则返回null</returns>
        public string HttpPost(string url,Encoding encoding,string data = null,string referer = null)
        {
            try
            {
                byte[] _data;
                HttpWebRequest _req = WebRequest.Create(url) as HttpWebRequest;
                _req.Method = "post";
                _req.ContentType = "application/x-www-form-urlencoded";
                if (referer != null) _req.Referer = referer;
                if(data != null)
                {
                    _data = encoding.GetBytes(data);
                    _req.ContentLength = _data.Length;
                    using (Stream _stream = _req.GetRequestStream())
                    {
                        _stream.Write(_data, 0, _data.Length);
                    }
                }

                using (HttpWebResponse _res = _req.GetResponse() as HttpWebResponse)
                {
                    if (_res.StatusCode == HttpStatusCode.OK)
                    {
                        using (StreamReader _sr = new StreamReader(_res.GetResponseStream()))
                        {
                            return _sr.ReadToEnd();
                        }
                    }
                    else return null;
                }

            }
            catch(Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// 对传入的数据进行URL编码
        /// </summary>
        /// <param name="data">待编码的数据</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>编码后的数据</returns>
        public string URL_Encoding(string data,Encoding encoding)
        {
            StringBuilder _sb = new StringBuilder();
            byte[] _dataBytes = encoding.GetBytes(data);

            for (int i = 0; i < _dataBytes.Length;i++)
            {
                _sb.Append(@"%" + _dataBytes[i].ToString("x2"));
            }

            return _sb.ToString();
        }
    }
}