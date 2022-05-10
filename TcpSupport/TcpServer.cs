using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Net.NetworkInformation;
using Ultilities;

namespace TcpSupport
{
    public class TcpServer
    {
        public enum Mode
        {
            Free,
            BaseClientList,
        }

        public enum Status
        {
            Free,
            Listening,
            Stoped,
            Started,
            Running,
        }

        public Status ListenerStatus { get; set; } = Status.Free;
        public Status ServerStatus { get; set; } = Status.Stoped;

        public Socket Listen { get; set; }
        public IPEndPoint ServerEP { get; set; }
        public Mode AcceptClientMode { get; set; } = Mode.Free;
        public List<string> AccessableClients { get; set; }
        public Dictionary<string, Socket> ConnectedClients { get; set; }
        public Dictionary<string, Task> ClientServiceTasks { get; set; }
        public CancellationTokenSource CancellationServerRunning { get; set; }

        public System.Timers.Timer CheckSubcribedClientsTimer;

        public int SendingTimeouttime = 5000;
        public int ReceivingTimeouttime = 5000;
        public int ProcessTimeouttime = 5000;

        // Event
        public event EventHandler Listening;
        public event EventHandler UnListening;
        public event EventHandler Running;
        public event EventHandler Stop;
        public event EventHandler Received;
        public event EventHandler Sended;
        public event EventHandler Accepted;
        public event EventHandler SendTimeout;
        public event EventHandler ReceivedTimeout;
        public event EventHandler ProcessTimeout;

        public void OnListening()
        {
            this.ListenerStatus = Status.Listening;
            Listening?.Invoke(this, EventArgs.Empty);
        }

        public void OnUnListening()
        {
            this.ListenerStatus = Status.Stoped;
            UnListening?.Invoke(this, EventArgs.Empty);
        }

        public void OnRunning()
        {
            this.ServerStatus = Status.Running;
            Running?.Invoke(this, EventArgs.Empty);
        }

        public void OnStop()
        {
            this.ServerStatus = Status.Stoped;
            Stop?.Invoke(this, EventArgs.Empty);
        }

        public void OnReceived(byte[] _data)
        {
            Received?.Invoke(this, new TcpArgs(_data));
        }

        public void OnSended()
        {
            Sended?.Invoke(this, EventArgs.Empty);
        }

        public void OnAccepted(Socket client)
        {
            Accepted?.Invoke(client, EventArgs.Empty);
        }

        public void OnSendTimeout()
        {
            SendTimeout?.Invoke(this, EventArgs.Empty);
        }

        public void OnReceiveTimeout()
        {
            ReceivedTimeout?.Invoke(this, EventArgs.Empty);
        }

        public void OnProceesTimeout()
        {
            ProcessTimeout?.Invoke(this, EventArgs.Empty);
        }
        //
        public TcpServer()
        {
            this.AccessableClients = new List<string>();
            this.ConnectedClients = new Dictionary<string, Socket>();
            this.ClientServiceTasks = new Dictionary<string, Task>();
            this.CheckSubcribedClientsTimer = new System.Timers.Timer();
            this.CheckSubcribedClientsTimer.Interval = 100;
            this.CheckSubcribedClientsTimer.Elapsed += CheckSubcribedClientsTimer_Elapsed;
            this.CheckSubcribedClientsTimer.Start();
        }

        private void CheckSubcribedClientsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.CheckSubcribedClientsTimer.Stop();
            if (this.AcceptClientMode == Mode.Free || this.AccessableClients.Count == 0) return;
            else
            {
                foreach(string clientip in AccessableClients)
                {
                    Ping ping = new Ping();
                    PingReply rep = ping.Send(clientip, 500);
                    if(rep.Status != IPStatus.Success)
                    {
                        //Do something when determining a offline client
                    }
                }
            }
            this.CheckSubcribedClientsTimer.Start();
        }

        /// <summary>
        /// Goi lenh khoi dong server
        /// </summary>
        public async Task StartServer(string _ip, int _port)
        {
            try
            {
                IPAddress address = IPAddress.Parse(_ip);
                ServerEP = new IPEndPoint(address, _port);
                this.Listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Task _bindingthread = new Task(() =>
                {
                    for(int i = 0;i < 3; i++)
                    {
                        try
                        {
                            if (ServerEP == null) throw new Exception("Server IPEndpoint is Null!");
                            this.Listen.Bind(ServerEP);
                            this.Listen.Listen(100);
                            this.OnListening();
                            break;
                        }
                        catch
                        {
                            if(i == 2)
                            {
                                this.OnUnListening();
                            }
                        }
                    }
                });
                _bindingthread.Start();
                await _bindingthread;
                if (this.ListenerStatus == Status.Stoped) throw new Exception("Can not Binding Server!");
                this.CancellationServerRunning = new CancellationTokenSource();
                Task _ = this.Run(CancellationServerRunning);
                //-->Reservation
                //if (!this.Listen.IsBound)
                //{
                //    throw new Exception("Server IPEndpoint is Null!");
                //}
            }
            catch(Exception t)
            {
                ServerStatus = Status.Stoped;
                throw t;
            }
        }

        public void StopServer()
        {
            this.CancellationServerRunning.Cancel();
        }
        public async Task Run(CancellationTokenSource cancelSource)
        {
            this.OnRunning();
            CancellationToken cancelToken = cancelSource.Token;
            try
            {
                Task _t = new Task(() =>
                {
                    while(true)
                    {
                        // Waiting for connecting clients
                        try
                        {
                            Socket connectclient = this.Listen.Accept();
                            this.OnAccepted(connectclient);
                        }
                        catch {}
                    }
                });
                _t.Start();
                //Command to Cancel Taskwhile(true)
                while(true)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        cancelToken.ThrowIfCancellationRequested();
                    }
                    await Task.Delay(5);
                }
            }
            catch(OperationCanceledException c)
            {
                cancelSource.Dispose();
                // Do something apter Cancelling Server
            }
            catch(Exception t)
            {
                // Do something when happenning Unhandler Exceptions
                cancelSource.Dispose();
                Log.WriteLog(t);
            }
            finally
            {
                this.Listen.Close();
                this.OnStop();
                this.OnUnListening();
            }
        }

        private void _send(Socket client,byte[] _sendData)
        {
            if(_sendData == null)
            {
                return;
            }
            int offset = 0;
            int senddatalength = _sendData.Length;
            byte[] byte_senddatalengt = BitConverter.GetBytes(senddatalength);
            client.Send(byte_senddatalengt);
            Thread.Sleep(10);
            while (true)
            {
                int write = client.Send(_sendData, offset, senddatalength - offset, SocketFlags.None);
                offset += write;
                if (offset == senddatalength) break;
            }
        }

        private byte[] _receive(Socket client)
        {
            byte[] _receiveData;
            byte[] byte_receivedatalengt = new byte[4];
            client.Receive(byte_receivedatalengt, 0, 4, SocketFlags.None);
            int receivedatalength = BitConverter.ToInt32(byte_receivedatalengt, 0);
            _receiveData = new byte[receivedatalength];
            int offset = 0;
            while (true)
            {
                int read = client.Receive(_receiveData, offset, receivedatalength - offset, SocketFlags.None);
                offset += read;
                if (offset == receivedatalength) break;
            }
            return _receiveData;
        }

        public bool Send(Socket _client, byte[] _sendData)
        {
            bool iscomplete = false;
            try
            {
                int count4timeout = 0;
                count4timeout = 0;
                Thread _send = new Thread(() =>
                {
                    this._send(_client, _sendData);
                    iscomplete = true;
                });
                _send.IsBackground = true;
                _send.Start();
                while (count4timeout < (ReceivingTimeouttime / 10) && !iscomplete)
                {
                    Thread.Sleep(10);
                    count4timeout++;
                }
                if (!iscomplete)
                {
                    _send.Abort();
                }
            }
            catch(Exception t)
            {
                throw t;
            }
            if (iscomplete) OnSended();
            return iscomplete;
        }

        public byte[] Receive(Socket _client, out bool _iscomplete)
        {
            bool iscomplete = false;
            byte[] receivedata = null;
            try
            {
                //--Receive
                //-->temp "receivedata"
                int count4timeout = 0;
                Thread _receive = new Thread(() =>
                {
                    receivedata = this._receive(_client);
                    iscomplete = true;
                });
                _receive.IsBackground = true;
                _receive.Start();
                while (count4timeout < (ReceivingTimeouttime / 10) && !iscomplete)
                {
                    Thread.Sleep(10);
                    count4timeout++;
                }
                if (!iscomplete)
                {
                    _receive.Abort();
                }
            }
            catch (Exception t)
            {
                throw t;
            }
            _iscomplete = iscomplete;
            if (_iscomplete) OnReceived(receivedata);
            return receivedata;
        }

        public byte[] Process(byte[] _data)
        {
            return _data;
        }
    }
}
