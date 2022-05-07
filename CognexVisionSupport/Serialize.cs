using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro;

namespace CognexVisionSupport
{
    public static class Serialize
    {
        static public object LoadToolBlock(string _toolpath)
        {
            return CogSerializer.LoadObjectFromFile(_toolpath);
        }
        static public void SaveToolBlock(object _ob, string _path)
        {
            CogSerializer.SaveObjectToFile(_ob, _path);
        }
    }
}
