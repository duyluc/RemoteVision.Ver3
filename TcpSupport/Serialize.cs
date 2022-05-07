using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VisionSupport;
using System.Drawing;

namespace TcpSupport
{
    public static class Serialize
    {
        static public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        static public object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = binForm.Deserialize(memStream);
            return obj;
        }

        static public byte[] TerminalToByteArray(Dictionary<string,Terminal> _termianl,out long _taktTime)
        {
            // Takt Time
            _taktTime = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (_termianl == null)
            {
                // Takt Time
                _taktTime = sw.ElapsedMilliseconds;
                sw.Stop();
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, _termianl);
                // Takt Time
                _taktTime = sw.ElapsedMilliseconds;
                sw.Stop();
                return ms.ToArray();
            }
        }

        static public Dictionary<string, Terminal> ByteArrayToTerminal(byte[] arrBytes,out long _taktTime)
        {
            // Takt Time
            _taktTime = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Dictionary<string, Terminal> _termianl = binForm.Deserialize(memStream) as Dictionary<string, Terminal>;
            // Takt Time
            _taktTime = sw.ElapsedMilliseconds;
            sw.Stop();
            return _termianl;
        }

        static public byte[] TerminalToByteArray(Dictionary<string, Terminal> _termianl)
        {
            if (_termianl == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, _termianl);
                return ms.ToArray();
            }
        }

        static public Dictionary<string, Terminal> ByteArrayToTerminal(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Dictionary<string, Terminal> _termianl = binForm.Deserialize(memStream) as Dictionary<string, Terminal>;
            return _termianl;
        }

        static public byte[] ImageToTerminal(Bitmap _bitImage)
        {
            byte[] data;
            using (Image bmp = _bitImage)
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                data = ms.ToArray();
            }
            return data;
        }

        static public byte[] ImageToTerminal(Bitmap _bitImage,out long _taktTime)
        {
            // Takt Time
            _taktTime = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            byte[] data;
            using (Image bmp = _bitImage)
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                data = ms.ToArray();
            }
            // Takt Time
            _taktTime = sw.ElapsedMilliseconds;
            sw.Stop();
            return data;
        }
    }
}
