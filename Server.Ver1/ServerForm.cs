﻿using Cognex.VisionPro.ToolBlock;
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

namespace Server.Ver1
{
    public partial class ServerForm : Form
    {
        public TcpServer Server { get; set; }
        public Dictionary<string, Terminal> Input { get; set; }
        public Dictionary<string, Terminal> Output { get; set; }
        public static CogToolBlock CogToolBlock { get; set; }
        FrmStartup startuppage;

        public ServerForm()
        {
            
            InitializeComponent();
            
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            startuppage = new FrmStartup();
            startuppage.Show();
            this.Hide();
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
            CogToolBlock = CognexVisionSupport.Serialize.LoadToolBlock(@"C:\Users\duong\Desktop\Test_RemoteServer\ToolBlock\tool.vpp") as CogToolBlock;
            this.Show();
            startuppage.Close();
        }

        private void Server_Received(object sender, EventArgs e)
        {
            Thread _ = new Thread(() =>
            {
                byte[] receivedData = ((TcpArgs)e).Data;
                Input = TcpSupport.Serialize.ByteArrayToTerminal(receivedData);
                Terminal ImageTerminal = Input["Image"] as Terminal;
                Bitmap inputimage = ImageTerminal.Value as Bitmap;
                this.Display1.Invoke(new Action(() =>
                {
                    this.Display1.BackgroundImage = inputimage;
                }));
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
            ShowMessage($"Accpeted Connect From: {ip}:{port}");
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
            ToolForm toolform = new ToolForm();
            toolform.Show();
        }

        private void ServerForm_Shown(object sender, EventArgs e)
        {
            
        }

        
    }
}
