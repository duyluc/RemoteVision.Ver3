using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Ultilities;

namespace TcpSupport
{
    public class TcpClient
    {
        public enum Status
        {
            Free,
            Connected,
            Disconnected,
            Seding,
            Receiving
        }

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler Sended;
        public event EventHandler Received;

        public int SendingTimeouttime = 1000;
        public int ReceivingTimeouttime = 1000;
        public int ProcessTimeout = 2000;

        public void OnConnected()
        {
            Connected?.Invoke(this, EventArgs.Empty);
        }

        public void OnDisconnected()
        {
            Disconnected?.Invoke(this, EventArgs.Empty);
        }

        public void OnSended()
        {
            Sended?.Invoke(this, EventArgs.Empty);
        }

        public void OnReceived(byte[] _receivedData)
        {
            Received?.Invoke(this, new TcpArgs(_receivedData));
        }

        public Socket Client { get; set; }
        public TcpClient()
        {

        }

        public bool Connect(string _ip, int _port)
        {
            IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint iPEndpoint = new IPEndPoint(ip, _port);
            this.Client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            for(int i = 0; i < 3; i++)
            {
                try
                {
                    this.Client.Connect(iPEndpoint);
                    if (this.Client.Connected) break;
                }
                catch (Exception t)
                {
                    
                }
            }
            if (!this.Client.Connected) return false;
            else
            {
                this.OnConnected();
                return true;
            }
        }

        public void Send(Socket socket, byte[] _sendData)
        {
            if (_sendData == null)
            {
                return;
            }
            int offset = 0;
            int senddatalength = _sendData.Length;
            byte[] byte_senddatalengt = BitConverter.GetBytes(senddatalength);
            socket.Send(byte_senddatalengt);
            Thread.Sleep(10);
            while (true)
            {
                int write = socket.Send(_sendData, offset, senddatalength - offset, SocketFlags.None);
                offset += write;
                if (offset == senddatalength) break;
            }
        }

        public byte[] Receive(Socket socket)
        {
            byte[] byte_receivedatalengt = new byte[4];
            socket.Receive(byte_receivedatalengt, 0, 4, SocketFlags.None);
            int receivedatalength = BitConverter.ToInt32(byte_receivedatalengt, 0);
            byte[] _receiveData = new byte[receivedatalength];
            int offset = 0;
            while (true)
            {
                int read = socket.Receive(_receiveData, offset, receivedatalength - offset, SocketFlags.None);
                offset += read;
                if (offset == receivedatalength) break;
            }
            return _receiveData;
        }

        public async Task Run(byte[] _senddata)
        {
            try
            {
                bool iscomplete = false;
                int count4timeout = 0;
                //--> Send

                Thread _send = new Thread(() =>
                {
                    this.Send(this.Client, _senddata);
                    iscomplete = true;
                });
                _send.IsBackground = true;
                _send.Start();
                while (count4timeout < (ReceivingTimeouttime / 10) && !iscomplete)
                {
                    await Task.Delay(10);
                    count4timeout++;
                }
                if (!iscomplete)
                {
                    _send.Abort();
                    throw new TimeoutException();
                }
                this.OnSended();
                //--Receive
                //-->temp "receivedata"
                iscomplete = false;
                count4timeout = 0;
                byte[] receivedata = null;
                Thread _receive = new Thread(() =>
                {
                    receivedata = this.Receive(this.Client);
                    iscomplete = true;
                });
                _receive.IsBackground = true;
                _receive.Start();
                while (count4timeout < (ReceivingTimeouttime / 10) && !iscomplete)
                {
                    await Task.Delay(10);
                    count4timeout++;
                }
                if (!iscomplete)
                {
                    _receive.Abort();
                    throw new TimeoutException();
                }
                this.OnReceived(receivedata);
            }
            catch (TimeoutException timeout)
            {
                // Do something when happenning timeout Exception
            }
            catch (Exception t)
            {
                Log.WriteLog(t);
            }
            finally
            {

            }
        }

        public void Disconnect()
        {
            this.Client.Close();
            this.OnDisconnected();
        }
    }
}
