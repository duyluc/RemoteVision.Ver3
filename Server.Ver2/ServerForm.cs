using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpSupport;
using Ultilities;
using VisionSupport;
using CognexVisionSupport;
using Cognex.VisionPro;

namespace Server.Ver2
{
    public partial class ServerForm : Form
    {
        public TcpServer Server { get; set; }
        public Dictionary<string, Terminal> Input { get; set; }
        public Dictionary<string, Terminal> Output { get; set; }
        public static CogToolBlock CogToolBlock { get; set; }
        static public bool IsStartuped { get; set; } = false;
        public List<string> AcceptableClient;

        public ServerForm()
        {
            this.Hide();
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            this.Server = new TcpServer();
            Input = new Dictionary<string, Terminal>();
            Output = new Dictionary<string, Terminal>();
            this.Server.Listening += Server_Listening;
            this.Server.UnListening += Server_UnListening;
            this.Server.Running += Server_Running;
            this.Server.Stop += Server_Stop;
            this.Server.Accepted += Server_Accepted;
            this.Server.SendTimeout += Server_SendTimeout;
            this.Server.ReceivedTimeout += Server_ReceivedTimeout;
            this.Server.ProcessTimeout += Server_ProcessTimeout;
            this.Server.Received += Server_Received;
            this.dvAcceptableIp.Initial(this.dvAcceptableIp.Name);
            CogToolBlock = CognexVisionSupport.Serialize.LoadToolBlock(@"D:\TAI LIEU CONG VIEC\PROJECT SOURCE\RemoteVision.Ver3\Sources\ToolBlock\tool1.vpp") as CogToolBlock;
            this.MainDisplay.Tool = CogToolBlock;
            IsStartuped = true;
            AcceptableClient = new List<string>();
            this.Server.AcceptClientMode = TcpServer.Mode.BaseClientList;
            this.Show();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string ip = this.tbIp.Text;
            int port = int.Parse(this.tbPort.Text);
            if (this.Server.ServerStatus == TcpServer.Status.Running) return;
            try
            {
                Task _ =this.Server.StartServer(ip, port);
            }
            catch(Exception t)
            {
                ShowMessage(t.Message);
                Log.WriteLog(t.Message);
            }
        }

        public void ShowMessage(string t)
        {
            this.tbxMessage.Invoke(new Action(() => { this.tbxMessage.Text += t + Environment.NewLine; }));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Server.ServerStatus == TcpServer.Status.Running)
                {
                    this.Server.StopServer();
                }
            }
            catch (Exception t)
            {
                ShowMessage(t.Message);
                Log.WriteLog(t.Message);
            }
        }

        private void btnToolBlock_Click(object sender, EventArgs e)
        {
            ToolForm tool = new ToolForm();
            tool.Show();
        }

        private void ServerForm_Shown(object sender, EventArgs e)
        {
            
        }
        #region TCP
        //-->TCP Event Method
        private void Server_Received(object sender, EventArgs e)
        {
            Thread _ = new Thread(() =>
            {
                byte[] receivedData = ((TcpArgs)e).Data;
                Input = TcpSupport.Serialize.ByteArrayToTerminal(receivedData);
                Terminal ImageTerminal = Input["Image"] as Terminal;
                Bitmap inputimage = ImageTerminal.Value as Bitmap;
            });
            _.Start();
        }

        private void Server_ProcessTimeout(object sender, EventArgs e)
        {
            ShowMessage("Processing Timeout!");
        }

        private void Server_ReceivedTimeout(object sender, EventArgs e)
        {
            ShowMessage("Receiving Timeout!");
        }

        private void Server_SendTimeout(object sender, EventArgs e)
        {
            ShowMessage("Sending Timeout!");
        }

        private void Server_Accepted(object sender, EventArgs e)
        {
            Socket connectClinet = sender as Socket;
            if (connectClinet == null) return;
            string ip = ((IPEndPoint)connectClinet.RemoteEndPoint).Address.ToString();
            int port = ((IPEndPoint)connectClinet.RemoteEndPoint).Port;
            try
            {
                if (this.Server.AcceptClientMode == TcpServer.Mode.BaseClientList)
                {
                    if (!this.Server.AccessableClients.Contains(ip))
                    {
                        ShowMessage($">>> Block IP: {ip}:{port.ToString()}");
                        connectClinet.Disconnect(false);
                        return;
                    }
                }
                ShowMessage($"Accpeted Connect From: {ip}:{port}");
                this.Server.ConnectedClients.Add(((IPEndPoint)connectClinet.RemoteEndPoint).Address.ToString(), connectClinet);
                Task _ = this.ClientServiceTask(connectClinet);
            }
            catch(Exception t) 
            {
                ShowMessage(t.Message);
            }
        }

        private void Server_Stop(object sender, EventArgs e)
        {
            this.btnStart.Invoke(new Action(() => { this.btnStart.Enabled = true; }));
            this.btnStop.Invoke(new Action(() => { this.btnStop.Enabled = false; }));
            this.ShowMessage("Server was Stoped!");
        }

        private void Server_Running(object sender, EventArgs e)
        {
            this.ShowMessage("Server is Running...");
        }

        private void Server_UnListening(object sender, EventArgs e)
        {
            this.ShowMessage("Server is DisListening!");
        }

        private void Server_Listening(object sender, EventArgs e)
        {
            this.btnStart.Invoke(new Action(() =>
            {
                this.btnStart.Enabled = false;
            }));
            this.btnStop.Invoke(new Action(() => { this.btnStop.Enabled = true; }));
            this.ShowMessage("Server Listening...");
        }

        public async Task ClientServiceTask(Socket client)
        {
            Exception s = null;
            try
            {
                Task _task = new Task(() =>
                {
                    //->receive
                    bool iscomplete = false;
                    byte[] receiveData = null;
                    byte[] senddata = null;
                    receiveData = this.Server.Receive(client, out iscomplete);
                    if (!iscomplete) throw new TimeoutException();
                    //->processing
                    senddata = Processing(receiveData);
                    //->send
                    if (senddata == null) throw new Exception("Send Data is null");
                    iscomplete = this.Server.Send(client, senddata);
                    if (!iscomplete) throw new Exception("Send Data Fault!");
                });
                _task.Start();
                await _task;
            }
            catch (TimeoutException timeout)
            {
                s = timeout;
            }
            catch (Exception t)
            {
                Log.WriteLog(t);
                s = t;
            }
            finally
            {
                if (this.Server.ConnectedClients.ContainsKey(((IPEndPoint)client.RemoteEndPoint).Address.ToString()))
                    this.Server.ConnectedClients.Remove(((IPEndPoint)client.RemoteEndPoint).Address.ToString());
                if(s != null)
                {
                    throw s;
                }
            }
        }



        private void AcceptTable_TableChanged(object sender, EventArgs e)
        {
            this.Server.AccessableClients = (List<string>)sender;
            if (this.Server.AccessableClients.Count == 0) this.cbxShowClient.Items.Clear();
        }
        //-->
        #endregion
        #region Processing Vision
        public byte[] Processing(byte[] receivedata)
        {
            try
            {
                Dictionary<string, Terminal> input = TcpSupport.Serialize.ByteArrayToTerminal(receivedata);
                Terminal ter_image = input["Image"];
                Bitmap inputImage = ter_image.Value as Bitmap;
                CogImage8Grey coginputimage = new CogImage8Grey(inputImage);
                CogToolBlock.Inputs["InputImage"].Value = coginputimage;
                CogToolBlock.Run();
                Dictionary<string, Terminal> output = new Dictionary<string, Terminal>();

                foreach (CogToolBlockTerminal ter in CogToolBlock.Outputs)
                {
                    if (ter.ValueType == typeof(ICogImage))
                    {
                        Bitmap image = ((ICogImage)ter.Value).ToBitmap();
                        Terminal outterminal = new Terminal(ter.Name,image,typeof(Bitmap));
                        output.Add(outterminal.Name,outterminal);
                    }
                    else
                    {
                        Terminal outterminal = new Terminal(ter.Name,ter.Value,ter.ValueType);
                        output.Add(outterminal.Name, outterminal);
                    }
                }

                //-->Serialize output to byte array
                byte[] sendata = TcpSupport.Serialize.TerminalToByteArray(output);
                return sendata;

            }
            catch(NullReferenceException n)
            {
                return null;
            }
            catch (Exception t)
            {
                return null;
            }
        }
        #endregion
    }
}
