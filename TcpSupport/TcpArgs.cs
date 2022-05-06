using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpSupport
{
    public class TcpArgs:EventArgs
    {
        public byte[] Data;
        public TcpArgs(byte[] _data)
        {
            this.Data = _data;
        }
    }
}
