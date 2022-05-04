using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1
{
    public class StateObject
    {
        public Socket Listener { get; set; } = null;
        public Socket WorkSocket { get; set; } = null;
        public byte[] ReceiveData { get; set; } = null;
    }

    
    public class TcpServer
    {
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public IPEndPoint ServerEndPoint { get; set; } = null;
        public TcpServer(IPEndPoint _serverEndPoint)
        {
            this.ServerEndPoint = _serverEndPoint;
        }

        public void StartListening()
        {
            Socket Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Listener.Bind(ServerEndPoint);
            Listener.Listen(2);
            for(; ; )
            {
                allDone.Reset();
                Console.WriteLine("Waiting for a connect...");
                Listener.BeginAccept(new AsyncCallback(AcceptCallBack), Listener);
                allDone.WaitOne();
            }
        }

        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket _listener = ((Socket)ar.AsyncState);
            Socket handler
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
