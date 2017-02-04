using System;
using System.Text;

namespace ZonyLrcTools.Untils
{
    /// <summary>
    /// 编码转换器
    /// </summary>
    public class EncodingConverter
    {
        public virtual byte[] ConvertBytes(byte[] _sourceBytes, string encodingName)
        {
            return Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(encodingName), _sourceBytes);
        }
    }

    public class EncodingUTF8_Bom : EncodingConverter
    {
        public override byte[] ConvertBytes(byte[] _sourceBytes, string encodingName)
        {
            byte[] _tmpData = new byte[_sourceBytes.Length + 3];
            _tmpData[0] = 0xef; _tmpData[1] = 0xbb; _tmpData[2] = 0xbf;

            Array.Copy(_sourceBytes, 0, _tmpData, 3, _sourceBytes.Length);
            return _tmpData;
        }
    }

    public class EncodingANSI : EncodingConverter
    {
        public override byte[] ConvertBytes(byte[] _sourceBytes, string encodingName)
        {
            return Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(encodingName), _sourceBytes);
        }
    }
}