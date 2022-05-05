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
        public Dictionary<string,Socket> ConnectedClients { get; set; }
        public Dictionary<string,Task> ClientServiceTasks { get; set; }
        public CancellationTokenSource CancellationServerRunning { get; set; }

        public System.Timers.Timer CheckSubcribedClientsTimer;

        public int SendingTimeouttime = 1000;
        public int ReceivingTimeouttime = 1000;

        public TcpServer()
        {
            this.Listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
        public void Start(string _ip, int _port)
        {
            try
            {
                IPAddress address = IPAddress.Parse(_ip);
                ServerEP = new IPEndPoint(address, _port);

                Thread _bindingthread = new Thread(() =>
                {
                    for(int i = 0;i < 3; i++)
                    {
                        try
                        {
                            if (ServerEP == null) throw new Exception("Server IPEndpoint is Null!");
                            this.Listen.Bind(ServerEP);
                            this.Listen.Listen(100);
                            break;
                        }
                        catch
                        {
                            if(i == 2)
                            {
                                throw new Exception("Can not initial Server");
                            }
                        }
                    }
                });
                _bindingthread.Start();
                _bindingthread.Join();
                this.CancellationServerRunning = new CancellationTokenSource();
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

        public void Stop()
        {
            this.CancellationServerRunning.Cancel();
        }

        public async Task Run(CancellationTokenSource cancelSource)
        {
            ServerStatus = Status.Running;
            CancellationToken cancelToken = cancelSource.Token;
            try
            {
                Task _task = new Task(() =>
                {
                    //Command to Cancel Task
                    Thread _subtask = new Thread(() =>
                    {
                        for (; ; )
                        {
                            if (cancelToken.IsCancellationRequested)
                            {
                                cancelToken.ThrowIfCancellationRequested();
                            }
                        }
                    });
                    _subtask.Start();
                    _subtask.IsBackground = true;
                    //Do something
                    for (; ; )
                    {
                        // Waiting for connecting clients
                        Socket connectclient = this.Listen.Accept();
                        if(AcceptClientMode == Mode.BaseClientList)
                        {
                            if (!this.AccessableClients.Contains(((IPEndPoint)connectclient.RemoteEndPoint).Address.ToString())) return;
                        }
                        this.ConnectedClients.Add(((IPEndPoint)connectclient.RemoteEndPoint).Address.ToString(), connectclient);
                        Task _ = this.ClientServiceTask(connectclient);
                    }
                });
                _task.Start();
                await _task;
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
            }
        }

        public async Task ClientServiceTask(Socket client)
        {
            try
            {
                //--Receive
                //-->temp "receivedata"
                byte[] receivedata = null;
                bool iscomplete = false;
                int count4timeout = 0;
                Thread _receive = new Thread(() => 
                { 
                    this.Receive(client, receivedata);
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

                //-->Send
                iscomplete = false;
                count4timeout = 0;
                Thread _send = new Thread(() =>
                {
                    this.Send(client, receivedata);
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

            }
            catch(TimeoutException timeout)
            {
                // Do something when happenning timeout Exception
            }
            catch (Exception t)
            {
                Log.WriteLog(t);
            }
        }

        public void Send(Socket client,byte[] _sendData)
        {
            int offset = 0;
            int senddatalength = _sendData.Length;
            byte[] byte_senddatalengt = BitConverter.GetBytes(senddatalength);
            client.Send(_sendData);
            Thread.Sleep(10);
            while (true)
            {
                int write = client.Send(_sendData, offset, senddatalength - offset, SocketFlags.None);
                offset += write;
                if (offset == senddatalength) break;
            }
        }

        public void Receive(Socket client, byte[] _receiveData)
        {
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
        }
    }
}
